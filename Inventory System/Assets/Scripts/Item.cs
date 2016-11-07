using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class Item
{
    public string       itemName;
    public int          itemID;
    public string       itemDesc;
    public Sprite       itemSprite;
    public int          itemPower;
    public int          itemSpeed;
    public ItemType     itemType;
    public int          itemMaxQuantity;
    public int          itemPrice;
    public bool         itemQuest;


    public enum ItemType
    {
        Equipment,
        Consumable,
        Trash
    }


    public Item(string name, int ID, string desc, int power, int speed, string type, int maxQuantity, int price, bool quest)
    {
        itemName = name;
        itemID = ID;
        itemDesc = desc;
        itemSprite = Resources.Load<Sprite>("Item Icons/" + name);
        itemPower = power;
        itemSpeed = speed;
        itemMaxQuantity = maxQuantity;
        itemPrice = price;
        itemQuest = quest;

        itemType = (ItemType)Enum.Parse(typeof(ItemType),type);

    }

    public Item()
    {
        itemID = -1;
        itemPrice = -1;
    }

}
