using TMPro;
using UnityEngine;

public class EquipmentSlot : Slot
{
    [field: SerializeField] public ArmorType RequirableArmorType { private set; get; }

    [SerializeField] private TMP_Text protectionText;

    private void Start()
    {
        IsEmpty = true;
    }

    public override void SetUpItemInSlot(InventoryItem item)
    {
        if (item is ClothesItem equipmentItem)
        {
            if (equipmentItem.armorType.Equals(RequirableArmorType))
            {
                base.SetUpItemInSlot(equipmentItem);
                protectionText.text = equipmentItem.protectionAmount.ToString();
            }
        }
    }

    public override void ClearSlot()
    {
        base.ClearSlot();
        protectionText.text = "0";
    }

    public override void SelectSlot()
    {
        if (IsEmpty) return;
        PopUpManager.instance.OpenEquipmentInteractionWindow(this);
    }

    public void RemoveEquimpentItem()
    {
        InventoryManager.instance.AddItem(Item, 1);
        ClearSlot();
    }
}
