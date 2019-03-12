using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInformation : MonoBehaviour
{
    public NPC npc;
    public string name;

    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private List<NPCLootTable> inventory;

    private void Start()
    {
        name = npc.name;
        maxHealth = npc.maxHealth;
        inventory = npc.lootTable;
    }
}
