using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCLootHandler : MonoBehaviour
{
    private CircleCollider2D circleCollider2D;
    UIController uiController;
    bool isCollidingWithPlayer;

    private void OnEnable()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        uiController = FindObjectOfType<UIController>();
        isCollidingWithPlayer = false;
    }

    private void Update()
    {
        if (isCollidingWithPlayer && Input.GetKeyDown(KeyCode.E) && !uiController.isLootWindowOpen)
        {
            uiController.OpenLootWindow(GetComponentInParent<EnemyController>().gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isCollidingWithPlayer = true;
            uiController.ToggleInteractionTextOn();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isCollidingWithPlayer = false;
            uiController.ToggleInteractionTextOn(false);
            uiController.CloseLootWindow();
        }
    }
}
