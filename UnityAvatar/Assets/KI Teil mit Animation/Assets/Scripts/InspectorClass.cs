
using UnityEngine;
using System;
using System.Collections.Generic; // Import the System.Collections.Generic class to give us access to List<>
using UnityScript;
public class InspectorClass : MonoBehaviour
{

    //This is our custom class with our variables
    [System.Serializable]
    public class MyClass
    {
        public UnityEditor.MonoScript ScriptKI;
        public UnityEditor.MonoScript ScriptSprach;
       
    }

    //This is our list we want to use to represent our class as an array.
    public List<MyClass> MyList = new List<MyClass>(1);


    void AddNew()
    {
        //Add a new index position to the end of our list
        MyList.Add(new MyClass());
    }

    
}


