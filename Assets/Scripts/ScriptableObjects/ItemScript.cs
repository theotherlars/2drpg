using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour {

    [SerializeField]
    private Item_SO item;

    private void Update()
    {
        if (Input.GetKeyDown("mouse 0"))
        {
            print(item.Title);
            print(item.ItemSprite.name);
            print(item.ItemCategory);
            print(item.ItemLevel);
            print(item.IsStackable);
            print(item.ItemRarity);
            print(item.ItemWeaponArmor);
            print(item.ItemID);
            for (int i = 0; i < item.Attributes.Count; i++)
            {
                print(item.Attributes[i].attribute);
                print(item.Attributes[i].amount);
            }
        }
    }

}
