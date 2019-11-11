using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameController : MonoBehaviour
{
    [SerializeField] Battle battle;
    ObjectPooler pooler;

    private void Start()
    {        
        pooler = GetComponent<ObjectPooler>();

        FetchBattle_SO();
    }

    void FetchBattle_SO()
    {
        if (battle.Location != null)
        {
            //SceneManager.LoadScene(battle.Location.GetComponent<ScenePicker>().scenePath, LoadSceneMode.Additive);
            //Debug.Log(battle.Location.GetComponent<ScenePicker>().scenePath);
        }        
    }

    void SetObjectPoolerObjects()
    {
        //pooler.SetPoolObject();
    }
}
