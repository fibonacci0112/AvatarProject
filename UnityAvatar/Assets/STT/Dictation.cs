﻿using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using System.Collections.Generic;
using System;
using System.IO;

public class Dictation : MonoBehaviour
{
    [SerializeField]
    private Text m_Hypotheses;

    [SerializeField]
    private Text m_Recognitions;

    [SerializeField]
    private String m_keywordFilePath;

    private DictationRecognizer m_DictationRecognizer;

    private Dictionary<string, List<string>> keywords;

    [Serializable]
    public struct m_Keyword
    {
        public string key;
        public List<string> param;
    }

    [SerializeField]
    private List<m_Keyword> keywordinput;

    void Start()
    {
        if(m_keywordFilePath.Equals(""))
        {
            //TODO: use keywordfilepath to search for keyword file. if exists fill keywords
        }

        CustomKeywordParser();
        KeywordProcessing("zeig mir einen schuh der rot ist");
        m_DictationRecognizer = new DictationRecognizer();

        m_DictationRecognizer.DictationResult += (text, confidence) =>
        {
            Debug.LogFormat("Dictation result: {0}", text);
            m_Recognitions.text += text + "\n";
            KeywordProcessing(text);
            events.output = text;
        };

        m_DictationRecognizer.DictationHypothesis += (text) =>
        {
            //Debug.LogFormat("Dictation hypothesis: {0}", text);
            m_Hypotheses.text += text;
        };

        m_DictationRecognizer.DictationComplete += (completionCause) =>
        {
            if (completionCause != DictationCompletionCause.Complete)
                Debug.LogErrorFormat("Dictation completed unsuccessfully: {0}.", completionCause);
        };

        m_DictationRecognizer.DictationError += (error, hresult) =>
        {
            Debug.LogErrorFormat("Dictation error: {0}; HResult = {1}.", error, hresult);
        };


        events.voiceIn += VoiceInput;
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
        List<string> greetings = new List<string> {"hallo", "hi", "hey"};
        List<string> search = new List<string>() { "suche", "such", "google", "schau", "schaue" };
        List<string> sparams = new List<string>();
        int flag = -1;


        string result = "";

        //keyword search 
        foreach (string keyword in words)
        {
            
            if(greetings.Contains(keyword))
            {
                Debug.Log("greeting keyword - " + keyword + " - found");
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
                Debug.Log(suche);
                suche = suche.Substring(suche.IndexOf(result));
                Debug.Log(suche);
                suche = suche.Substring(result.Length + 1 );
                Debug.Log(suche);
                break;

            case 2:
                for (int i = 0; i < words.Length; i++)
                {
                    paramlist += " " + keywords[result].Find(x => x.Equals(words[i]));
                }
                break;
        }
        Debug.Log("params - " + paramlist + " - found");
    }

    public void VoiceInput()
    {
        m_DictationRecognizer.Start();
    }
}
