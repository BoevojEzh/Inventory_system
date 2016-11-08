using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {
    public Item item;
    public string data;
    private GameObject tooltip;

    void Start()
    {
        tooltip = GameObject.Find("Tooltip");
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
        string color = "#000000";

        switch (item.itemType)
        {
            case Item.ItemType.Equipment:
                {
                    color = "#FF352B";
                    break;
                }
            case Item.ItemType.Consumable:
                {
                    color = "#008400";
                    break;
                }
        }

        if (item.itemQuest) quest = "\n<color=#82345D>Quest Item</color>";


        data = "<b> <color=#000000>" + item.itemName + "</color> </b>"
            + "\n<color="+color+">" + item.itemType+"</color>"
            + quest
            + "\n" + item.itemDesc
            + "\n\nPower: <color=#7F0200>" + item.itemPower+"</color>"
            + "\nSpeed: <color=#01C614>" + item.itemSpeed+"</color>"
            + "\n\nSell price: <b><color=#FFC700>" + item.itemPrice / 2 + "</color></b>";
        tooltip.transform.GetChild(0).GetComponent<Text>().text = data;
    }

}
