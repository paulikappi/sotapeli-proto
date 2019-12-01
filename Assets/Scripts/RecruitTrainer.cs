using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecruitTrainer
{
    Queue<Unit> recruits = new Queue<Unit>();
    public FactionData faction;

    public void InitRecruits(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Unit recruit = new Unit();
            recruit.faction = faction;
            recruit.unitName = faction.surnames[Random.Range(0, faction.surnames.Count - 1)];
            recruit.rank = "Recruit";
            recruits.Enqueue(recruit);
            //Debug.Log("Recruited: " + recruit.Name);
        }
        for (int i = 0; i < faction.soldiersPerMinute; i++)
        {            
            TrainSoldier();
        }        
    }
    void TrainSoldier()
    {
        if (recruits.Count > 0)
        {
            Unit soldier = recruits.Dequeue();
            faction.soldierList.Add(soldier);
        }
        
    }

    IEnumerator TrainingDelay(float wait)
    {
        yield return new WaitForSeconds(wait);
    }    
}
