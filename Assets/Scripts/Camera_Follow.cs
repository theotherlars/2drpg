using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour {

    // What the camera will follow
    public Transform target;

    // Other camera properties
    Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.2f;

    //
    // Clamping variables
    //
    //enable and Max Y value
    public bool yMaxEnabled;
    public float yMaxValue;

    //enable and Minimum Y Value
    public bool yMinEnabled;
    public float yMinValue;

    //enable and Max X value
    public bool xMaxEnabled;
    public float xMaxValue;

    //enable and Minimum Y Value
    public bool xMinEnabled;
    public float xMinValue;

    void FixedUpdate() 
    {
        Vector3 targetPos = target.position;
        targetPos.z = transform.position.z;

        //
        // Clamps
        //
        // Vertical restrictions
        if (yMaxEnabled && yMinEnabled)
        {
            targetPos.y = Mathf.Clamp(target.position.y, yMinValue, yMaxValue);
        }
        else if (yMinEnabled)
        {
            targetPos.y = Mathf.Clamp(target.position.y,yMinValue,target.position.y);
        }
        else if (yMaxEnabled)
        {
            targetPos.y = Mathf.Clamp(target.position.y, target.position.y, yMaxValue);
        }

        // Horizontal restrictions
        if (xMaxEnabled && xMinEnabled)
        {
            targetPos.x = Mathf.Clamp(target.position.x, xMinValue, xMaxValue);
        }
        else if (xMinEnabled)
        {
            targetPos.x = Mathf.Clamp(target.position.x,xMinValue,target.position.x);
        }
        else if (xMaxEnabled)
        {
            targetPos.x = Mathf.Clamp(target.position.x, target.position.x, xMaxValue);
        }
        

        // Moving the camera    
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
}
