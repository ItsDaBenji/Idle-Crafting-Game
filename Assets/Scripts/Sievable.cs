using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sievable", menuName = "Scriptable Objects/Items/Sievable")]
public class Sievable : Item
{

    public float timeToSieve;

    public Item output;
    public int outputCount;

    public int requiredLevel;

}
