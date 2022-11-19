using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HotbarSlot : Slot, IPointerDownHandler
{

    public Image highlight;
    public Button clearButton;

    public Tool specificTool;

    public override void Start()
    {
        base.Start();
        clearButton.enabled = false;
        clearButton.gameObject.SetActive(false);
    }

    public void ClearButton()
    {
        clearButton.gameObject.SetActive(false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (gm.itemInHand == null || gm.itemInHand.itemType == ItemType.Tool)
        {
            base.OnPointerDown(eventData);

            if (itemInSlot == null)
            {
                clearButton.enabled = false;
                clearButton.gameObject.SetActive(false);
                specificTool = null;
                gm.SelectedHotbar();
            }
            else if (itemInSlot != null)
            {
                Tool tempTool = (Tool)itemInSlot;
                specificTool = tempTool;
                specificTool.toolType = tempTool.toolType;
                gm.SelectedHotbar();
                clearButton.gameObject.SetActive(true);
                clearButton.enabled = true;
            }
        }
    }
}
