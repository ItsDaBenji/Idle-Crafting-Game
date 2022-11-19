using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerDownHandler
{

    public bool canPlaceItemsIn;

    public ItemType requiredItemType;

    [HideInInspector] public GameManager gm;

    public Item itemInSlot;
    public Image slotIcon;
    public int itemInSlotCount;
    public Text slotCount;


    public virtual void Start()
    {
        gm = GameManager.instance;
        ClearSlot();
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //If the player DOESN'T have an item in hand
            if (gm.itemInHand == null)
            {
                //If the slot ISN'T empty
                if (itemInSlot != null)
                {
                    //Update the hand accordingly
                    gm.itemInHand = itemInSlot;
                    gm.itemInHandCount = itemInSlotCount;
                    gm.itemInHandCountText.enabled = true;
                    gm.itemInHandCountText.text = gm.itemInHandCount.ToString();
                    gm.itemInHandImage.enabled = true;
                    gm.itemInHandImage.sprite = gm.itemInHand.itemIcon;
                    gm.hasItemInHand = true;

                    if(gm.itemInHand.itemType == ItemType.Tool)
                    {
                        gm.ActivateHotbar();
                    }
                    else if(gm.itemInHand.itemType == ItemType.Sieve)
                    {
                        gm.UpgradeSieve((Sieve)gm.itemInHand);
                    }
                    else if(gm.itemInHand.itemType == ItemType.Furnace)
                    {
                        gm.UpgradeFurnace((Furnace)gm.itemInHand);
                    }

                    //Update the slot accordingly
                    ClearSlot();
                }
            }


            //Otherwise, if the player DOES have an item in hand
            else if (gm.itemInHand != null)
            {
                if (canPlaceItemsIn)
                {
                    //If the slot IS empty
                    if (itemInSlot == null)
                    {
                        //Update the slot accordingly
                        itemInSlot = gm.itemInHand;
                        itemInSlotCount = gm.itemInHandCount;
                        slotCount.enabled = true;
                        slotCount.text = itemInSlotCount.ToString();
                        slotIcon.enabled = true;
                        slotIcon.sprite = itemInSlot.itemIcon;

                        //Update the hand accordingly
                        ClearHand();

                    }
                    //Otherwise, if the item in the slot is the same as the item in hand
                    else if (itemInSlot == gm.itemInHand)
                    {
                        //If the combined count of the items in hand and the items in the slot are less than the maxStackSize
                        if (itemInSlotCount + gm.itemInHandCount <= itemInSlot.maxStackSize)
                        {
                            //Update the slot accordingly
                            itemInSlotCount += gm.itemInHandCount;
                            slotCount.text = itemInSlotCount.ToString();

                            //Update the hand accordingly
                            ClearHand();

                        }
                        //Otherwise, if the combined count of the items in hand and the items in the slot are greater than the maxStackSize
                        else
                        {

                            int diff = itemInSlot.maxStackSize - itemInSlotCount;

                            gm.itemInHandCount -= diff;
                            gm.itemInHandCountText.text = gm.itemInHandCount.ToString();

                            itemInSlotCount = itemInSlot.maxStackSize;
                            slotCount.text = itemInSlotCount.ToString();
                        }
                    }
                }
                else
                {
                    if(itemInSlot == gm.itemInHand)
                    {
                        if(itemInSlotCount + gm.itemInHandCount <= itemInSlot.maxStackSize)
                        {
                            gm.itemInHandCount += itemInSlotCount;
                            gm.itemInHandCountText.text = gm.itemInHandCount.ToString();

                            ClearSlot();
                        }
                    }
                }
            }
        }

        else if(eventData.button == PointerEventData.InputButton.Right)
        {
            //If the player DOESN'T have an item in hand
            if(gm.itemInHand == null)
            {
                //If the slot has an item in it
                if(itemInSlot != null)
                {
                    if (itemInSlotCount == 1)
                    {
                        gm.itemInHand = itemInSlot;
                        gm.itemInHandCount = 1;
                        gm.itemInHandCountText.enabled = true;
                        gm.itemInHandCountText.text = gm.itemInHandCount.ToString();
                        gm.itemInHandImage.enabled = true;
                        gm.itemInHandImage.sprite = gm.itemInHand.itemIcon;
                        gm.hasItemInHand = true;

                        ClearSlot();
                    }
                    else
                    {
                        //Figure out half the value of the slot
                        int halfCount = Mathf.CeilToInt(itemInSlotCount / 2);

                        //Add that many to the players hand
                        gm.itemInHand = itemInSlot;
                        gm.itemInHandCount = halfCount;
                        gm.itemInHandCountText.enabled = true;
                        gm.itemInHandCountText.text = gm.itemInHandCount.ToString();
                        gm.itemInHandImage.enabled = true;
                        gm.itemInHandImage.sprite = gm.itemInHand.itemIcon;
                        gm.hasItemInHand = true;

                        //Take that many away from the slot
                        itemInSlotCount -= halfCount;
                        slotCount.text = itemInSlotCount.ToString();
                    }

                }
            }
            //Otherwise, if the player DOES have an item in hand
            else if(gm.itemInHand != null)
            {
                if (canPlaceItemsIn)
                {
                    //If the slot has the same item as the one in the players hand
                    if (itemInSlot == gm.itemInHand)
                    {
                        if (itemInSlotCount < itemInSlot.maxStackSize)
                        {
                            //Add one to the slot...
                            itemInSlotCount++;
                            slotCount.text = itemInSlotCount.ToString();

                            //...and remove one from the players hand
                            gm.itemInHandCount--;
                            gm.itemInHandCountText.text = gm.itemInHandCount.ToString();
                            if (gm.itemInHandCount <= 0)
                            {
                                ClearHand();
                            }
                        }
                    }
                    //Otherwise, if the slot is empty
                    else if (itemInSlot == null)
                    {
                        //Give the slot one item
                        itemInSlot = gm.itemInHand;
                        itemInSlotCount = 1;
                        slotCount.enabled = true;
                        slotCount.text = itemInSlotCount.ToString();
                        slotIcon.enabled = true;
                        slotIcon.sprite = itemInSlot.itemIcon;

                        gm.itemInHandCount--;
                        gm.itemInHandCountText.text = gm.itemInHandCount.ToString();

                        if (gm.itemInHandCount <= 0)
                        {
                            ClearHand();
                        }

                    }
                }
            }
        }
    }

    public virtual void ClearHand()
    {
        gm.itemInHand = null;
        gm.itemInHandCount = 0;
        gm.itemInHandCountText.text = "0";
        gm.itemInHandCountText.enabled = false;
        gm.itemInHandImage.sprite = null;
        gm.itemInHandImage.enabled = false;
        gm.hasItemInHand = false;
    }

    public virtual void ClearSlot()
    {
        itemInSlot = null;
        itemInSlotCount = 0;
        slotCount.text = "0";
        slotCount.enabled = false;
        slotIcon.sprite = null;
        slotIcon.enabled = false;
    }

}
