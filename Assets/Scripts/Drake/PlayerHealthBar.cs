using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthBar : MonoBehaviour
{
    private Slider playerHealthBar;

    private void Awake()
    {
        playerHealthBar = GetComponent<Slider>();
        if (playerHealthBar == null)
        {
            Debug.LogError("Slider no encontrado en PlayerHealthBar");
        }
    }

    public void UpdateMaxHealth(float maxHealth)
    {
        playerHealthBar.maxValue = maxHealth;
    }

    public void UpdateCurrentHealth(float currentHealth)
    {
        playerHealthBar.value = currentHealth;
    }

    public void InitHealthBar(float currentHealth)
    {
        UpdateMaxHealth(currentHealth);
        UpdateCurrentHealth(currentHealth);
    }
}
