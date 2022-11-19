using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftedSlot : Slot, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{

    CraftingSystem craftingSystem;

    public Image highlight;

    public override void Start()
    {
        base.Start();
        craftingSystem = FindObjectOfType<CraftingSystem>();
        highlight.enabled = false;
        ResetCraftedSlot();
    }

    public void ResetCraftedSlot()
    {
        slotIcon.sprite = null;
        slotIcon.enabled = false;
        itemInSlot = null;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (itemInSlot != null)
        {
            if (gm.itemInHand == null)
            {
                base.OnPointerDown(eventData);
                craftingSystem.RemoveCraftingElements();
                if(itemInSlot == null)
                    highlight.enabled = false;
            }
            else if(gm.itemInHand == itemInSlot)
            {
                if(gm.itemInHandCount + itemInSlotCount <= itemInSlot.maxStackSize)
                {
                    base.OnPointerDown(eventData);
                    craftingSystem.RemoveCraftingElements();
                    if (itemInSlot == null)
                        highlight.enabled = false;
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemInSlot != null)
        {
            highlight.enabled = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlight.enabled = false;
    }

}
