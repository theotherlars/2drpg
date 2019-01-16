using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    //private Text tooltipText;
    [SerializeField]
    public Text itemTitle;
    [SerializeField]
    public Text itemCategory; 
    [SerializeField]
    public Text itemDescription;
    [SerializeField]
    public Text itemAttributes;
    [SerializeField]
    public Text itemLevel;

    private Image image;
    // Start is called before the first frame update
    void Start()
    {
        //tooltipText = GetComponentInChildren<Text>();
        image = GetComponent<Image>();
        gameObject.SetActive(false);
    }

    public void GenerateTooltip(Item_SO item)
    {
        image.enabled = true;
        itemTitle.text = item.Title;
        itemCategory.text = item.ItemCategory.ToString();
        itemDescription.text = item.Description;
        itemLevel.text = item.ItemLevel.ToString();

        string attributeText = "";
        if (item.Attributes.Count > 0)
        {
            for (int i = 0; i < item.Attributes.Count; i++)
            {
                attributeText += item.Attributes[i].attribute.name.ToString() + ": " + item.Attributes[0].amount.ToString() + "\n";
            }
        }
        itemAttributes.text = attributeText;

        gameObject.SetActive(true);

        /*
        image.enabled = true;
        string statText = "";

        if (item.Attributes.Count > 0)
        {
            foreach (var attribute in item.Attributes)
            {
                statText += attribute.attribute.name.ToString() + ": " + attribute.amount.ToString() + "\n";
            }
        }
        string tooltip = string.Format("<b>{0}</b>\n{1}\n\n<b>{2}</b>\n Item level: {3}", item.Title, item.Description,statText, item.ItemLevel);
        //tooltipText.text = tooltip;
        //tooltipText.gameObject.SetActive(true);

        */
    }
}
