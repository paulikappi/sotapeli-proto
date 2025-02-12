﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class SoldierGenerator : EditorWindow
{
    int count = 1;    
    bool groupEnabled;    
    float myFloat;

    static List<FactionData> factionList;
    string[] factionNameList;
    static List<int> factionIndexList;
    List<GameObject> objList;

    [MenuItem("Window/SoldierData Generator")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(SoldierGenerator));
    }    

    void Start()
    {
        factionNameList = GetScriptableObjectNameArray<FactionData>();        
        Debug.Log("Game Manager set");
    }

    public static string[] GetScriptableObjectNameArray<T>() where T : ScriptableObject
    {
        string[] factionNameArray;
        factionNameArray = AssetDatabase.FindAssets(typeof(FactionData).Name);
        T[] a = new T[factionNameArray.Length];        
        
        for (int i = 0; i < factionNameArray.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(factionNameArray[i]);
            a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            if (factionIndexList != null)
            {
                factionIndexList.Add(i);
            }
            
        }        
        return factionNameArray;
    }


    void OnGUI()
    {
        EditorGUILayout.LabelField("Base Settings", EditorStyles.boldLabel);        
        //EditorGUILayout.LongField("Amount");
        //EditorGUILayout.Popup(factionIndex, factionList);
       
        
        if (GUILayout.Button("PoolUnits BattleData Formations"))
        {
        }
    }
}

