using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameController : MonoBehaviour
{
    [SerializeField] Battle_SO battle;
    ObjectPooler pooler;
    Location battleScene;

    private void Start()
    {
        if (battle != null)
        {
            battleScene = battle.Location;           

        }      
    }
}
