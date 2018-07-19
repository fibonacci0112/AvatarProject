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

    private Dictionary<string, List<string>> keywords;
    private Boolean flag = false;

    [Serializable]
    public struct m_Keyword
    {
        public string key;
        public List<string> param;
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
                EventManager.TriggerEvent("KI_movement", "000ute");
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
        keywords = new Dictionary<string, List<string>>();

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
        List<string> walk = new List<string> { "lauf", "laufe", "geh", "gehe" };
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
                Debug.Log("greeting keyword - " + keyword + " - found");
                EventManager.TriggerEvent("KI_movement", "000hello");
                break;
            }

            if (goodbye.Contains(keyword))
            {
                Debug.Log("goodbye keyword - " + keyword + " - found");
                EventManager.TriggerEvent("KI_movement", "000bye");
                break;
            }

            if (stop.Contains(keyword))
            {
                Debug.Log("stop keyword - " + keyword + " - found");
                EventManager.TriggerEvent("KI_movement", "000stop");
                break;
            }

            if (walk.Contains(keyword))
            {
                Debug.Log("walk keyword - " + keyword + " - found");
                EventManager.TriggerEvent("KI_movement", "000walk");
                break;
            }

            if (table.Contains(keyword))
            {

                Debug.Log("table keyword - " + keyword + " - found");
                EventManager.TriggerEvent("KI_movement", "000table");
                break;
            }

            if (animate.Contains(keyword))
            {
                Debug.Log("animate keyword - " + keyword + " - found");
                EventManager.TriggerEvent("KI_movement", "000animate");
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
            else
            {
                EventManager.TriggerEvent("KI_movement", "000nothing");
                break;
            }
        }

        //parameter search
        string paramlist = "";
        switch (flag)
        {
            case 0:
                //do param free event
                break;

            case 1:
                
                string suche = string.Join(" ", words);
                
                suche = suche.Substring(suche.IndexOf(result));
                Debug.Log(suche);
                if (suche.Equals(result))
                    suche += "nichts";
                suche = suche.Substring(result.Length + 1);
                
                if (suche.Contains("nach"))
                {
                    suche = suche.Remove(0, 3);
                }
                EventManager.TriggerEvent("KI_movement", "000search");
                break;

            case 2:
                for (int i = 0; i < words.Length; i++)
                {
                    paramlist += " " + keywords[result].Find(x => x.Equals(words[i]));
                }
                EventManager.TriggerEvent("KI_movement", "000custom" + result + paramlist);
                break;
        }
    }

    public void VoiceInput()
    {
        m_DictationRecognizer.Start();
    }
}
