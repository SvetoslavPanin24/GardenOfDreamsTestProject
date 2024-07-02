using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public void Shot()
    {
        WeaponItem currentWeapon = GetComponent<PlayerWeapon>().CurrentWeapon;

        if (currentWeapon == null) return;
        InventorySlot SlotWithBullet = InventoryManager.instance.FindSlotWithItem(currentWeapon.requirableBullet);

        if (SlotWithBullet)
        {
            if (SlotWithBullet.Amount >= currentWeapon.ammoPerShotAmount)
            {
                SlotWithBullet.LowerItemAmount(currentWeapon.ammoPerShotAmount);
                EventBus.OnEnemyTakeDamage(currentWeapon.damage);
                EventBus.OnPlayerShot();
            }
            else
            {
                int bulletsToTake = currentWeapon.ammoPerShotAmount; //количество патронов которое осталось найти

                List<InventorySlot> slotsWithBullet = InventoryManager.instance.FindAllSlotsWithItem(currentWeapon.requirableBullet);

                int totalAmountBullets = slotsWithBullet.Where(slot => slot).Sum(slot => slot.Amount);

                foreach (InventorySlot slot in slotsWithBullet)
                {
                    if (bulletsToTake >= slot.Amount)
                    {
                        bulletsToTake -= slot.Amount;
                        slot.ClearSlot();
                    }
                    if (bulletsToTake <= slot.Amount)
                    {
                        slot.ChangeItemAmount(slot.Amount - bulletsToTake);
                        bulletsToTake = 0;

                        EventBus.OnEnemyTakeDamage(currentWeapon.damage);
                        EventBus.OnPlayerShot();
                    }
                }
            }
        }
    }
}
