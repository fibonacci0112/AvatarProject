using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InspectorClass))]

public class CustomInspector : Editor {



    enum displayFieldType { DisplayAsCustomizableGUIFields }
    displayFieldType DisplayFieldType;

    InspectorClass t;
    SerializedObject GetTarget;
    SerializedProperty ThisList;
    int ListSize;

    void OnEnable()
    {
        t = (InspectorClass)target;
        GetTarget = new SerializedObject(t);
        ThisList = GetTarget.FindProperty("MyList"); // Find the List in our script and create a refrence of it
    }

    public override void OnInspectorGUI()
    {
        //Update our list

        GetTarget.Update();

        //Choose how to display the list<> Example purposes only
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        DisplayFieldType = (displayFieldType)EditorGUILayout.EnumPopup("", DisplayFieldType);

        

        //Or add a new item to the List<> with a button
        EditorGUILayout.LabelField("Add a new item with a button");

        if (GUILayout.Button("Add New"))
        {
            t.MyList.Add(new InspectorClass.MyClass());
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Scripts for KI");

        //Display our list to the inspector window

        for (int i = 0; i < ThisList.arraySize; i++)
        {
            SerializedProperty MyListRef = ThisList.GetArrayElementAtIndex(i);

            SerializedProperty ScriptKI = MyListRef.FindPropertyRelative("ScriptKI");



            // Display the property fields in two ways.

            if (DisplayFieldType == 0)
            {// Choose to display automatic or custom field types. This is only for example to help display automatic and custom fields.
                //1. Automatic, No customization <-- Choose me I'm automatic and easy to setup
              
                EditorGUILayout.PropertyField(ScriptKI);
            }



            else
            {
                //Or

                //2 : Full custom GUI Layout <-- Choose me I can be fully customized with GUI options.
                EditorGUILayout.LabelField("Customizable Field With GUI");
                ScriptKI.objectReferenceValue = EditorGUILayout.ObjectField("ScriptKI", ScriptKI.objectReferenceValue, typeof(MonoScript), true);





            }
        }
        //Or add a new item to the List<> with a button
        EditorGUILayout.LabelField("Add a new item with a button");

        if (GUILayout.Button("Add New"))
        {
            t.MyList.Add(new InspectorClass.MyClass());
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Scripts for Sprachsteuerung");

        //Display our list to the inspector window

        for (int i = 0; i < ThisList.arraySize; i++)
        {
            SerializedProperty MyListRef = ThisList.GetArrayElementAtIndex(i);

            SerializedProperty ScriptSprach = MyListRef.FindPropertyRelative("ScriptSprach");



            // Display the property fields in two ways.

            if (DisplayFieldType == 0)
            {// Choose to display automatic or custom field types. This is only for example to help display automatic and custom fields.
             //1. Automatic, No customization <-- Choose me I'm automatic and easy to setup

                EditorGUILayout.PropertyField(ScriptSprach);
            }



            else
            {
                //Or

                //2 : Full custom GUI Layout <-- Choose me I can be fully customized with GUI options.
                EditorGUILayout.LabelField("Customizable Field With GUI");
                ScriptSprach.objectReferenceValue = EditorGUILayout.ObjectField("ScriptSprach", ScriptSprach.objectReferenceValue, typeof(MonoScript), true);





            }
        }



        //Apply the changes to our list
        GetTarget.ApplyModifiedProperties();
    }
}
 
 


