using UnityEngine;

public class EquipmentSlotsManager : MonoBehaviour
{
    public static EquipmentSlotsManager instance;
    [field: SerializeField] public EquipmentSlot BodyArmorSlot { private set; get; }
    [field: SerializeField] public EquipmentSlot HeadArmorSlot { private set; get; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }        
    }

    private void OnEnable()
    {
        EventBus.OnDataLoaded += LoadBodyArmor;
        EventBus.OnDataLoaded += LoadHeadArmor;
    }
    private void OnDisable()
    {
        EventBus.OnDataLoaded -= LoadBodyArmor;
        EventBus.OnDataLoaded -= LoadHeadArmor;
    }

    public void SetUpEquipment(InventorySlot slot)
    {
        if (slot.Item is ClothesItem equipmentItem)
        {
            if (equipmentItem.armorType.Equals(ArmorType.Body))
            {
                if (BodyArmorSlot.Item != null)
                {
                    ClothesItem tempItem = BodyArmorSlot.Item as ClothesItem;
                    BodyArmorSlot.SetUpItemInSlot(equipmentItem);
                    slot.SetUpItemInSlot(tempItem);
                }
                else
                {
                    BodyArmorSlot.SetUpItemInSlot(equipmentItem);
                    slot.ClearSlot();
                }
                
                DataManager.instance.SetPlayerBodyArmor(equipmentItem);
            }

            if (equipmentItem.armorType.Equals(ArmorType.Head))
            {
                if (HeadArmorSlot.Item != null)
                {
                    ClothesItem tempItem = HeadArmorSlot.Item as ClothesItem;
                    HeadArmorSlot.SetUpItemInSlot(equipmentItem);
                    slot.SetUpItemInSlot(tempItem);
                }
                else
                {
                    HeadArmorSlot.SetUpItemInSlot(equipmentItem);
                    slot.ClearSlot();
                }

                DataManager.instance.SetPlayerHeadArmor(equipmentItem);
            }
        }
    }
    public void LoadBodyArmor()
    {
        BodyArmorSlot.SetUpItemInSlot(DataManager.instance.GetPlayerBodyArmor());
    }

    public void LoadHeadArmor()
    {
        HeadArmorSlot.SetUpItemInSlot(DataManager.instance.GetPlayerHeadArmor());
    }
}

