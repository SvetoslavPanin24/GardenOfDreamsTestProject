using System;

[Serializable]
public class SerializableInventorySlot
{
    public int Amount;
    public InventoryItem Item;

    public SerializableInventorySlot(InventorySlot slot)
    {
        Amount = slot.Amount;
        Item = slot.Item;
    }

    public SerializableInventorySlot(InventoryItem item, int amount)
    {
        Amount = amount;
        Item = item;
    }
}   
