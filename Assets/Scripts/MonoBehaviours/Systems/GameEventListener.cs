﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] GameEventData gameEvent;
    [SerializeField] UnityEvent response;

    private void OnEnable()
    {
        gameEvent.RegisterListener(this);
    }
    private void OnDisable()
    {
        gameEvent.UnRegisterListener(this);
    }
    public void OnEventRaised()
    {
        response.Invoke();
    }
}
