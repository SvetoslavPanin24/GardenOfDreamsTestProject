using UnityEngine;

public abstract class Health : MonoBehaviour
{
    protected int maxHealth;

    protected int currentHealth;
    public int CurrentHealth => currentHealth;

    protected virtual void OnEnable()
    {
        EventBus.OnDataLoaded += LoadHealth;
    }

    protected virtual void OnDisable()
    {
        EventBus.OnDataLoaded -= LoadHealth;
    }

    protected abstract void LoadHealth();               
    protected abstract void UpdateHealthUI(int healthAmount);    
}
