using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : Slot, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{

    public Button clearButton;
    public Image highlight;


    public override void Start()
    {
        base.Start();
        highlight.enabled = false;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        if(itemInSlot == null)
        {
            clearButton.enabled = false;
            clearButton.gameObject.SetActive(false);
        }
        else if(itemInSlot != null)
        {
            clearButton.gameObject.SetActive(true);
            clearButton.enabled = true;
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
