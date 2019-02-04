using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tooltip : MonoBehaviour
{
    //private Text tooltipText;
    [SerializeField]
    public TextMeshProUGUI itemTitle;
    [SerializeField]
    public TextMeshProUGUI itemCategory; 
    [SerializeField]
    public TextMeshProUGUI itemDescription;
    [SerializeField]
    public TextMeshProUGUI itemAttributes;
    [SerializeField]
    public TextMeshProUGUI itemLevel;

    private Image image;
    

    private void Update()
    {
        itemTitle.enabled = image.enabled;
        itemCategory.enabled = image.enabled;
        itemDescription.enabled = image.enabled;
        itemAttributes.enabled = image.enabled;
        itemLevel.enabled = image.enabled;
    }

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    public void GenerateTooltip(Item_SO item)
    {
        image.enabled = true;

        switch (item.ItemRarity.ToString())
        {
            case "Normal":
            {
                itemTitle.color = Color.white;
                break;
            }
            case "Rare":
            {
                itemTitle.color = Color.blue;
                break;
            }
            case "Epic":
            {
                itemTitle.color = Color.magenta;
                break;
            }
            case "Mythic":
            {
                itemTitle.color = Color.red;
                break;
            }
            case "Legendary":
            {
                itemTitle.color = Color.yellow;
                break;
            }
            default:
            {
                itemTitle.color = Color.white;
                break;
            }
        }

        itemTitle.text = item.ItemTitle;
        itemCategory.text = item.ItemWeaponArmor.ToString();
        itemDescription.text = item.Description + "\n";
        itemLevel.text = "Item Level: " + item.ItemLevel.ToString();

        string attributeText = "";
        if (item.Attributes.Count > 0)
        {
            for (int i = 0; i < item.Attributes.Count; i++)
            {
                attributeText += item.Attributes[i].attribute.attributeName + ": " + item.Attributes[i].amount.ToString() + "\n";
            }
        }
        itemAttributes.text = attributeText;
    }
}
