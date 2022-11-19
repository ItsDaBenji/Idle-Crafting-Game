using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Smeltable", menuName = "Scriptable Objects/Items/Smeltable")]
public class Smeltable : Item
{

    public float timeToSmelt;

    public Item output;
    public int outputCount;

    public int requiredLevel;

}
