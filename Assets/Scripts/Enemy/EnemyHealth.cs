using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : Health
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText;

    protected override void OnEnable()
    {
        base.OnEnable();
        EventBus.OnEnemyTakeDamage += TakeDamage;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        EventBus.OnEnemyTakeDamage -= TakeDamage;
    }

    protected void Start()
    {
        maxHealth = 100;
    }

    protected void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI(currentHealth);        
        DataManager.instance.SetEnemyHealth(currentHealth);

        if (currentHealth <= 0)
        {
            EventBus.OnEnemyDead();
        }
    }

    protected override void UpdateHealthUI(int amount)
    {
        healthSlider.GetComponent<Slider>().value = amount;
        healthText.text = amount.ToString();
    }

    protected override void LoadHealth()
    {
        currentHealth = DataManager.instance.GetEnemyHealth();
        UpdateHealthUI(currentHealth);
    }
}

