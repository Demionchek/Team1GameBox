using System;
using UnityEngine;
using StarterAssets;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Sprite commonInventorySlotIcon;
    [SerializeField] private int maxInventorySlots;
    [SerializeField] private int countOfItemsInSlot;

    private Item[,] inventory;
    private StarterAssetsInputs playerInputs;

    public Sprite CommonSlotImage { get { return commonInventorySlotIcon; } }

    public static event Action<int, int, bool> UpdateUI;

    enum ItemType
    {
        HealthPotion = 0,
        EnergyPotion = 1
    }

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
        var dimensionIndex = GetItemType<T>(item);
        if (dimensionIndex != -1)
        {
            var slotId = FindFirstEmptySlot(dimensionIndex);
            HideObject(item);
            TryUpdateUI(dimensionIndex, dimensionIndex, false);
            inventory[dimensionIndex, slotId] = item;
        }
        return FindFirstEmptySlot(dimensionIndex) > 0;
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
        for(int i=0; i < inventory.GetLength(dimensionIndex); i++)
        {
            if (inventory[dimensionIndex, i] == null)
                return i;
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
        for (int i = inventory.GetLength(dimensionIndex) -1; i > -1; i--)
        {
            if (inventory[dimensionIndex, i] != null)
                return i;
        }
        return -1;
    }
}
