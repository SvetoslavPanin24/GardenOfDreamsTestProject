using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private WeaponItem currentWeapon;
    public WeaponItem CurrentWeapon => currentWeapon;

    public void SwitchWeapon(WeaponItem weapon)
    {
        currentWeapon = weapon;
    }
}
