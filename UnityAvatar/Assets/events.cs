using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class events : MonoBehaviour {


    public delegate void voiceI();
    public delegate void voiceO(string text);
    public static event voiceI voiceIn;
    public static event voiceO voiceOut;

    // Use this for initialization
    void Start ()
    {
        Debug.Log("press a and b for voice input and output");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (voiceIn != null)
                voiceIn();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (voiceOut != null)
                voiceOut("blaaaaa");
        }
    }
}
