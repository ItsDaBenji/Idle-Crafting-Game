using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmeltingSystem : MonoBehaviour
{

    public Slot inputSlot;
    public Slot outputSlot;
    public Slot fuelSlot;

    public int level;

    [SerializeField] float smeltTime;
    [SerializeField] float burnTime;
    [SerializeField] bool isSmelting;

    [SerializeField] RectTransform smeltMask;
    [SerializeField] Vector3 startPos;
    [SerializeField] Vector3 endPos;

    void Update()
    {
        if (inputSlot.itemInSlot != null)
        {
            if (fuelSlot.itemInSlot != null)
            {
                Smeltable smeltable = (Smeltable)inputSlot.itemInSlot;
                Fuel fuel = (Fuel)fuelSlot.itemInSlot;
                if (!isSmelting)
                {
                    smeltTime = smeltable.timeToSmelt;
                    burnTime = fuel.burnTime;
                    isSmelting = true;
                }
                smeltTime -= Time.deltaTime * level;
                burnTime -= Time.deltaTime;

                //Move the mask from the start pos to the end pos
                smeltMask.localPosition = Vector3.Lerp(smeltMask.localPosition, endPos, Time.deltaTime / smeltTime);

                if (smeltTime <= 0)
                {
                    inputSlot.itemInSlotCount--;
                    inputSlot.slotCount.text = inputSlot.itemInSlotCount.ToString();
                    smeltTime = smeltable.timeToSmelt;
                    if (inputSlot.itemInSlotCount <= 0)
                    {
                        inputSlot.ClearSlot();
                    }

                    if (outputSlot.itemInSlot == null)
                    {
                        outputSlot.itemInSlot = smeltable.output;
                        outputSlot.itemInSlotCount = smeltable.outputCount;
                        outputSlot.slotIcon.enabled = true;
                        outputSlot.slotIcon.sprite = smeltable.output.itemIcon;
                        outputSlot.slotCount.enabled = true;
                        outputSlot.slotCount.text = smeltable.outputCount.ToString();
                    }
                    else
                    {
                        outputSlot.itemInSlotCount++;
                        outputSlot.slotCount.text = outputSlot.itemInSlotCount.ToString();
                    }

                    smeltMask.localPosition = startPos;
                    isSmelting = false;
                }
                if(burnTime <= 0)
                {
                    fuelSlot.itemInSlotCount--;
                    fuelSlot.slotCount.text = fuelSlot.itemInSlotCount.ToString();
                    burnTime = fuel.burnTime;
                    if(fuelSlot.itemInSlotCount <= 0)
                    {
                        fuelSlot.ClearSlot();
                    }
                }
            }
        }
    }

}
