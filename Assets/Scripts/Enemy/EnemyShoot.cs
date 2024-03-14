using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    private int damage;

    private bool isHeadShot;
    private bool isBodyShot;

    protected virtual void OnEnable()
    {
        EventBus.OnPlayerShot += Shot;
    }

    protected virtual void OnDisable()
    {
        EventBus.OnPlayerShot -= Shot;
    }

    private void Start()
    {
        damage = 15;
        isHeadShot = true;
        isBodyShot = false;
    }

    public void Shot()
    {
        if (isHeadShot)
        {
            EventBus.OnPlayerTakeDamage(damage, BodyPartType.Head);
            isHeadShot = false;
            isBodyShot = true;
            return;
        }
        if (isBodyShot)
        {
            EventBus.OnPlayerTakeDamage(damage, BodyPartType.Body);
            isHeadShot = true;
            isBodyShot = false;
            return;
        }
    }
}
