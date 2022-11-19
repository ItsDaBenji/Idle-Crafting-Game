using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SieveSystem : MonoBehaviour
{

    public Slot inputSlot;
    public Slot outputSlot;

    public int level;

    [SerializeField] float time;
    [SerializeField] bool isSieving;

    [SerializeField] RectTransform sieveMask;
    [SerializeField] Vector3 startPos;
    [SerializeField] Vector3 endPos;


    void Update()
    {
        if(inputSlot.itemInSlot != null)
        {
            Sievable sievable = (Sievable)inputSlot.itemInSlot;
            if (!isSieving)
            {
                time = sievable.timeToSieve;
                isSieving = true;
            }
            time -= Time.deltaTime * level;

            //Move the mask from the start pos to the end pos
            sieveMask.localPosition = Vector3.Lerp(sieveMask.localPosition, endPos, Time.deltaTime / time);

            if(time <= 0)
            {
                inputSlot.itemInSlotCount--;
                inputSlot.slotCount.text = inputSlot.itemInSlotCount.ToString();
                time = sievable.timeToSieve;
                if(inputSlot.itemInSlotCount <= 0)
                {
                    inputSlot.ClearSlot();
                }

                if (outputSlot.itemInSlot == null)
                {
                    outputSlot.itemInSlot = sievable.output;
                    outputSlot.itemInSlotCount = sievable.outputCount;
                    outputSlot.slotIcon.enabled = true;
                    outputSlot.slotIcon.sprite = sievable.output.itemIcon;
                    outputSlot.slotCount.enabled = true;
                    outputSlot.slotCount.text = sievable.outputCount.ToString();
                }
                else
                {
                    outputSlot.itemInSlotCount++;
                    outputSlot.slotCount.text = outputSlot.itemInSlotCount.ToString();
                }
                sieveMask.localPosition = startPos;
                isSieving = false;
            }
        }
    }
}
