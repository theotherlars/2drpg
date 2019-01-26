using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpingWithinScene : MonoBehaviour
{
    public Transform warpTarget;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.transform.position = warpTarget.transform.position;
        }

        //Debug.Log("An object collided");
        //other.gameObject.transform.position = warpTarget.position;
        //Camera.main.transform.position = warpTarget.position;
    }
}
