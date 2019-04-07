using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCLootHandler : MonoBehaviour
{
    private CircleCollider2D lootTrigger;
    private EnemyController parent;
    UIController uiController;
    bool isCollidingWithPlayer;

    private void OnEnable()
    {
        parent = GetComponentInParent<EnemyController>();
        lootTrigger = GetComponent<CircleCollider2D>();
        uiController = FindObjectOfType<UIController>();
        isCollidingWithPlayer = false;
    }
    
    private void Update()
    {
        if (isCollidingWithPlayer && Input.GetKeyDown(KeyCode.E) && !uiController.isLootWindowOpen)
        {    
            uiController.OpenLootWindow(parent.gameObject);
        }

        if (parent.availableLoot.Count <= 0 && parent.creditLoot <= 0)
        {
            GetComponentInParent<EnemyAnimationController>().LootParticles(false);
            lootTrigger.enabled = false;
        }
        else if (parent.availableLoot.Count >= 1 || parent.creditLoot > 0)
        {
            GetComponentInParent<EnemyAnimationController>().LootParticles(true);
            lootTrigger.enabled = true;
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
