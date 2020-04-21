using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public float maxHealth = 3;
    public float currentHealth = 0;
    public HealthBar healthBar = null;

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    
    public void UpdateHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0 , maxHealth);
        healthBar.SetHealthBarValue(currentHealth / maxHealth);
    }
}
