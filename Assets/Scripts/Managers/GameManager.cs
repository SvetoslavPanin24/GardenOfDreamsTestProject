using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] private List<InventoryItem> itemPool; // גסו ןנוהלועû

    private void OnEnable()
    {
        EventBus.OnEnemyDead += GivePlayerRandomItem;
    }
    private void OnDisable()
    {
        EventBus.OnEnemyDead -= GivePlayerRandomItem;
    }
    private void GivePlayerRandomItem()
    {
        if (itemPool.Count > 0)
        {
            int randomIndex = Random.Range(0, itemPool.Count);
            InventoryItem randomItem = itemPool[randomIndex];
            InventoryManager.instance.AddItem(randomItem, randomItem.maximumAmount);
        }
    }  
}
