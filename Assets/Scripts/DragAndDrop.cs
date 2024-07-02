using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    // исходная позиция объекта
    private Vector3 originalPosition;
    //Слот из которого перетаскивается объект 
    private Slot dragSlot;
    //Слот в которой дропается объект
    private Slot droppedSlot;
    //Перетаскиваемый объект
    [field: SerializeField] private Transform dragObject;

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragSlot = GetComponent<Slot>();

        if (!dragSlot.IsEmpty)
        {
            originalPosition = dragObject.position; // запоминание исходной позиции 
            //выводиам перетаскиваемый объект поверх остальных элементов интерфейса
            dragObject.SetParent(transform.root);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!dragSlot.IsEmpty)
        {
            dragObject.GetComponent<RectTransform>().localPosition += new Vector3(eventData.delta.x, eventData.delta.y);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!dragSlot.IsEmpty)
        {
            if ((droppedSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>()) &&
            (dragSlot != droppedSlot))
            {
                if (droppedSlot is InventorySlot inventoryDroppedSlot)
                {
                    if (dragSlot is InventorySlot inventoryDragSlot)
                    {
                        if (dragSlot.Item == droppedSlot.Item)
                        {
                            if (inventoryDragSlot.Amount + inventoryDroppedSlot.Amount <= dragSlot.Item.maximumAmount)
                            {
                                inventoryDroppedSlot.ChangeItemAmount(inventoryDragSlot.Amount + inventoryDroppedSlot.Amount);
                                inventoryDragSlot.ClearSlot();
                            }
                            else
                            {
                                inventoryDragSlot.ChangeItemAmount(inventoryDragSlot.Amount + inventoryDroppedSlot.Amount - inventoryDragSlot.Item.maximumAmount);
                                inventoryDroppedSlot.ChangeItemAmount(inventoryDroppedSlot.Item.maximumAmount);
                            }
                        }
                        else
                        {
                            if (droppedSlot.IsEmpty)
                            {
                                inventoryDroppedSlot.SetUpItemInSlot(inventoryDragSlot.Item, inventoryDragSlot.Amount);
                                inventoryDragSlot.ClearSlot();
                            }
                            else
                            {
                                InventoryItem tempItem = inventoryDragSlot.Item;
                                int tempAmount = inventoryDragSlot.Amount;

                                inventoryDragSlot.SetUpItemInSlot(inventoryDroppedSlot.Item, inventoryDroppedSlot.Amount);
                                inventoryDroppedSlot.SetUpItemInSlot(tempItem, tempAmount);
                            }
                        }
                    }
                    if (dragSlot is EquipmentSlot equipmentDragSlot)
                    {
                        if (droppedSlot.Item is ClothesItem)
                        {
                            InventoryItem tempItem = equipmentDragSlot.Item;
                            equipmentDragSlot.SetUpItemInSlot(droppedSlot.Item);
                            droppedSlot.SetUpItemInSlot(tempItem);
                        }
                        if (inventoryDroppedSlot.IsEmpty)
                        {
                            inventoryDroppedSlot.SetUpItemInSlot(equipmentDragSlot.Item, 1);
                            equipmentDragSlot.ClearSlot();
                        }
                    }
                }
                if (droppedSlot is EquipmentSlot equipmentDroppedSlot)
                {
                    if (dragSlot is InventorySlot inventoryDragSlot)
                    {
                        if (droppedSlot)
                            if (inventoryDragSlot.Item is ClothesItem equipItem)
                            {
                                if (equipmentDroppedSlot.RequirableArmorType.Equals(equipItem.armorType))
                                {
                                    if (droppedSlot.IsEmpty)
                                    {
                                        EquipmentSlotsManager.instance.SetUpEquipment(inventoryDragSlot);
                                        inventoryDragSlot.ClearSlot();
                                    }
                                    else
                                    {
                                        EquipmentSlotsManager.instance.SetUpEquipment(inventoryDragSlot);
                                    }
                                }
                            }
                    }
                }
            }

            droppedSlot = null;
            dragObject.SetParent(dragSlot.transform);
            dragObject.position = originalPosition;
        }
    }
}