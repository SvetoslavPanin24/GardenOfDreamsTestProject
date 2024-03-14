using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletItem", menuName = "Inventory/Items/Clothes/newClothItem")]
[Serializable]
public class ClothesItem : InventoryItem
{
    public ArmorType armorType;
    public int protectionAmount;  
}
