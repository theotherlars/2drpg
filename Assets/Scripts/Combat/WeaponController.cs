using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponController : MonoBehaviour
{

    public Transform firePoint;
    public BulletController bulletPrefab;
    public bool isFiring = false;
    public float timeBetweenShots;
    private float shotCounter;
    
    // Update is called once per frame
    void Update()
    {


        if (Input.GetButton("Fire1") && !IsPointerOverUIObject())
        {
            isFiring = true;
        }

        //Delay between shots
        if (isFiring)
        {
            shotCounter -= Time.deltaTime;

            if(shotCounter <= 0)
            {
                shotCounter = timeBetweenShots;
                Shoot();
                isFiring = false;
            }
        }
        else
        {
            shotCounter = 0;
        }
    }

    void Shoot()
    {
        //...instantiating the bullet from the firepoint
        BulletController newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation) as BulletController;
    }

    private bool IsPointerOverUIObject()
    {
        var eventDataCurrentPosition = new PointerEventData(EventSystem.current)
        {
            position = new Vector2(Input.mousePosition.x, Input.mousePosition.y)
        };
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
