using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BattleController battleController;
    public ObjectPooler pooler;    
    
    private void Start()
    {        
        battleController = FindObjectOfType<BattleController>();
        pooler = FindObjectOfType<ObjectPooler>();

        battleController.InitBattleFormations();

    }
}
