using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Objects/Items/Item")]
public class Item : ScriptableObject
{

    public Sprite itemIcon;

    //public Recipe recipe;

    public int maxStackSize;

    public ItemType itemType;

}

public enum ItemType
{
    None,
    Fuel,
    Smeltable,
    Tool,
    Sievable,
    Sieve,
    Furnace
}
