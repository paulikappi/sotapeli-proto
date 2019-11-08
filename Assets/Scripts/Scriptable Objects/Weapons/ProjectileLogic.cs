using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLogic : MonoBehaviour
{
    public Transform shooter;

    void Start()
    {        
        Destroy(gameObject, 2.0f);
    }
    private void OnCollisionEnter(Collision other) 
    {
        Destroy(gameObject);
    }
}
