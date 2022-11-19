using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{

    [SerializeField] Recipe[] recipes;
    [SerializeField] Slot[] slots;

    [SerializeField] Slot craftedSlot;

    private void Start()
    {

    }

    public void TryCraftItem()
    {
        bool canCraftItem = false;
        Recipe recipeAttempt = null;

        foreach (Recipe recipe in recipes)
        {
            recipeAttempt = recipe;
            canCraftItem = true;
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].itemInSlot != recipe.requiredItems[i])
                {
                    canCraftItem = false;
                }
            }
            if (canCraftItem)
            {
                craftedSlot.itemInSlot = recipeAttempt.itemCrafted;
                craftedSlot.slotIcon.sprite = recipeAttempt.itemCrafted.itemIcon;
                craftedSlot.slotIcon.enabled = true;
                craftedSlot.itemInSlotCount = recipeAttempt.itemsCrafted;
                craftedSlot.slotCount.enabled = true;
                craftedSlot.slotCount.text = craftedSlot.itemInSlotCount.ToString();
                break;
            }
            else
            {
                craftedSlot.ClearSlot();
            }
        }
    }

    public void RemoveCraftingElements()
    {
        foreach(Slot slot in slots)
        {
            if(slot.itemInSlot != null)
            {
                slot.itemInSlotCount--;
                slot.slotCount.text = slot.itemInSlotCount.ToString();
                if(slot.itemInSlotCount <= 0)
                {
                    slot.ClearSlot();
                }
            }
        }
        TryCraftItem();
    }

}
