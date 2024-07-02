using UnityEngine;
using UnityEngine.UI;

public class WeaponSlotsManager : MonoBehaviour
{
    [SerializeField] GameObject rifleSlot;
    [SerializeField] GameObject pistolSlot;

    public void SelectRifleSlot()
    {
        rifleSlot.GetComponent<Outline>().enabled = true;
        pistolSlot.GetComponent<Outline>().enabled = false;
    }
    
    public void SelectPistolSlot() 
    {
        rifleSlot.GetComponent<Outline>().enabled = false;
        pistolSlot.GetComponent<Outline>().enabled = true;
    }
}
