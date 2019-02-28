using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameObjectEvent", menuName = "Own Menu/Events/New Game Object Event", order = 30)]
public class GameObjectEvent : ScriptableObject
{
    List<GameObjectEventListener> listeners = new List<GameObjectEventListener>();

    public void Raise(GameObject go)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(go);
        }
    }

    public void RegisterListener(GameObjectEventListener listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(GameObjectEventListener listener)
    {
        listeners.Remove(listener);
    }
}
