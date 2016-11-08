using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ContextMenu : MonoBehaviour {

    private Item item;
    private int slotID, amount;
    private Gold gold;
    private Health health;
    private Speed speed;
    private GameObject contextMenu, amountField;
    private Inventory inv;
    private ErrorMessage error;

    bool equipped;
    bool sell = false;

	void Start()
    {
        gold = GameObject.Find("Gold").GetComponent<Gold>();
        health = GameObject.Find("Health").GetComponent<Health>();
        speed = GameObject.Find("Speed").GetComponent<Speed>();
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        error = inv.GetComponent<ErrorMessage>();
        contextMenu = GameObject.Find("Context Menu");

        amountField = GameObject.Find("Amount Panel");

       contextMenu.SetActive(false);

        amountField.SetActive(false);
	}

    public void ShowContextMenu(Item contextItem, int contextSlotID, int contextAmount)
    {
        item = contextItem;

        slotID = contextSlotID;
        amount = contextAmount;
        contextMenu.SetActive(true);
        contextMenu.transform.position = Input.mousePosition;

        contextMenu.transform.GetChild(0).GetComponent<Button>().gameObject.SetActive(true);

        switch (item.itemType)
        {
            case Item.ItemType.Equipment:
                {  ///sorry for the next few lines. Just didnt want to create a Player`s character sheet for this demo project. So its just illusion of equipment action. Sorry for that crap again.
                    if (inv.invSlots[slotID].transform.GetComponent<Image>().sprite != Resources.Load<Sprite>("Item Icons/equipped_slot"))
                    {
                        contextMenu.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Equip";
                        equipped = false;
                    }
                    else
                    {
                        contextMenu.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Unequip";
                        equipped = true;
                    }
                    break;
                }
            case Item.ItemType.Consumable:
                {
                    contextMenu.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Use";
                    break;
                }
            default:
                {
                    contextMenu.transform.GetChild(0).GetComponent<Button>().gameObject.SetActive(false);
                    break;
                }
        }
        

    }

    public void HideContextMenu()
    {
        Cursor.SetCursor(Resources.Load<Texture2D>("Item icons/drop_cursor"), Vector2.zero, CursorMode.ForceSoftware);
        contextMenu.SetActive(false);
        amountField.SetActive(false);
    }

    public void Use()
    {

        HideContextMenu();

        if (item.itemType == Item.ItemType.Consumable)
        {
            speed.speed -= item.itemSpeed; //cause it`s not just speed, it`s actually a delay before attack
            health.health += item.itemPower;
            if (health.health > 100) health.health = 100;

            speed.transform.GetChild(0).GetComponent<Text>().text = speed.speed.ToString();
            health.transform.GetChild(0).GetComponent<Text>().text = health.health.ToString();

            inv.RemoveItemFromSlot(slotID);

        }
        if (item.itemType == Item.ItemType.Equipment)
        {
            if (equipped)
            {
                speed.speed -= item.itemSpeed; //cause it`s not just speed, it`s actually a delay before attack
                inv.invSlots[slotID].transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Item Icons/free_slot");
            }
            else
            {

                for (int i = 0; i < inv.inventory.Count; i++)
                {

                    if (inv.inventory[i].itemID == item.itemID && inv.invSlots[i].transform.GetComponent<Image>().sprite == Resources.Load<Sprite>("Item Icons/equipped_slot")) 
                    {
                        speed.speed -= item.itemSpeed;
                        inv.invSlots[i].transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Item Icons/free_slot");
         
                    }                   

                }

                speed.speed += item.itemSpeed; //cause it`s not just speed, it`s actually a delay before attack
                inv.invSlots[slotID].transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Item Icons/equipped_slot");
            }

            speed.transform.GetChild(0).GetComponent<Text>().text = speed.speed.ToString();


        }
    }

     void Sell(int sellAmount)
    {

            for (int i = 0; i < sellAmount; i++)
            {
                gold.gold += item.itemPrice / 2;
                gold.transform.GetChild(0).GetComponent<Text>().text = gold.gold.ToString();
                inv.RemoveItemFromSlot(slotID);
            }

    }

     void Drop(int dropAmount)
    {

            for (int i = 0; i<dropAmount; i++)
            {
                inv.RemoveItemFromSlot(slotID);
            }

    }


     void ShowAmountField(int a)
    {
        amountField.SetActive(true);
        amountField.transform.position = Input.mousePosition;
        amountField.transform.GetChild(0).GetComponent<InputField>().text = a.ToString();

    }

    void HideAmountField()
    {
        amountField.SetActive(false);
    }

    public void SellButton()
    {
        HideContextMenu();
        if (!item.itemQuest)
        {
            if (amount > 1)
            {
                sell = true;
                ShowAmountField(amount);
            }
            else Sell(amount);
        }
        else error.ShowError("You can`t sell quest item!");

    }

    public void DropButton()
    {
        HideContextMenu();
        if (!item.itemQuest)
        {
            if (amount > 1)
            {
                sell = false;
                ShowAmountField(amount);
            }
            else Drop(amount);
        }
        else error.ShowError("You can`t drop quest item!");
    }

   public void OkButton()
    {
        int okAmount = int.Parse(amountField.transform.GetChild(0).GetComponent<InputField>().text);

        if (okAmount <= amount)
        {
            if (sell) Sell(okAmount);
            else Drop(okAmount);
            HideAmountField();
            Cursor.SetCursor(Resources.Load<Texture2D>("Item icons/drop_cursor"), Vector2.zero, CursorMode.ForceSoftware);

        }
        else
        {
            amountField.transform.GetChild(0).GetComponent<InputField>().text = amount.ToString();
        }


    }
}
