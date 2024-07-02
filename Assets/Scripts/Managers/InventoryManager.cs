using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public List<InventorySlot> inventorySlots { get; private set; }
    [SerializeField] private Transform inventoryPanel;

    private void Awake()
    {
        instance = this;
        inventorySlots = new List<InventorySlot>();

        for (int i = 0; i < inventoryPanel.childCount; i++)
        {
            if (inventoryPanel.GetChild(i).GetComponent<InventorySlot>())
            {
                inventorySlots.Add(inventoryPanel.GetChild(i).GetComponent<InventorySlot>());
            }
        }
    }

    private void OnEnable()
    {
        EventBus.OnDataLoaded += LoadInventory;
    }

    private void OnDisable()
    {
        EventBus.OnDataLoaded -= LoadInventory;
    }

    public void AddItem(InventoryItem item, int amount)
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.IsEmpty)
            {
                if (slot.Amount + amount <= item.maximumAmount)
                {
                    slot.SetUpItemInSlot(item, amount);
                    return;
                }
                else
                {
                    amount = amount + slot.Amount - item.maximumAmount;
                    slot.SetUpItemInSlot(item, item.maximumAmount);
                }
            }
            else if (slot.Item == item)
            {
                if (slot.Amount + amount <= item.maximumAmount)
                {
                    slot.ChangeItemAmount(slot.Amount + amount);
                    return;
                }
                else
                {
                    amount = amount + slot.Amount - item.maximumAmount;
                    slot.ChangeItemAmount(item.maximumAmount);
                }
            }
        }

        DataManager.instance.SetInventory(inventorySlots);
    }

    public InventorySlot FindSlotWithItem(InventoryItem item)
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.Item != null)
            {
                if (slot.Item.Equals(item))
                {
                    return slot;
                }
            }
        }
        return null;
    }
    public List<InventorySlot> FindAllSlotsWithItem(InventoryItem item)
    {
        List<InventorySlot> slotsWithDesiredItem = new List<InventorySlot>();

        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.Item != null)
            {
                if (slot.Item.Equals(item))
                {
                    slotsWithDesiredItem.Add(slot);
                }
            }
        }
        return slotsWithDesiredItem;
    }

    private void LoadInventory()
    {
        if (DataManager.instance.GetInventory().Count > 0)
        {
            for (int i = 0; i < DataManager.instance.GetInventory().Count; i++)
            {
                if (DataManager.instance.GetInventory()[i].Item != null)
                {
                    inventorySlots[i].SetUpItemInSlot(DataManager.instance.GetInventory()[i].Item, DataManager.instance.GetInventory()[i].Amount);
                }
            }
        }
    }
}

