/**
* Copyright 2015 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

using UnityEngine;
using System.Collections;
using IBM.Watson.DeveloperCloud.Logging;
using IBM.Watson.DeveloperCloud.Services.SpeechToText.v1;
using IBM.Watson.DeveloperCloud.Utilities;
using IBM.Watson.DeveloperCloud.DataTypes;
using System.Collections.Generic;
using UnityEngine.UI;

public class ExampleStreaming : MonoBehaviour
{
    #region PLEASE SET THESE VARIABLES IN THE INSPECTOR
    [Space(10)]
    [Tooltip("The service URL (optional). This defaults to \"https://stream.watsonplatform.net/speech-to-text/api\"")]
    [SerializeField]
    private string _serviceUrl;
    [Tooltip("Text field to display the results of streaming.")]
    public Text ResultsField;
    [Header("CF Authentication")]
    [Tooltip("The authentication username.")]
    [SerializeField]
    private string _username;
    [Tooltip("The authentication password.")]
    [SerializeField]
    private string _password;
    [Header("IAM Authentication")]
    [Tooltip("The IAM apikey.")]
    [SerializeField]
    private string _iamApikey;
    [Tooltip("The IAM url used to authenticate the apikey (optional). This defaults to \"https://iam.bluemix.net/identity/token\".")]
    [SerializeField]
    private string _iamUrl;

    private Dictionary<string, List<string>> keywords;

    [System.Serializable]
    public struct m_Keyword
    {
        public string key;
        public List<string> param;
    }

    [SerializeField]
    private List<m_Keyword> keywordinput;
    #endregion


    private int _recordingRoutine = 0;
    private string _microphoneID = null;
    private AudioClip _recording = null;
    private int _recordingBufferSize = 1;
    private int _recordingHZ = 22050;

    private SpeechToText _service;

    void Start()
    {
        //LogSystem.InstallDefaultReactors();
        CustomKeywordParser();
        Runnable.Run(CreateService());
    }

    private IEnumerator CreateService()
    {
        //  Create credential and instantiate service
        Credentials credentials = null;
        if (!string.IsNullOrEmpty(_username) && !string.IsNullOrEmpty(_password))
        {
            //  Authenticate using username and password
            credentials = new Credentials(_username, _password, _serviceUrl);
        }
        else if (!string.IsNullOrEmpty(_iamApikey))
        {
            //  Authenticate using iamApikey
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = _iamApikey,
                IamUrl = _iamUrl
            };

            credentials = new Credentials(tokenOptions, _serviceUrl);

            //  Wait for tokendata
            while (!credentials.HasIamTokenData())
                yield return null;
        }
        else
        {
            throw new WatsonException("Please provide either username and password or IAM apikey to authenticate the service.");
        }

        _service = new SpeechToText(credentials);
        _service.StreamMultipart = true;

        
        Active = true;
        StartRecording();
    }

    public bool Active
    {
        get { return _service.IsListening; }
        set
        {
            if (value && !_service.IsListening)
            {
                _service.DetectSilence = true;
                _service.EnableWordConfidence = true;
                _service.EnableTimestamps = true;
                _service.SilenceThreshold = 0.01f;
                _service.MaxAlternatives = 0;
                _service.EnableInterimResults = true;
                _service.OnError = OnError;
                _service.InactivityTimeout = -1;
                _service.ProfanityFilter = false;
                _service.SmartFormatting = true;
                _service.SpeakerLabels = false;
                _service.WordAlternativesThreshold = null;
                _service.StartListening(OnRecognize, OnRecognizeSpeaker);
            }
            else if (!value && _service.IsListening)
            {
                _service.StopListening();
            }
        }
    }

    private void StartRecording()
    {
        if (_recordingRoutine == 0)
        {
            UnityObjectUtil.StartDestroyQueue();
            _recordingRoutine = Runnable.Run(RecordingHandler());
        }
    }

    private void StopRecording()
    {
        if (_recordingRoutine != 0)
        {
            Microphone.End(_microphoneID);
            Runnable.Stop(_recordingRoutine);
            _recordingRoutine = 0;
        }
    }

    private void OnError(string error)
    {
        Active = false;

        Log.Debug("ExampleStreaming.OnError()", "Error! {0}", error);
    }

    private IEnumerator RecordingHandler()
    {
        Log.Debug("ExampleStreaming.RecordingHandler()", "devices: {0}", Microphone.devices);
        _recording = Microphone.Start(_microphoneID, true, _recordingBufferSize, _recordingHZ);
        yield return null;      // let _recordingRoutine get set..

        if (_recording == null)
        {
            StopRecording();
            yield break;
        }

        bool bFirstBlock = true;
        int midPoint = _recording.samples / 2;
        float[] samples = null;

        while (_recordingRoutine != 0 && _recording != null)
        {
            int writePos = Microphone.GetPosition(_microphoneID);
            if (writePos > _recording.samples || !Microphone.IsRecording(_microphoneID))
            {
                Log.Error("ExampleStreaming.RecordingHandler()", "Microphone disconnected.");

                StopRecording();
                yield break;
            }

            if ((bFirstBlock && writePos >= midPoint)
              || (!bFirstBlock && writePos < midPoint))
            {
                // front block is recorded, make a RecordClip and pass it onto our callback.
                samples = new float[midPoint];
                _recording.GetData(samples, bFirstBlock ? 0 : midPoint);

                AudioData record = new AudioData();
				record.MaxLevel = Mathf.Max(Mathf.Abs(Mathf.Min(samples)), Mathf.Max(samples));
                record.Clip = AudioClip.Create("Recording", midPoint, _recording.channels, _recordingHZ, false);
                record.Clip.SetData(samples, 0);

                _service.OnListen(record);

                bFirstBlock = !bFirstBlock;
            }
            else
            {
                // calculate the number of samples remaining until we ready for a block of audio, 
                // and wait that amount of time it will take to record.
                int remaining = bFirstBlock ? (midPoint - writePos) : (_recording.samples - writePos);
                float timeRemaining = (float)remaining / (float)_recordingHZ;

                yield return new WaitForSeconds(timeRemaining);
            }

        }

        yield break;
    }

    private void OnRecognize(SpeechRecognitionEvent result, Dictionary<string, object> customData)
    {
        if (result != null && result.results.Length > 0)
        {
            foreach (var res in result.results)
            {
                foreach (var alt in res.alternatives)
                {
                    string text = string.Format(alt.transcript);
                    if (res.final)
                    {
                        KeywordProcessing(text);
                    }
                }

                if (res.keywords_result != null && res.keywords_result.keyword != null)
                {
                    foreach (var keyword in res.keywords_result.keyword)
                    {
                        //Log.Debug("ExampleStreaming.OnRecognize()", "keyword: {0}, confidence: {1}, start time: {2}, end time: {3}", keyword.normalized_text, keyword.confidence, keyword.start_time, keyword.end_time);
                    }
                }

                if (res.word_alternatives != null)
                {
                    foreach (var wordAlternative in res.word_alternatives)
                    {
                        // Log.Debug("ExampleStreaming.OnRecognize()", "Word alternatives found. Start time: {0} | EndTime: {1}", wordAlternative.start_time, wordAlternative.end_time);
                        foreach (var alternative in wordAlternative.alternatives)
                        {
                            //Log.Debug("ExampleStreaming.OnRecognize()", "\t word: {0} | confidence: {1}", alternative.word, alternative.confidence);
                        }
                    }
                }
            }
        }
    }

    private void OnRecognizeSpeaker(SpeakerRecognitionEvent result, Dictionary<string, object> customData)
    {
        if (result != null)
        {
            foreach (SpeakerLabelsResult labelResult in result.speaker_labels)
            {
               // Log.Debug("ExampleStreaming.OnRecognize()", string.Format("speaker result: {0} | confidence: {3} | from: {1} | to: {2}", labelResult.speaker, labelResult.from, labelResult.to, labelResult.confidence));
            }
        }
    }

    void CustomKeywordParser()
    {
        keywords = new Dictionary<string, List<string>>();

        foreach (m_Keyword word in keywordinput)
        {
            keywords.Add(word.key, word.param);
        }
    }

    void KeywordProcessing(string text)
    {
        events.testout(text);
        Debug.Log(text);
        string[] words = text.Split(' ');
        List<string> greetings = new List<string> { "hello", "hi", "hey" };
        List<string> goodbye = new List<string> { "goodbye", "bye"};
        List<string> stop = new List<string> { "stop", "halt", "stand"};
        List<string> walk = new List<string> { "walk", "walking", "move", "patrol" };
        List<string> table = new List<string> { "table"};
        List<string> animate = new List<string> { "dance"};
        List<string> search = new List<string>() { "search", "google", "look"};
        List<string> sparams = new List<string>();
        int flag = -1;
        

        string result = "";

        //keyword search 
        foreach (string keyword in words) 
        {

            if (greetings.Contains(keyword))
            {
                Debug.Log("greeting keyword - " + keyword + " - found");
               
                result = keyword;
                flag = 0;
                break;
            }

            if (goodbye.Contains(keyword))
            {
                Debug.Log("goodbye keyword - " + keyword + " - found");
                result = keyword;
                flag = 0;
                break;
            }

            if (walk.Contains(keyword))
            {
                Debug.Log("walk keyword - " + keyword + " - found");
                result = keyword;
                flag = 0;
                break;
            }

            if (table.Contains(keyword))
            {
                EventManager.TriggerEvent("bla");
                Debug.Log("table keyword - " + keyword + " - found");
                result = keyword;
                flag = 0;
                break;
            }

            if (animate.Contains(keyword))
            {
                Debug.Log("animate keyword - " + keyword + " - found");
                result = keyword;
                flag = 0;
                break;
            }

            if (search.Contains(keyword))
            {
                Debug.Log("search keyword - " + keyword + " - found");
                result = keyword;
                flag = 1;
                break;
            }

            if (keywords.ContainsKey(keyword))
            {
                Debug.Log("custom keyword - " + keyword + " - found");
                result = keyword;
                flag = 2;
                break;
            }
        }

        //take out keyword
        //words[Array.IndexOf(words, result)] = "";
        //parameter search
        string paramlist = "";
        switch (flag)
        {
            case 0:
                //do greeting. no params needed
                break;

            case 1:
                string suche = string.Join(" ", words);
                suche = suche.Substring(suche.IndexOf(result));
                suche = suche.Substring(result.Length + 1);
                Debug.Log(suche);
                if (suche.Contains("for"))
                {
                    suche = suche.Remove(0, 3);
                }
                Debug.Log("Google suche nach: " + suche);
                break;

            case 2:
                for (int i = 0; i < words.Length; i++)
                {
                    paramlist += " " + keywords[result].Find(x => x.Equals(words[i]));
                }
                Debug.Log("params - " + paramlist + " - found");
                break;
        }
        
    }
}
