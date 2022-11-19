using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "Scriptable Objects/ Wave")]
public class Wave : ScriptableObject
{

    public WaveBlock[] blocksInWave;
    public int blockCount;

}

[System.Serializable]
public class WaveBlock
{
    public Block block;
    public int weight;
}
