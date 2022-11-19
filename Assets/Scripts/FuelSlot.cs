using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FuelSlot : Slot, IPointerEnterHandler, IPointerExitHandler
{

    public Image highlight;
    public SmeltingSystem smeltingSystem;

    public override void Start()
    {
        base.Start();
        smeltingSystem = FindObjectOfType<SmeltingSystem>();
        highlight.enabled = false;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (gm.itemInHand == null || gm.itemInHand.itemType == requiredItemType)
        {
            Fuel sievable = (Fuel)gm.itemInHand;

            base.OnPointerDown(eventData);
        }
        else
        {
            Debug.Log("NEED A FUEL OBJECT");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        highlight.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlight.enabled = false;
    }
}
