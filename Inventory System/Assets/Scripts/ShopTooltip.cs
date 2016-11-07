using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopTooltip : MonoBehaviour {
    public Item item;
    public string data;
    private GameObject tooltip;

    void Start()
    {
        tooltip = GameObject.Find("Shop Tooltip");
        tooltip.SetActive(false);
    }

    void Update()
    {
        if (tooltip.activeSelf)
        {
            tooltip.transform.position = Input.mousePosition;
        }
    }

    public void ShowTooltip(Item item)
    {
        this.item = item;
        tooltip.SetActive(true);
        CreateTooltipString();
    }

    public void HideTooltip()
    {
        tooltip.SetActive(false);
    }

    public void CreateTooltipString()
    {
        string quest = "";
        if (item.itemQuest) quest = "\nQuest Item";

        data = "<b>" + item.itemName + "</b>"
            + "\n" + item.itemType
            + quest
            + "\n\n" + item.itemDesc
            + "\n\nPower: " + item.itemPower
            + "\nSpeed: " + item.itemSpeed;
        tooltip.transform.GetChild(0).GetComponent<Text>().text = data;
    }
}
