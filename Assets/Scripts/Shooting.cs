using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float shootingDistance = 100;
    float propability;
    int missCount = 0;
    int hitCount = 0;

    public struct SkillLevel
    {
        public float Angle;
        public SkillLevel(float angle)
        {
            Angle = angle;
        }
    }

    SkillLevel Elite = new SkillLevel(0.02f);
    SkillLevel Pro = new SkillLevel(0.04f);
    SkillLevel Good = new SkillLevel(0.06f);
    SkillLevel Ok = new SkillLevel(0.11f);
    SkillLevel Basic = new SkillLevel(0.15f);
    SkillLevel Bad = new SkillLevel(0.18f);
    SkillLevel Awful = new SkillLevel(0.21f);

    public struct Stance
    {
        public float Sum;
        public float Multi;
        public float Area;

        public Stance(float sum, float multi, float area)
        {
            Sum = sum;      // main factor
            Multi = multi;  // smoothing factor
            Area = area;
        }
    }

    Stance Prone = new Stance(0.0f, 1.0f, 0.13f);
    Stance Kneeing = new Stance(0.2f, 1.0f, 0.25f);
    Stance Standing = new Stance(0.25f, 0.9f, 0.4f);
    Stance Walking = new Stance(0.35f, 0.7f, 0.35f);
    Stance Running = new Stance(0.45f, 0.6f, 0.3f);

    private void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            HitCalculator(Good, Prone, Running, 150);            
        }
        Debug.Log("Prob: " + propability);
        Debug.Log("Hit %: " + 100 * Convert.ToSingle(hitCount) / Convert.ToSingle(missCount + hitCount));
    }

    public bool HitCalculator(SkillLevel skillLevel, Stance shooterStance, Stance targetStance, float shootingDistance)
    {
        
        float hitArea = Mathf.PI * (Mathf.Pow((Mathf.Tan(((Mathf.Deg2Rad*skillLevel.Angle) + shooterStance.Sum) * shooterStance.Multi) * shootingDistance), 2));
        propability = 100 * targetStance.Area / hitArea;
        float random = Convert.ToSingle(UnityEngine.Random.Range(0, 1000)) / 10;

        if (propability > random)
        {
            
            Debug.Log("Hit!: " + random);
            hitCount++;
            return true;
        }
        else
        {
            Debug.Log("Miss: " + random);
            missCount++;
            return false;
            
        }
    }
}
