using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent",menuName = "Own Menu/Events/New Game Event",order = 31)]
public class GameEvent : ScriptableObject
{
    List<GameEventListener> listeners = new List<GameEventListener>();

    public void Raised()
    {
        for (int i = listeners.Count -1 ; i >= 0; i--)
        {
            listeners[i].OnEventRaised();
        }
    }

    public void RegistereListener(GameEventListener listener)
    {
        listeners.Add(listener);
    }

    public void UnregistereListener(GameEventListener listener)
    {
        listeners.Remove(listener);
    }
}
