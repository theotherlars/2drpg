using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectEventListener : MonoBehaviour
{
    public GameObjectEvent gameObjectEvent;
    public GameObjectUnityEvent Response;

    public void OnEnable()
    {
        gameObjectEvent.RegisterListener(this);
    }

    public void OnDisable()
    {
        gameObjectEvent.UnregisterListener(this);
    }

    public void OnEventRaised(GameObject go)
    {
        Response.Invoke(go);
    }
}
