using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField][Range(0, 200)] protected int maxHealth = 100;

    [Header("Health UI")]
    [SerializeField] protected Slider healthSlider;
    [SerializeField] protected TMP_Text healthText;

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
