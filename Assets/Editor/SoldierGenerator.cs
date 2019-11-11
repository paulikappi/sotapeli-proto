using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SoldierGenerator : EditorWindow
{
    int count = 1;    
    bool groupEnabled;    
    float myFloat;

    static List<Faction> factionList;
    static string[] factionNameList;
    static List<int> factionIndexList;

    [MenuItem("Window/Soldier Generator")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(SoldierGenerator));
    }

    static void Init()
    {
        factionNameList = GetScriptableObjectNameArray<Faction>();
    }

    public static string[] GetScriptableObjectNameArray<T>() where T : ScriptableObject
    {
        string[] factionNameArray;
        factionNameArray = AssetDatabase.FindAssets(typeof(Faction).Name);
        Debug.Log(factionNameArray);
        T[] a = new T[factionNameArray.Length];        
        
        for (int i = 0; i < factionNameArray.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(factionNameArray[i]);
            a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            factionIndexList.Add(i);
        }        
        return factionNameArray;
    }


    void OnGUI()
    {
        EditorGUILayout.LabelField("Base Settings", EditorStyles.boldLabel);        
        //EditorGUILayout.LongField("Amount");
        //EditorGUILayout.Popup(factionIndex, factionList);
       
        if (GUILayout.Button("Generate"))
        {
            GenerateSoldiers(myFloat);
        }
    }

    void GenerateSoldiers(float i)
    {
    }
}

