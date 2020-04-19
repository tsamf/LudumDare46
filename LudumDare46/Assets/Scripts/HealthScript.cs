using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    public int currentHealth = 0;

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    
    public void UpdateHealth(int amount)
    {
      currentHealth = Mathf.Clamp(currentHealth + amount, 0 , maxHealth);     
    }
}
