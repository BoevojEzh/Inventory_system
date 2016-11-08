using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler , IPointerClickHandler{
    public int id;
    private Inventory inv;
    private ContextMenu contextMenu;

    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        contextMenu = inv.GetComponent<ContextMenu>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();

        if (inv.inventory[id].itemID == -1)
        {
            inv.inventory[droppedItem.slotID] = new Item();
            inv.invSlots[droppedItem.slotID].transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Item Icons/free_slot");
            inv.inventory[id] = droppedItem.item;
            droppedItem.slotID = id;

        }
        else if(droppedItem.slotID != id)
        {
            Sprite sprite = inv.invSlots[droppedItem.slotID].transform.GetComponent<Image>().sprite;
            Transform itTrans = this.transform.GetChild(0);
            itTrans.GetComponent<ItemData>().slotID = droppedItem.slotID;
            itTrans.transform.SetParent(inv.invSlots[droppedItem.slotID].transform);
            itTrans.transform.position = inv.invSlots[droppedItem.slotID].transform.position;


            droppedItem.transform.SetParent(this.transform);
            droppedItem.transform.position = this.transform.position;

            inv.inventory[droppedItem.slotID] = itTrans.GetComponent<ItemData>().item;
            inv.invSlots[droppedItem.slotID].transform.GetComponent<Image>().sprite = inv.invSlots[id].transform.GetComponent<Image>().sprite;
            inv.inventory[id] = droppedItem.item;
            inv.invSlots[id].transform.GetComponent<Image>().sprite = sprite;
            droppedItem.slotID = id;



        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            contextMenu.HideContextMenu();
        }
    }
}
