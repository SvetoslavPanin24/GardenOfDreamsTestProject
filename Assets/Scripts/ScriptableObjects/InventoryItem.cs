using System;

[Serializable]
public abstract class InventoryItem : ItemScriptableObject
{
    public string itemName;
    public int maximumAmount;
    public float weigh;   
}
