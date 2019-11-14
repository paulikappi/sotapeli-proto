using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInspector : MonoBehaviour
{
    [SerializeField] BattleData battle;
    // Start is called before the first frame update
    void Start()
    {
        if (BattleController.SharedInstance != null)
        {
            if (battle != null)
            {
                BattleController.SharedInstance.Battle = battle;
            }

            else if (BattleController.SharedInstance.Battle != null)
            {
                battle = BattleController.SharedInstance.Battle;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
