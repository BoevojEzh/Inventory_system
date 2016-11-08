using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    public Item item;
    public int amount;
    public int slotID;

    private Inventory inv;
    private Tooltip tooltip;
    private Vector2 offset;

    private ContextMenu contextMenu;

    Sprite sprite;

    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        tooltip = inv.GetComponent<Tooltip>();
        contextMenu = inv.GetComponent<ContextMenu>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        contextMenu.HideContextMenu();
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Cursor.SetCursor(Resources.Load<Texture2D>("Item icons/grab_cursor"), Vector2.zero, CursorMode.ForceSoftware);
            Vector2 vecPos = this.transform.position;
            offset = eventData.position - vecPos;
            this.transform.position = eventData.position - offset;
            sprite = inv.invSlots[slotID].transform.GetComponent<Image>().sprite;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Cursor.SetCursor(Resources.Load<Texture2D>("Item icons/drop_cursor"), Vector2.zero, CursorMode.ForceSoftware);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (item != null)
            {

                this.transform.SetParent(this.transform.parent.parent.parent.parent);
                GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (item != null)
            {
                this.transform.position = eventData.position - offset;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(inv.invSlots[slotID].transform);
        this.transform.position = inv.invSlots[slotID].transform.position;
        this.transform.parent.GetComponent<Image>().sprite = sprite;

        GetComponent<CanvasGroup>().blocksRaycasts = true;


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
        if (eventData.button == PointerEventData.InputButton.Right)
        {

            Cursor.SetCursor(Resources.Load<Texture2D>("Item icons/point_cursor"), Vector2.zero, CursorMode.ForceSoftware);

            contextMenu.ShowContextMenu(item, slotID, amount);
            
        }
    }
}
