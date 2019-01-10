using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Warping : MonoBehaviour
{
    public Transform warpTarget;

    void OnTriggerEnter2D(Collider2D other)
    {
        SceneManager.LoadScene(0);
        //Debug.Log("An object collided");
        //other.gameObject.transform.position = warpTarget.position;
        //Camera.main.transform.position = warpTarget.position;
    }
}
