using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PropertyHealth : MonoBehaviour
{
    private float currentHealth;

    [SerializeField] private float maxHealth;
    [SerializeField] private ParticleSystem deathParticle;

    public Action<float> OnUpdateUI;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    public void TakeDamage(float damage)
    {
        if(currentHealth - damage > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth -= damage;
        }

        UpdateUI();

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (deathParticle != null)
        {
            Instantiate(deathParticle, transform.position, transform.rotation);
        }
        Destroy(this.gameObject);
    }

    public void UpdateUI()
    {
        //convert health to a %
        float percentHP = currentHealth / maxHealth * 100;

        if (OnUpdateUI != null)
        {
            OnUpdateUI(percentHP);
        }

    }
}
