using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Location", menuName = "Scriptable Objects/Location", order = 3)]
public class Location_SO : ScriptableObject
{
    [SerializeField] string locationName;
    [SerializeField] GameObject locationEnvironment;

    public string LocationName
    {
        get
        {
            return locationName;
        }
    }
}
