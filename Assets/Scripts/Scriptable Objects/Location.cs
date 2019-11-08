using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Location : MonoBehaviour
{
    [SerializeField] Location_SO location;
    [SerializeField] string locationName;
    [SerializeField] Scene scene;

    private void Awake()
    {
        locationName = location.LocationName;
    }

}
