using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteBlock : MonoBehaviour
{

    bool isPlaying = false;


    [SerializeField] Wave[] waves;
    [SerializeField] Wave currentWave;
    int waveIndex = 0;
    Block currentBlock;
    int waveBlockCount;
    public int totalBlockCount;

    [SerializeField] GameObject inventory;
    [SerializeField] GameObject craftingPanel;
    [SerializeField] GameObject craftingSlot;
    [SerializeField] GameObject blueprintPanel;

    [SerializeField] GameObject[] blueprints;

    [SerializeField] int toolSpeed = 1;

    #region Specific block variables
    float startBreakTime;
    [SerializeField] float breakTime;
    [SerializeField] bool restoringBlock;
    float time;

    SpriteRenderer spriteRenderer;

    List<Drops> blockLootTable = new List<Drops>();
    #endregion

    [SerializeField] ParticleSystem breakingParticle;
    [SerializeField] ParticleSystem burstParticle;

    GameManager gm;

    Animator animator;


    public void StartGame()
    {
        animator.SetTrigger("StartGame");
        isPlaying = true;
        StartCoroutine(StopAnimator());
    }

    private IEnumerator StopAnimator()
    {
        yield return new WaitForSeconds(1);
        animator.enabled = false;
    }


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        gm = GameManager.instance;

        SpawnNewBlock();
    }

    private void Update()
    {
        if (isPlaying)
        {
            //Set the scale amount that the block will be increasing or decreasing over the destruction time
            float scale = -Mathf.Sin(breakTime / startBreakTime) + 1.85f;

            //As the player holds the mouse down
            if (Input.GetMouseButton(0))
            {

                if (gm.equipedToolType == currentBlock.preferedTool)
                {
                    toolSpeed = gm.equipedTool.toolTier;
                }
                else
                {
                    toolSpeed = 1;
                }
                restoringBlock = false;
                time += Time.deltaTime * toolSpeed;
                breakTime -= Time.deltaTime * toolSpeed;
                transform.localScale = new Vector3(scale, scale, scale);

                if (breakTime <= 0)
                {
                    if (totalBlockCount == 0)
                    {
                        SummonInventory();
                    }
                    CollectLoot();
                    SpawnParticles();
                    CheckNewWave();
                    SpawnNewBlock();
                }
            }

            //When the player releases the mouse
            if (Input.GetMouseButtonUp(0))
            {
                restoringBlock = true;
            }

            //Start undoing all the destruction process. Needs to be in a separate area because GetMouseButtonUp is only called for the frame it is released
            if (restoringBlock)
            {
                time -= Time.deltaTime;
                breakTime += Time.deltaTime;
                transform.localScale = new Vector3(scale, scale, scale);
                if (breakTime > startBreakTime)
                {
                    time = 0;
                    restoringBlock = false;
                    transform.localScale = Vector3.one;
                    breakTime = startBreakTime;
                }
            }
        }
    }

    Sprite lootSprite;

    private void SummonInventory()
    {
        inventory.SetActive(true);
        craftingPanel.SetActive(true);
        craftingSlot.SetActive(true);
    }

    //Deals with adding the items dropped by the blocks to the inventory
    private void CollectLoot()
    {
        bool hasUniqueItem = false;
        bool hasDroppedItem = false;
        foreach (Drops drop in blockLootTable)
        {
            if (drop.uniqueDrop)
            {
                if (gm.equipedToolType  == drop.requiredTool)
                {
                    float rand = Random.Range(0f, 1f);
                    if (rand <= drop.dropPercentage)
                    {
                        Inventory.instance.SearchForSlot(drop.item);
                        lootSprite = drop.item.itemIcon;
                        hasUniqueItem = true;
                        hasDroppedItem = true;
                    }
                }
            }
        }
        if (!hasUniqueItem)
        {
            foreach (Drops drop in blockLootTable)
            {
                if (gm.equipedToolType == drop.requiredTool || drop.requiredTool == Tools.None)
                {
                    float rand = Random.Range(0f, 1f);
                    if (rand <= drop.dropPercentage)
                    {
                        Inventory.instance.SearchForSlot(drop.item);
                        lootSprite = drop.item.itemIcon;
                        hasDroppedItem = true;
                    }
                }
            }
        }

        if (!hasDroppedItem)
        {
            lootSprite = null;
        }
    }

    //Plays the particle system of the blocks breaking
    private void SpawnParticles()
    {
        var main = burstParticle.main;
        var texture = breakingParticle.textureSheetAnimation;

        main.startColor = new ParticleSystem.MinMaxGradient(currentBlock.colorMin, currentBlock.colorMax);

        if (lootSprite != null)
        {
            texture.SetSprite(0, lootSprite);
            breakingParticle.Play();
        }
        burstParticle.Play();
    }

    //Checks to see if you are allowed to move onto the next wave
    private void CheckNewWave()
    {
        waveBlockCount++;
        totalBlockCount++;
        if(waveBlockCount >= currentWave.blockCount)
        {
            waveBlockCount = 0;
            if(waveIndex == 0)
            {
                blueprintPanel.SetActive(true);
            }
            blueprints[waveIndex].SetActive(true);
            waveIndex++;
            currentWave = waves[waveIndex];
        }
    }

    //Spawns a new block and sets all the values back to their default when the old block is destroyed
    private void SpawnNewBlock()
    {
        //Choosing the block to be spawned
        List<Block> blocksInWave = new List<Block>();

        for (int i = 0; i < currentWave.blocksInWave.Length; i++)
        {
            for (int j = 0; j < currentWave.blocksInWave[i].weight; j++)
            {
                blocksInWave.Add(currentWave.blocksInWave[i].block);
            }
        }

        int rand = Random.Range(0, blocksInWave.Count);

        currentBlock = blocksInWave[rand];


        //Assigning all the data to that of the selected block
        startBreakTime = currentBlock.baseBreakTime;
        breakTime = startBreakTime;

        spriteRenderer.sprite = currentBlock.blockIcon;

        transform.localScale = Vector3.one;
        time = 0;

        blockLootTable.Clear();
        foreach(Drops drop in currentBlock.lootTable)
        {
            blockLootTable.Add(drop);
        }
    }

}
