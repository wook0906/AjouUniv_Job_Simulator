using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
#if !UNITY_EDITOR
public class LocalizedTextEditor
{
}
#endif
#if UNITY_EDITOR
public class LocalizedTextEditor : EditorWindow
{
    public LocalizationData localizationData;
    
    [MenuItem ("Window/Localized Text Editor")]
    static void Init()
    {
        EditorWindow.GetWindow (typeof(LocalizedTextEditor)).Show ();
    }

    private Vector2 scrollPos = Vector2.zero;

    private void OnGUI()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, true, true, GUILayout.Width(GetWindow (typeof(LocalizedTextEditor)).position.width), GUILayout.Height(GetWindow (typeof(LocalizedTextEditor)).position.height));
        if (localizationData != null) 
        {
            SerializedObject serializedObject = new SerializedObject (this);
            SerializedProperty serializedProperty = serializedObject.FindProperty ("localizationData");
            
            
            EditorGUILayout.PropertyField (serializedProperty, true);
            serializedObject.ApplyModifiedProperties ();

            if (GUILayout.Button ("Save data")) 
            {
                SaveGameData ();
            }
        }

        if (GUILayout.Button ("Load data")) 
        {
            LoadGameData ();
        }

        if (GUILayout.Button ("Create new data")) 
        {
            CreateNewData ();
        }
        
        if (GUILayout.Button ("Load CSV File")) 
        {
            LoadCSVFile();
        }
        EditorGUILayout.EndScrollView();
    }

    private void LoadGameData()
    {
        string filePath = EditorUtility.OpenFilePanel ("Select localization data file", Application.dataPath, "txt");

        if (!string.IsNullOrEmpty (filePath)) 
        {
            string dataAsJson = File.ReadAllText (filePath);

            localizationData = JsonUtility.FromJson<LocalizationData> (dataAsJson);
        }
    }

    private void SaveGameData()
    {
        string filePath = EditorUtility.SaveFilePanel ("Save localization data file", Application.dataPath, "", "txt");

        if (!string.IsNullOrEmpty(filePath))
        {
            string dataAsJson = JsonUtility.ToJson(localizationData);
            File.WriteAllText (filePath, dataAsJson);
        }
    }
    private void LoadCSVFile()
    {
        string filePath = EditorUtility.OpenFilePanel ("Select localization data file", Application.dataPath, "csv");
        
        if (!string.IsNullOrEmpty (filePath)) 
        {
            string dataAsJson = File.ReadAllText (filePath, Encoding.UTF8);
            string[] stringBigList = dataAsJson.Split('\n');
            localizationData = new LocalizationData();
            localizationData.items = new LocalizationItem[stringBigList.Length];
            for (var i = 1; i < stringBigList.Length; i++)
            {
                string[] stringList = stringBigList[i].Split(',');
                for (var j = 0; j < stringList.Length; j++)
                {
                    localizationData.items[i - 1] = new LocalizationItem(stringList[1], stringList[2]);
                }
            }
        }
    }
    private void CreateNewData()
    {
        localizationData = new LocalizationData ();
    }
}
#endif
