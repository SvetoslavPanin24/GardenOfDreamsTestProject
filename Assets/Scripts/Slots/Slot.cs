using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public abstract class Slot : MonoBehaviour
{
    [field: SerializeField] public InventoryItem Item { get; protected set; }
    [field: SerializeField] public Image IconUIObject { get; protected set; }
    public bool IsEmpty { get; protected set; }

    public virtual void SetUpItemInSlot(InventoryItem item)
    {
        ClearSlot();
        IsEmpty = false;
        Item = item;
        SetIcon(item.icon);
    }

    public virtual void ClearSlot()
    {
        IsEmpty = true;
        Item = null;
        DeleteIcon();
    }

    protected void SetIcon(Sprite sprite)
    {
        IconUIObject.color = new Color(1, 1, 1, 1);
        IconUIObject.sprite = sprite;
    }

    protected void DeleteIcon()
    {
        IconUIObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        IconUIObject.GetComponent<Image>().sprite = null;
    }

    public abstract void SelectSlot();
}
