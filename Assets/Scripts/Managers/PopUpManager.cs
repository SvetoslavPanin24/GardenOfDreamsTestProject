using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    public static PopUpManager instance;
    private GameObject currentWindow;
    private Slot interactableSlot;

    [SerializeField] private GameObject antiClicker;

    [Header("Armor ui settings")]
    [SerializeField] private GameObject armorInteractionWindow;
    [Space]
    [SerializeField] private TMP_Text armorNameText;
    [SerializeField] private TMP_Text armorWeightText;
    [SerializeField] private TMP_Text armorProtectionText;
    [Space]
    [SerializeField] private Image armorIcon;

    [Header("Ammo ui settings")]
    [SerializeField] private GameObject ammoInteractionWindow;
    [Space]
    [SerializeField] private TMP_Text ammoNameText;
    [SerializeField] private TMP_Text ammoWeighText;
    [Space]
    [SerializeField] private Image ammoIcon;

    [Header("Heal ui settings")]
    [SerializeField] private GameObject healInteractionWindow;
    [Space]
    [SerializeField] private TMP_Text healNameText;
    [SerializeField] private TMP_Text healAmountText;
    [SerializeField] private TMP_Text healWeighText;
    [Space]
    [SerializeField] private Image healIcon;

    [Header("Equipment ui settings")]
    [SerializeField] private GameObject equipmentInteractionWindow;
    [Space]
    [SerializeField] private TMP_Text equipmentNameText;
    [SerializeField] private TMP_Text equipmentWeightText;
    [SerializeField] private TMP_Text equipmentProtectionText;
    [Space]
    [SerializeField] private Image equipmentIcon;

    [Header("GameOver ui settings")]
    [SerializeField] private GameObject gameOverWindow;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        EventBus.OnPlayerDead += OpenGameOverWindow;
    }

    private void OnDisable()
    {
        EventBus.OnPlayerDead -= OpenGameOverWindow;
    }

    public void OpenArmorInteractionWindow(InventorySlot slot)
    {
        antiClicker.SetActive(true);
        currentWindow = armorInteractionWindow;
        armorInteractionWindow.SetActive(true);

        ClothesItem tempItem = (ClothesItem)slot.Item;
        armorNameText.text = slot.Item.itemName;
        armorWeightText.text = slot.Item.weigh.ToString();
        armorProtectionText.text = "+ " + tempItem.protectionAmount.ToString();

        armorIcon.sprite = slot.Item.icon;

        interactableSlot = slot;
    }

    public void OpenAmmoInteractionWindow(InventorySlot slot)
    {
        antiClicker.SetActive(true);
        ammoInteractionWindow.SetActive(true);
        currentWindow = ammoInteractionWindow;

        ammoNameText.text = slot.Item.itemName;
        ammoWeighText.text = slot.Item.weigh.ToString();
        ammoIcon.sprite = slot.Item.icon;

        interactableSlot = slot;
    }

    public void OpenHealInteractionWindow(InventorySlot slot)
    {
        antiClicker.SetActive(true);
        healInteractionWindow.SetActive(true);
        currentWindow = healInteractionWindow;

        HealItem tempItem = (HealItem)slot.Item;
        healAmountText.text = "+ " + tempItem.healAmount.ToString();
        ammoNameText.text = slot.Item.itemName;
        ammoWeighText.text = slot.Item.weigh.ToString();

        ammoIcon.sprite = slot.Item.icon;

        interactableSlot = slot;
    }

    public void OpenEquipmentInteractionWindow(EquipmentSlot slot)
    {
        antiClicker.SetActive(true);
        currentWindow = equipmentInteractionWindow;
        currentWindow.SetActive(true);

        ClothesItem tempItem = (ClothesItem)slot.Item;
        equipmentNameText.text = slot.Item.itemName;
        equipmentWeightText.text = slot.Item.weigh.ToString();
        equipmentProtectionText.text = "+ " + tempItem.protectionAmount.ToString();

        equipmentIcon.sprite = slot.Item.icon;

        interactableSlot = slot;
    }
    public void OpenGameOverWindow()
    {
        antiClicker.SetActive(true);   
        gameOverWindow.SetActive(true);
    }

    public void InteractWithArmorSlot()
    {
        if (interactableSlot is InventorySlot slotWithArmorItem)
        {
            slotWithArmorItem.InteractArmorItem();            
        }
        CloseWindow();
    }

    public void InteractWithHealSlot()
    {
        if (interactableSlot is InventorySlot slotWithHealItem)
        {
            slotWithHealItem.InteractHealItem();
        }
        CloseWindow();
    }

    public void InteractWithAmmoSlot()
    {
        if (interactableSlot is InventorySlot slotWithAmmoItem)
        {
            slotWithAmmoItem.InteractAmmoItem();
        }
        CloseWindow();
    }
    public void InteractWithEquimpentSlot()
    {
        if (interactableSlot is EquipmentSlot slotWithEquipmentItem)
        {
            slotWithEquipmentItem.RemoveEquimpentItem();
        }
        CloseWindow();
    }
    public void CloseWindow()
    {
        antiClicker.SetActive(false);
        currentWindow.SetActive(false);
        currentWindow = null;
    }

    public void DeleteItemFromSlot()
    {
        interactableSlot.ClearSlot();
        CloseWindow();
    }
}
