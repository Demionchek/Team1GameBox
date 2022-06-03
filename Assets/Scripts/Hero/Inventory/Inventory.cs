using System;
using UnityEngine;
using StarterAssets;

public enum ItemType
{
    HealthPotion = 0,
    EnergyPotion = 1
}

public class Inventory : MonoBehaviour
{
    [SerializeField] private Sprite commonInventorySlotIcon;
    [SerializeField] private int maxInventorySlots;
    [SerializeField] private int countOfItemsInSlot;
    [SerializeField] private GameObject healthItemPrefab;
    [SerializeField] private GameObject energyItemPrefab;


    private Item[,] inventory;
    private StarterAssetsInputs playerInputs;

    public Sprite CommonSlotImage { get { return commonInventorySlotIcon; } }

    public static event Action<int, int, bool> UpdateUI;

    private void Start()
    {
        inventory = new Item[maxInventorySlots, countOfItemsInSlot];
        playerInputs = GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
        CheckInventoryUse();
    }

    private void CheckInventoryUse()
    {
        if (playerInputs.inventoryFirstSlot)
        {
            UseItem(0);
            playerInputs.inventoryFirstSlot = false;
        }
        else if (playerInputs.inventorySecondSlot)
        {
            UseItem(1);
            playerInputs.inventorySecondSlot = false;
        }
    }

    public bool AddItem<T>(T item) where T : Item
    {
        var dimensionIndex = GetItemType(item);
        var emptySlot = FindFirstEmptySlot(dimensionIndex);
        if (emptySlot != -1)
        {
            HideObject(item);
            TryUpdateUI(dimensionIndex, emptySlot, false);
            inventory[dimensionIndex, emptySlot] = item;
        }
        return emptySlot != -1;
    }

    private int GetItemType<T>(T item) where T : Item
    {
        if (item.TryGetComponent<HealthItem>(out HealthItem healthItem))
            return (int) ItemType.HealthPotion;
        else if (item.TryGetComponent<EnergyItem>(out EnergyItem energyItem))
            return (int) ItemType.EnergyPotion;
        else
            return -1;
    }

    private int FindFirstEmptySlot(int dimensionIndex)
    {
        if (dimensionIndex > -1)
        {
            var length = countOfItemsInSlot;
            for (int i = 0; i < length; i++)
            {
                if (inventory[dimensionIndex, i] == null)
                    return i;
            }
        }
        return -1;
    }

    private void HideObject<T>(T item) where T : Item
    {
        item.gameObject.SetActive(false);
    }

    private void TryUpdateUI(int dimensionIndex, int slotId, bool isUsed)
    {
        if (UpdateUI != null)
            UpdateUI(dimensionIndex, slotId, isUsed);
    } 

    public void UseItem(int dimensionIndex)
    {
        var lastItemInInventory = FindLastItemInInventory(dimensionIndex);
        if (lastItemInInventory != -1)
        {
            var usedItem = inventory[dimensionIndex, lastItemInInventory];
            usedItem.Use();
            TryUpdateUI(dimensionIndex, lastItemInInventory, true);
            inventory[dimensionIndex, lastItemInInventory] = null;
        }
    }

    private int FindLastItemInInventory(int dimensionIndex)
    {
        for (int i = countOfItemsInSlot -1; i > -1; i--)
        {
            if (inventory[dimensionIndex, i] != null)
                return i;
        }
        return -1;
    }

    public int GetCountOfItem(ItemType itemType)
    {
        switch (itemType)
        {
            case (ItemType.HealthPotion):
                return FindLastItemInInventory((int)ItemType.HealthPotion) + 1;
            case (ItemType.EnergyPotion):
                return FindLastItemInInventory((int)ItemType.EnergyPotion) + 1;
            default:
                return -1;
        }
    }

    public void AddCountOfItem(ItemType itemType, int count)
    {
        for(int i=0; i < count; i++)
            AddItem(itemType);
    }

    private void AddItem(ItemType itemType)
    {
        var dimensionIndex = ((int)itemType);
        var emptySlot = FindFirstEmptySlot(dimensionIndex);
        if (emptySlot != -1)
        {
            TryUpdateUI(dimensionIndex, emptySlot, false);
            if (itemType == ItemType.HealthPotion)
            {
                var item = Instantiate(healthItemPrefab).GetComponent<HealthItem>();
                item.SetHealthItem(this);
                inventory[dimensionIndex, emptySlot] = item;
            }
            else
            {
                var item = Instantiate(energyItemPrefab).GetComponent<EnergyItem>();
                item.SetEnergyItem(this);
                inventory[dimensionIndex, emptySlot] = item;
            }
        }
    }
}
