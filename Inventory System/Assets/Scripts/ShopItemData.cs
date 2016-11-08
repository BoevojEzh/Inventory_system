using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;


public class ShopItemData : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public int slotID;
    public Item item;

    private Gold gold;
    private ItemDatabase shop;
    private Inventory inv;
    private ShopTooltip tooltip;
    private ContextMenu contextMenu;
    private ErrorMessage error;

    void Start()
    {
        gold = GameObject.Find("Gold").GetComponent<Gold>();
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        shop = GameObject.Find("Shop").GetComponent<ItemDatabase>();
        tooltip = shop.GetComponent<ShopTooltip>();
        contextMenu = inv.GetComponent<ContextMenu>();
        error = inv.GetComponent<ErrorMessage>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.ShowTooltip(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.HideTooltip();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        contextMenu.HideContextMenu();

        if (eventData.button == PointerEventData.InputButton.Right)
        {



            if (gold.gold >= item.itemPrice)
            {

                if (item.itemQuest && inv.InventoryContains(item) != -1)
                {

                    ItemData data = inv.invSlots[inv.InventoryContains(item)].transform.GetChild(0).GetComponent<ItemData>();
                    if (data.amount >= item.itemMaxQuantity)
                    {
                        error.ShowError("You can`t have this item more than " + item.itemMaxQuantity + "!");
                    }
                    else
                    {
                        BuyItem();
                    }
                }

                else BuyItem();
            }
            else
            {

                error.ShowError("You have not enough money!");
            }


        }

    }

    void BuyItem()
    {
        if (inv.AddItem("Inventory", item.itemID))
            {
            gold.gold -= item.itemPrice;
            gold.transform.GetChild(0).GetComponent<Text>().text = gold.gold.ToString();
        }
    }
}
