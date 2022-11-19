using UnityEngine;

[CreateAssetMenu(fileName = "New Block", menuName = "Scriptable Objects/Block")]
public class Block : ScriptableObject
{

    public float baseBreakTime;

    public Sprite blockIcon;

    public Drops[] lootTable;

    public Tools preferedTool;

    public Color colorMin;
    public Color colorMax;

}

[System.Serializable]
public class Drops
{
    public Item item;

    [Range(0, 1)] public float dropPercentage;

    public Tools requiredTool;

    public bool uniqueDrop;
}

[System.Serializable]
public enum Tools
{
    None,
    Hand,
    Axe,
    Pickaxe,
    Hammer
}
