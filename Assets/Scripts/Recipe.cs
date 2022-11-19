using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Scriptable Objects/Recipe")]
public class Recipe : ScriptableObject
{

    public Item[] requiredItems;

    public Item itemCrafted;

    public int itemsCrafted;
}
