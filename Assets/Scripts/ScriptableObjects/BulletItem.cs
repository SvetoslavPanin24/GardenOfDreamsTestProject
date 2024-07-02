using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletItem", menuName = "Inventory/Items/Consumables/newBulletItem")]
[Serializable]
public class BulletItem : InventoryItem 
{        
    public CaliberType type;  
}
