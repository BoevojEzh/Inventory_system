using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    //public int slotAmount;
    public List<Item> inventory = new List<Item>();
    public List<GameObject> invSlots = new List<GameObject>();

    private ItemDatabase database;

    public int inventorySize;
    public GameObject invSlot;
    public GameObject invItem;
    GameObject Panel;
    GameObject slotPanel;



    void Start () {


        database = GameObject.FindGameObjectWithTag("Item Database").GetComponent<ItemDatabase>();

//Filling the Shop by items` icons

        for (int i=0; i < database.items.Count; i++)
        {
            int idDB = database.items[i].itemID;
            AddItem("Storage", idDB); 
         //   database.shopItem.transform.GetChild(0).GetComponent<Text>().text = database.items[i].itemPrice.ToString();
        }
///////////////////////////////////////////////

//Drawing Inventory slots
        Panel = GameObject.Find("Inventory");
        slotPanel = Panel.transform.FindChild("Inventory panel").gameObject;

        for (int i = 0; i < inventorySize; i++)
        {
            inventory.Add(new Item());
            invSlots.Add(Instantiate(invSlot));
            invSlots[i].GetComponent<Slot>().id = i;
            invSlots[i].transform.SetParent(slotPanel.transform);
        }
//////////////////////////////////////////////



    }

    void Update()
    {
       
    }

   

    public void RemoveItemFromSlot (int id)
    {

        ItemData data = invSlots[id].transform.GetChild(0).GetComponent<ItemData>();
        data.amount--;
        data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();

        if (data.amount == 0)
        {
            invSlots[id].transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Item Icons/free_slot");
            Destroy(invSlots[id].transform.GetChild(0).gameObject);
            inventory[id] = new Item();
        }
                    if (data.amount == 1)
                    {
                        invSlots[id].transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "";


                    }

    }

   public bool AddItem(string destination, int id)
    {
        bool added = false;
        Item itemAdd = ItemFromDBbyID(id);
        switch (destination)
        {
            case "Storage":
                {
                         for (int i = 0; i < database.DBslots.Count; i++)
                            {
                                if (database.DBslots[i].transform.childCount == 0)
                                {
                                    GameObject itemObj = Instantiate(database.shopItem);
                                    itemObj.GetComponent<ShopItemData>().item = itemAdd;
                                    itemObj.GetComponent<ShopItemData>().slotID = i;
                                    itemObj.transform.SetParent(database.DBslots[i].transform);
                                    itemObj.transform.position = Vector2.zero;
                                    itemObj.GetComponent<Image>().sprite = itemAdd.itemSprite;
                                    itemObj.name = itemAdd.itemName;
                                    itemObj.transform.GetChild(0).GetComponent<Text>().text = itemAdd.itemPrice.ToString();


                            added = true;
                            break; 
                                }
                            }

                        break;
                }

            case "Inventory":
                {
                    int cId = InventoryContains(itemAdd);
                    if (itemAdd.itemMaxQuantity > 1 && cId >= 0)
                    {
                        ItemData data = invSlots[cId].transform.GetChild(0).GetComponent<ItemData>();
                        if (data.amount < itemAdd.itemMaxQuantity)
                        {
                            data.amount++;
                            added = true;
                            data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                        }
                        else
                        {
                            added = AddNewInventoryItem(itemAdd);
                        }
                    }
                    else
                    {
                        added = AddNewInventoryItem(itemAdd);
                    }
                    break;
                }
        }
        return added;
    }

   public int InventoryContains(Item item)
    {
        int iID = -1;
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].itemID == item.itemID)
            {
                if (inventory[i].itemMaxQuantity > 1 && !inventory[i].itemQuest)
                {
                    ItemData data = invSlots[i].transform.GetChild(0).GetComponent<ItemData>();
                    if (data.amount < inventory[i].itemMaxQuantity)
                    {
                        iID = i;
                    }
                }
                else iID = i;
            }
        }

        return iID;
    }

   public Item ItemFromDBbyID(int id)
    {
        for(int i =0; i< database.items.Count; i++)
        {
            if(database.items[i].itemID == id)
            {
                return database.items[i];
            }
        }
        return null;
    }

   bool AddNewInventoryItem(Item newItem)
    {
        bool added = false;
        for (int i = 0; i < invSlots.Count; i++)
        {
            if (invSlots[i].transform.childCount == 0)
            {
                GameObject itemObj = Instantiate(invItem);
                itemObj.GetComponent<ItemData>().item = newItem;
                itemObj.GetComponent<ItemData>().slotID = i;
                itemObj.transform.SetParent(invSlots[i].transform);
                itemObj.transform.position = invSlots[i].transform.position;
                itemObj.GetComponent<Image>().sprite = newItem.itemSprite;
                itemObj.name = newItem.itemName;
                inventory[i] = newItem;
                itemObj.GetComponent<ItemData>().amount = 1;
                added = true;
                break;
            }
        }
        return added;
    }

    public void SortedList(string sortType)
    {
        switch (sortType)
        {
            case "Price":
                {
                    for (int i = 0; i < inventory.Count - 1; i++)
                    {
                        int max = i;
                        for (int j = i + 1; j < inventory.Count; j++)
                        {
                            if (inventory[j].itemPrice > inventory[max].itemPrice) 
                            {
                                max = j;
                            }
                        }
                     
                            SwapItems(i, max);

                    }

                    break;
                }
               
            case "Type":
                {
                    for (int i = 0; i < inventory.Count - 1; i++)
                    {
                        int max = i;
                        for (int j = i + 1; j < inventory.Count; j++)
                        {
                            if (ItemTypePriority(inventory[j]) > ItemTypePriority(inventory[max])) 
                            {
                                max = j;
                            }
                        }

                        SwapItems(i, max);

                    }

                    break;

                }
            default:
                {
                    print("No changes");
                    break;
                }
        }


    }

    int ItemTypePriority(Item item)
    {
        int priority = 0;
        switch (item.itemType)
        {
            case Item.ItemType.Equipment:
                {
                    priority = 30; //priority x10 to have more item types in future
                    break;
                }
            case Item.ItemType.Consumable:
                {
                    priority = 20;
                    break;
                }
            case Item.ItemType.Trash:
                {
                    priority = 10;
                    break;
                }
        }
        if (item.itemQuest) priority++;
        if (item.itemID == -1) priority = -1;
        return priority;
    }

    void SwapItems(int a, int b)
    {

        if (invSlots[b].transform.childCount > 0 && invSlots[a].transform.childCount > 0)
        {
            Sprite sprite = invSlots[b].transform.GetComponent<Image>().sprite;

            ItemData swapData = invSlots[b].transform.GetChild(0).GetComponent<ItemData>();
            Transform itTrans = invSlots[a].transform.GetChild(0);
            itTrans.GetComponent<ItemData>().slotID = swapData.slotID;
            itTrans.transform.SetParent(invSlots[swapData.slotID].transform);
            itTrans.transform.position = invSlots[swapData.slotID].transform.position;

            swapData.transform.SetParent(invSlots[a].transform);
            swapData.transform.position = invSlots[a].transform.position;

            invSlots[b].transform.GetComponent<Image>().sprite = invSlots[a].transform.GetComponent<Image>().sprite;
            invSlots[a].transform.GetComponent<Image>().sprite = sprite;

            inventory[swapData.slotID] = itTrans.GetComponent<ItemData>().item;
            inventory[a] = swapData.item;
            swapData.slotID = a;
        }
        else
        {
            if (invSlots[b].transform.childCount > 0)
            {
               ItemData itemBuff = invSlots[b].transform.GetChild(0).GetComponent<ItemData>();

                    inventory[a] = itemBuff.item;
                itemBuff.transform.SetParent(invSlots[a].transform);
                itemBuff.transform.position = invSlots[a].transform.position;

                invSlots[a].transform.GetComponent<Image>().sprite = invSlots[b].transform.GetComponent<Image>().sprite;
                invSlots[b].transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Item Icons/free_slot");

                inventory[itemBuff.slotID] = new Item();

                    itemBuff.slotID = a;


               

            }


        }

    }




}

