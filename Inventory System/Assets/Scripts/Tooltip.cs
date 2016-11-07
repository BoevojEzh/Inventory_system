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
        if (item.itemQuest) quest = "\nQuest Item";

        data = "<b> <color=#000000>" + item.itemName + "</color> </b>"
            + "\n" + item.itemType
            + quest
            + "\n\n" + item.itemDesc
            + "\n\nPower: " + item.itemPower
            + "\nSpeed: " + item.itemSpeed
            + "\n\nSell price: <b><color=#DB7400>" + item.itemPrice / 2 + "</color></b>";
        tooltip.transform.GetChild(0).GetComponent<Text>().text = data;
    }

}
