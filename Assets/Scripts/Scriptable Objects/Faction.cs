using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction : MonoBehaviour
{
    [SerializeField] Faction_SO faction;
    [SerializeField] string commanderName;
    private void Awake()
    {
        commanderName = faction.CommanderName;
    }    
}
