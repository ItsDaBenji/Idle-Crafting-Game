using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    public Tool equipedTool;
    public Tools equipedToolType;

    public GameObject hotbarObject;

    public HotbarSlot[] hotbar;
    public int selectedHotbarIndex;

    public Item itemInHand;
    public Image itemInHandImage;
    public int itemInHandCount;
    public Text itemInHandCountText;

    public bool hasItemInHand;

    [SerializeField] Camera mainCamera;
    public Transform imageTransform;


    public bool sieveActive;
    public bool furnaceActive;

    public Image sieveImage;
    public Image furnaceImage;

    public SieveSystem sieveSystem;
    public SmeltingSystem smeltingSystem;

    private void Start()
    {
        RemoveItemFromHand();
        for (int i = 0; i < hotbar.Length; i++)
        {
            if(i != selectedHotbarIndex)
            {
                hotbar[i].highlight.enabled = false;
            }
        }
    }

    public void HaveItemInHand(Item itemHeld)
    {
        itemInHand = itemHeld;
        itemInHandImage.sprite = itemHeld.itemIcon;
        itemInHandImage.enabled = true;
        hasItemInHand = true;
    }

    public void RemoveItemFromHand()
    {
        itemInHand = null;
        itemInHandCount = 0;
        itemInHandImage.sprite = null;
        itemInHandImage.enabled = false;
        hasItemInHand = false;
        itemInHandCountText.text = "0";
        itemInHandCountText.enabled = false;
    }

    private void Update()
    {
        if (hasItemInHand)
        {
            itemInHandImage.gameObject.transform.position = new Vector3(mainCamera.ScreenToWorldPoint(Input.mousePosition).x, mainCamera.ScreenToWorldPoint(Input.mousePosition).y, 0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedHotbarIndex = 0;
            SelectedHotbar();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedHotbarIndex = 1;
            SelectedHotbar();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedHotbarIndex = 2;
            SelectedHotbar();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedHotbarIndex = 3;
            SelectedHotbar();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            selectedHotbarIndex = 4;
            SelectedHotbar();
        }
    }

    public void SelectedHotbar()
    {
        foreach (HotbarSlot slot in hotbar)
        {
            slot.highlight.enabled = false;
        }
        hotbar[selectedHotbarIndex].highlight.enabled = true;
        if (hotbar[selectedHotbarIndex].itemInSlot != null)
        {
            equipedTool = hotbar[selectedHotbarIndex].specificTool;
            equipedToolType = hotbar[selectedHotbarIndex].specificTool.toolType;
        }
        else
        {
            equipedTool = null;
            equipedToolType = Tools.Hand;
        }
    }


    public void ActivateHotbar()
    {
        hotbarObject.SetActive(true);
    }

    public void UpgradeSieve(Sieve sieve)
    {
        if (!sieveActive)
        {
            sieveSystem.gameObject.SetActive(true);
            sieveSystem.level = sieve.level;
            sieveImage.sprite = itemInHand.itemIcon;
            RemoveItemFromHand();
            sieveActive = true;
        }
        else
        {
            if (sieveSystem.level < sieve.level)
            {
                sieveImage.sprite = itemInHand.itemIcon;
                sieveSystem.level = sieve.level;
            }
            RemoveItemFromHand();
        }
    }

    public void UpgradeFurnace(Furnace furnace)
    {
        if (!furnaceActive)
        {
            smeltingSystem.gameObject.SetActive(true);
            smeltingSystem.level = furnace.level;
            furnaceImage.sprite = itemInHand.itemIcon;
            RemoveItemFromHand();
            furnaceActive = true;
        }
        else
        {
            if (smeltingSystem.level < furnace.level)
            {
                furnaceImage.sprite = itemInHand.itemIcon;
                smeltingSystem.level = furnace.level;
            }
            RemoveItemFromHand();
        }
    }

}
