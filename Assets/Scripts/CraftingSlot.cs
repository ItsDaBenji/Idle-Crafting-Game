using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftingSlot : Slot, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{

    CraftingSystem craftingSystem;

    public Image highlight;

    public override void Start()
    {
        base.Start();
        craftingSystem = FindObjectOfType<CraftingSystem>();
        highlight.enabled = false;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        craftingSystem.TryCraftItem();
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
