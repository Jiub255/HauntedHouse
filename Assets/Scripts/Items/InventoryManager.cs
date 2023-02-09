using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<ItemAmount> inventoryList { get; private set; } = new List<ItemAmount>();

    public static event Action onInventoryChanged;

    private void OnEnable()
    {
        PickUpItem.onItemPickedUp += AddItem;
    }

    private void OnDisable()
    {
        PickUpItem.onItemPickedUp -= AddItem;
    }

    public void AddItem(ItemSO item)
    {
        foreach (ItemAmount itemAmount in inventoryList)
        {
            if (itemAmount.item == item)
            {
                itemAmount.amount++;
                onInventoryChanged?.Invoke();
                return;
            }
        }

        AddNewItemToList(item);
        onInventoryChanged?.Invoke();
    }

    private void AddNewItemToList(ItemSO item)
    {
        ItemAmount newItem = new ItemAmount();

        newItem.item = item;
        newItem.amount = 1;

        inventoryList.Add(newItem);
    }

    public void RemoveItem(ItemSO item)
    {
        foreach (ItemAmount itemAmount in inventoryList)
        {
            if (itemAmount.item == item)
            {
                itemAmount.amount--;
                onInventoryChanged?.Invoke();

                if (itemAmount.amount <= 0)
                {
                    inventoryList.Remove(itemAmount);
                    onInventoryChanged?.Invoke();
                    return;
                }
            }
        }
    }

    public ItemSO GetItemByName(string name)
    {
        foreach (ItemAmount itemAmount in inventoryList)
        {
            if (itemAmount.item.itemName == name)
            {
                return itemAmount.item;
            }
        }

        return null;
    }

    public ItemAmount ItemToItemAmount(ItemSO item)
    {
        foreach (ItemAmount itemAmount in inventoryList)
        {
            if (itemAmount.item == item)
            {
                return itemAmount;
            }
        }

        return null;
    }
}