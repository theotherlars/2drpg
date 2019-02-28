using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent gameEvent;
    public UnityEvent Response;

    public void OnEnable()
    {
        gameEvent.RegistereListener(this);
    }

    public void OnDisable()
    {
        gameEvent.UnregistereListener(this);
    }

    public void OnEventRaised()
    {
        Response.Invoke();
    }
}
