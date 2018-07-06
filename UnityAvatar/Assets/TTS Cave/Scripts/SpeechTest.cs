using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechLib;

public class SpeechTest : MonoBehaviour {

    private TTS voice;

    void Start()
    {
        voice = new TTS("female");
        var textToSpeech = this.GetComponent<HoloToolkit.Unity.TextToSpeech>();
        textToSpeech.StartSpeaking("One Two Three");
        events.voiceOut += VoiceOutput;
    }

    public void VoiceOutput(string text)
    {
        voice.speak(text);
    }
}
