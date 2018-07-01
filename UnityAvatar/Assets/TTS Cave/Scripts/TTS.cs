using UnityEngine;
using SpeechLib;
using System.Runtime.InteropServices;
using System;

public class TTS {

    private SpVoice voice;
    private String gender;

	public TTS(String gender)
    {
        voice = new SpVoice();
        voice.Speak("<speak version='1.0' xmlns='http://www.w3.org/2001/10/synthesis' xml:lang='de-DE'></speak>",
                        SpeechVoiceSpeakFlags.SVSFlagsAsync | SpeechVoiceSpeakFlags.SVSFIsXML);
        this.gender = gender;
    }

    // Gibt den übergebenen String mit einer Stimme des übergeben Geschlechts (female, male) aus
    public void speak(String s)
    {
        try
        {
            voice.Voice = voice.GetVoices("gender=" + gender).Item(0);
        }
        catch (COMException e)
        {
            Console.WriteLine("No " + gender + " voice available");
        }
        voice.Speak(s, SpeechVoiceSpeakFlags.SVSFlagsAsync | SpeechVoiceSpeakFlags.SVSFIsXML);
    }

}
