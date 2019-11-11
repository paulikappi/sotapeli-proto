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
        
        pooler = GetComponent<ObjectPooler>();

        FetchBattle_SO();
    }

    void FetchBattle_SO()
    {
        SceneManager.LoadScene(battle.Location.GetComponent<ScenePicker>().scenePath, LoadSceneMode.Additive);
        Debug.Log(battle.Location.GetComponent<ScenePicker>().scenePath);
    }

    void SetObjectPoolerObjects()
    {
        //pooler.SetPoolObject();
    }
}
