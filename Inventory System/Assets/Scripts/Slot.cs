using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

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
            inv.inventory[id] = droppedItem.item;
            droppedItem.slotID = id;
        }
        else if(droppedItem.slotID != id)
        {
            Transform itTrans = this.transform.GetChild(0);
            itTrans.GetComponent<ItemData>().slotID = droppedItem.slotID;
            itTrans.transform.SetParent(inv.invSlots[droppedItem.slotID].transform);
            itTrans.transform.position = inv.invSlots[droppedItem.slotID].transform.position;

            
            droppedItem.transform.SetParent(this.transform);
            droppedItem.transform.position = this.transform.position;

            inv.inventory[droppedItem.slotID] = itTrans.GetComponent<ItemData>().item;
            inv.inventory[id] = droppedItem.item;
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
