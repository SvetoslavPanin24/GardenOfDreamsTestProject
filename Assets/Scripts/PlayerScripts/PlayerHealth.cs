using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : Health
{
    private PlayerArmor armor;
  
    protected override void OnEnable()
    {
        base.OnEnable();
        EventBus.OnPlayerTakeDamage += TakeDamage;
        EventBus.OnPlayerHeal += Heal;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        EventBus.OnPlayerTakeDamage -= TakeDamage;
        EventBus.OnPlayerHeal -= Heal;
    }

    protected void Start()
    {
        armor = GetComponent<PlayerArmor>();
        maxHealth = 100;
    }

    protected void TakeDamage(int damage, BodyPartType bodyPart)
    {
        if (bodyPart == BodyPartType.Body)
        {
            damage -= armor.BodyProtection;
        }
        if (bodyPart == BodyPartType.Head)
        {
            damage -= armor.HeadProtection;
        }

        damage = (int)Mathf.Clamp(damage, 0, Mathf.Infinity);

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            EventBus.OnPlayerDead();
        }

        UpdateHealthUI(currentHealth);

        DataManager.instance.SetPlayerHealth(currentHealth);
    }
    private void Heal(int healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI(currentHealth);

        DataManager.instance.SetPlayerHealth(currentHealth);

    }
    protected override void UpdateHealthUI(int healthAmount)
    {
        healthSlider.GetComponent<SliderController>().ChangeSliderValue(healthAmount);
        healthText.text = healthAmount.ToString();
    }

    protected override void LoadHealth()
    {
        currentHealth = DataManager.instance.GetPlayerHealth();
        UpdateHealthUI(currentHealth);
    }
}
