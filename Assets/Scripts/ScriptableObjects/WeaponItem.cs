using UnityEngine;

[CreateAssetMenu(fileName = "BulletItem", menuName = "Inventory/Items/Weapon/NewWeaponItem")]
public class WeaponItem : ItemScriptableObject
{
    public BulletItem requirableBullet;
    public int ammoPerShotAmount;
    public int damage;   
}
