using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class UnityInternalCodeEditor : EditorWindow
{
    string text = "Nothing Opened...";
    TextAsset txtAsset;
    Vector2 scroll;

    [MenuItem("Code Editor/Unity Internal Code Editor")]
    static void Init()
    {
        UnityInternalCodeEditor window = (UnityInternalCodeEditor)GetWindow(typeof(UnityInternalCodeEditor), true, "EditorGUILayout.TextArea");
        window.Show();
    }

    UnityEngine.Object source;

    void OnGUI()
    {
        source = EditorGUILayout.ObjectField(source, typeof(UnityEngine.Object), true);
        TextAsset newTxtAsset = (TextAsset)source;

        if (newTxtAsset != txtAsset)
            ReadTextAsset(newTxtAsset);

        scroll = EditorGUILayout.BeginScrollView(scroll);
        text = EditorGUILayout.TextArea(text);
        EditorGUILayout.EndScrollView();
        if (GUILayout.Button("Save"))
        {
            SaveFile();
        }
    }

    void ReadTextAsset(TextAsset txt)
    {
        text = txt.text;
        txtAsset = txt;
    }
    public void SaveFile()
    {
        try
        {
            StreamWriter sw = new StreamWriter(Application.dataPath + "/Scripts/" + source.name + ".cs");
            sw.Write(text);
            sw.Close();
            AssetDatabase.Refresh();

        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        finally
        {
            Console.WriteLine("Executing finally block.");
        }
    }
}
