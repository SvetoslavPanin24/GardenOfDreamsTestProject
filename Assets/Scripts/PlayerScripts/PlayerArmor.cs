using UnityEngine;

public class PlayerArmor : MonoBehaviour
{
    public int BodyProtection => bodyArmor != null ? bodyArmor.protectionAmount : 0;
    public int HeadProtection => headArmor != null ? headArmor.protectionAmount : 0;

    private ClothesItem bodyArmor => EquipmentSlotsManager.instance.BodyArmorSlot!=null ? EquipmentSlotsManager.instance.BodyArmorSlot.Item as ClothesItem : null;
    private ClothesItem headArmor => EquipmentSlotsManager.instance.HeadArmorSlot != null ? EquipmentSlotsManager.instance.HeadArmorSlot.Item as ClothesItem : null;    
}

