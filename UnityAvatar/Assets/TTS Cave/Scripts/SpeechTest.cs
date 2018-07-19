using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechLib;
using UnityEngine.UI;

public class SpeechTest : MonoBehaviour {

    public static TTS voice;

    #region PLEASE SET THESE VARIABLES IN THE INSPECTOR

    private static Dictionary<string, List<string>> keywords;
    [System.Serializable]
    public struct m_Keyword
    {
        public string key;
        public List<string> param;
    }

    [SerializeField]
    private List<m_Keyword> customphrases;
    #endregion

    void Start()
    {
        voice = new TTS("female");
        var textToSpeech = this.GetComponent<HoloToolkit.Unity.TextToSpeech>();
        textToSpeech.StartSpeaking("One Two Three");
        PhraseParser();
    }

    void PhraseParser()
    {
        keywords = new Dictionary<string, List<string>>();

        foreach (m_Keyword word in customphrases)
        {
            keywords.Add(word.key, word.param);
        }
    }

    public static void VoiceOutput(string text)
    {
        List<string> outputValues;
        string customparams=string.Empty;
        if (text.StartsWith("custom"))
        {
            customparams = text.Substring(6);
            text = "custom";
        }
        switch (text)
        {
            case "ute":
                outputValues = new List<string>() { "was kann ich fuer dich tun", "ja", "wie kann ich dir helfen", "was gibts" };
                break;
            case "hello":
                outputValues = new List<string>() { "hallo","guten tag","hi","willkommen"};
                break;

            case "bye":
                outputValues = new List<string>() { "bis bald", "auf wiedersehen", "machs gut", "tschüss" };
                break;

            case "follow":
                outputValues = new List<string>() { "ok ich folge dir", "gerne", "hinterher", "ich laufe dir nach" };
                break;

            case "stop":
                outputValues = new List<string>() { "ok ich halte an", "ok ich bleib stehen", "ich stoppe", "dann bleib ich stehen" };
                break;

            case "walk":
                outputValues = new List<string>() { "ok ich lauf weiter", "dann lauf ich wieder los", "alles klar", "gerne" };
                break;

            case "table":
                outputValues = new List<string>() { "ok", "gerne", "ich gehe zum tisch", "ok ich laufe zum tisch" };
                break;

            case "animate":
                outputValues = new List<string>() { "yeeeeah", "wuhuuu", "lass uns tanzen", "fetzig" };
                break;

            case "search":
                outputValues = new List<string>() { "ich führe eine google suche aus", "ok ich nutze google", "lass mich das für dich suchen", "ich suche das für dich" };
                break;

            case "nothing":
                outputValues = new List<string>() { "" };
                break;

            case "custom":
                Debug.Log("cp:" + customparams);
                if(!keywords.TryGetValue(customparams, out outputValues))
                {
                    outputValues = new List<string>() { "" };
                }
                break;

            default:
                outputValues = new List<string>() { "das habe ich nicht verstanden", "was", "bitte wiederhole das", "entschuldigung ich habe dich nicht verstanden" };
                break;
        }
        string s = outputValues[Random.Range(0, outputValues.Count)];
        if(voice!=null)
            voice.speak(s);
    }
}
