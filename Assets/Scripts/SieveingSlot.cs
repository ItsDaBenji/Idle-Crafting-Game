using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SieveingSlot : Slot, IPointerEnterHandler, IPointerExitHandler
{

    public Image highlight;
    public SieveSystem sieveSystem;

    public override void Start()
    {
        base.Start();
        sieveSystem = FindObjectOfType<SieveSystem>();
        highlight.enabled = false;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if(gm.itemInHand == null)
        {
            base.OnPointerDown(eventData);
        }
        else if(gm.itemInHand.itemType == requiredItemType)
        {
            Sievable sievable = (Sievable)gm.itemInHand;
            if(sievable.requiredLevel <= sieveSystem.level)
            {
                base.OnPointerDown(eventData);
            }
            else
            {
                Debug.Log("NEED A HIGHER LEVEL SIEVE");
            }
        }
        else
        {
            Debug.Log("NEED A SIEVABLE OBJECT");
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
