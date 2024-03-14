using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[Serializable]
public class InventorySlot : Slot
{
    public int Amount { get; protected set; }
    [SerializeField] protected TMP_Text amountText;

    public InventorySlot(SerializableInventorySlot serializableInventorySlot)
    {
        Amount = serializableInventorySlot.Amount;
        Item = serializableInventorySlot.Item;
    }

    private void Start()
    {
        IsEmpty = true;
        IconUIObject = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        amountText = transform.GetChild(1).GetComponent<TMP_Text>();
    }

    public override void SelectSlot()
    {
        switch (Item)
        {
            case ClothesItem:
                PopUpManager.instance.OpenArmorInteractionWindow(this);
                break;

            case HealItem:
                PopUpManager.instance.OpenHealInteractionWindow(this);
                break;

            case BulletItem:
                PopUpManager.instance.OpenAmmoInteractionWindow(this);
                break;
        }
    }

    public override void ClearSlot()
    {
        base.ClearSlot();
        ChangeItemAmount(0);
        
    }

    public void SetUpItemInSlot(InventoryItem item, int amount)
    {
        base.SetUpItemInSlot(item);
        ChangeItemAmount(amount);
    }

    public void ChangeItemAmount(int amount)
    {
        Amount = amount;
        if (amount > 1)
        {
            amountText.text = amount.ToString();
        }
        else
        {
            amountText.text = "";
        }
    }

    public void LowerItemAmount(int amount)
    {
        ChangeItemAmount(Amount - amount);

        if (Amount <= 0)
        {
            ClearSlot();
        }
    }

    public void InteractArmorItem()
    {
        EquipmentSlotsManager.instance.SetUpEquipment(this);
    }

    public void InteractHealItem()
    {
        HealItem interactableHealItem = Item as HealItem;
        EventBus.OnPlayerHeal(interactableHealItem.healAmount);
        LowerItemAmount(1);
    }

    public void InteractAmmoItem()
    {
        ChangeItemAmount(Item.maximumAmount);
    }
}