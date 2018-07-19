using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using System.Collections.Generic;
using System;
using System.IO;

public class Dictation : MonoBehaviour
{
    #region PLEASE SET THESE VARIABLES IN THE INSPECTOR
    [SerializeField]
    private Text m_Hypotheses;

    [SerializeField]
    private Text m_Recognitions;

    private DictationRecognizer m_DictationRecognizer;

    private Dictionary<string, string> keywords;
    private Boolean flag = false;

    [Serializable]
    public struct m_Keyword
    {
        public string key;
        public string param;
    }

    [SerializeField]
    private List<m_Keyword> keywordinput;
    #endregion

    void Start()
    {

        CustomKeywordParser();
        KeywordProcessing("zeig mir einen schuh der rot ist");
        m_DictationRecognizer = new DictationRecognizer();

        m_DictationRecognizer.DictationResult += (text, confidence) =>
        {
            Debug.LogFormat("Dictation result: {0}", text);
            m_Recognitions.text += text + "\n";
            
            if (flag)
            {
                KeywordProcessing(text);
                flag = false;
            }
            if (text.Equals("helga"))
            {
                flag = true;
                m_DictationRecognizer.Start();
                EventManager.TriggerEvent("KI_movement", "000100ute");
            }
        };

        m_DictationRecognizer.DictationHypothesis += (text) =>
        {
            //Debug.LogFormat("Dictation hypothesis: {0}", text);
            m_Hypotheses.text += text;
        };

        m_DictationRecognizer.DictationComplete += (completionCause) =>
        {
            if (completionCause != DictationCompletionCause.Complete)
                //Debug.LogErrorFormat("Dictation completed unsuccessfully: {0}.", completionCause);
                m_DictationRecognizer.Stop();
                m_DictationRecognizer.Start();
        };

        m_DictationRecognizer.DictationError += (error, hresult) =>
        {
            //Debug.LogErrorFormat("Dictation error: {0}; HResult = {1}.", error, hresult);
            m_DictationRecognizer.Stop();
            m_DictationRecognizer.Start();
        };


        m_DictationRecognizer.Start();
    }

    void CustomKeywordParser()
    {
        keywords = new Dictionary<string, string>();

        foreach (m_Keyword word in keywordinput)
        {
            keywords.Add(word.key, word.param);
        }
    }

    void KeywordProcessing(string text)
    {
        string[] words = text.Split(' ');
        List<string> greetings = new List<string> { "hallo", "hi", "hey" };
        List<string> goodbye = new List<string> { "tschüss", "wiedersehen" };
        List<string> stop = new List<string> { "stop", "stopp", "halt", "stehen" };
        List<string> follow = new List<string> { "folge", "folg", "hinterher"};
        List<string> walk = new List<string> { "lauf", "laufe"};
        List<string> table = new List<string> { "tisch" };
        List<string> animate = new List<string> { "tanz" };
        List<string> search = new List<string>() { "suche", "google", "look" };
        int flag = 0;


        string result = "";

        //keyword search 
        foreach (string keyword in words)
        {

            if (greetings.Contains(keyword))
            {
                EventManager.TriggerEvent("KI_movement", "000001hello");
                break;
            }

            if (goodbye.Contains(keyword))
            {
                EventManager.TriggerEvent("KI_movement", "000001bye");
                break;
            }

            if (stop.Contains(keyword))
            {
                EventManager.TriggerEvent("KI_movement", "000100stop");
                break;
            }

            if (follow.Contains(keyword))
            {
                EventManager.TriggerEvent("KI_movement", "100000follow");
                break;
            }

            if (walk.Contains(keyword))
            {
                EventManager.TriggerEvent("KI_movement", "010000walk");
                break;
            }

            if (table.Contains(keyword))
            {

                EventManager.TriggerEvent("KI_movement", "001000table");
                break;
            }

            if (animate.Contains(keyword))
            {
                EventManager.TriggerEvent("KI_movement", "000010animate");
                break;
            }

            if (search.Contains(keyword))
            {
                result = keyword;
                flag = 1;
                break;
            }

            if (keywords.ContainsKey(keyword))
            {
                result = keyword;
                flag = 2;
                break;
            }
            else
            {
                EventManager.TriggerEvent("KI_movement", "000100nothing");
                break;
            }
        }

        //parameter search
        string param = "0";
        switch (flag)
        {
            case 0:
                //do param free event
                break;

            case 1:
                
                string suche = string.Join(" ", words);
                
                suche = suche.Substring(suche.IndexOf(result));
                
                if (suche.Equals(result))
                    suche += "nichts";
                suche = suche.Substring(result.Length + 1);
                
                if (suche.Contains("nach"))
                {
                    suche = suche.Remove(0, 3);
                }
                EventManager.TriggerEvent("KI_movement", "000100search");
                break;

            case 2:
                keywords.TryGetValue(result, out param);
                EventManager.TriggerEvent("KI_movement", "000100nothing");

                EventManager.TriggerEvent("KI_custom", param + "custom" + result);
                break;
        }
    }

    public void VoiceInput()
    {
        m_DictationRecognizer.Start();
    }
}
