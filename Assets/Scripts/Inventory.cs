using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public static Inventory instance;

    private void Awake()
    {
        instance = this;
    }

    Slot[] inventorySlots;

    public void Start()
    {
        gameObject.SetActive(false);
        inventorySlots = GetComponentsInChildren<Slot>();
        ClearInventory();
    }

    public void ClearInventory()
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            ClearSlot(slot);
        }
    }

    public void ClearSlot(InventorySlot slot)
    {
        slot.itemInSlot = null;
        slot.itemInSlotCount = 0;

        slot.slotIcon.sprite = null;
        slot.slotIcon.enabled = false;

        slot.slotCount.text = "0";
        slot.slotCount.enabled = false;

        slot.clearButton.gameObject.SetActive(false);
    }

    public void SearchForSlot(Item item)
    {
        bool foundItemOfType = false;
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.itemInSlot == item)
            {
                if (slot.itemInSlotCount < item.maxStackSize)
                {
                    AddItem(slot, item);

                    foundItemOfType = true;

                    break;
                }
            }
        }
        if (!foundItemOfType)
        {
            foreach (InventorySlot slot in inventorySlots)
            {
                if (slot.itemInSlot == null)
                {
                    AddItem(slot, item);

                    break;
                }
            }
        }
    }

    public void AddItem(InventorySlot slot, Item item)
    {
        slot.itemInSlot = item;
        slot.itemInSlotCount++;

        slot.slotIcon.sprite = item.itemIcon;
        slot.slotIcon.enabled = true;

        slot.slotCount.text = slot.itemInSlotCount.ToString();
        slot.slotCount.enabled = true;

        slot.clearButton.gameObject.SetActive(true);
        slot.clearButton.enabled = true;
    }
}


