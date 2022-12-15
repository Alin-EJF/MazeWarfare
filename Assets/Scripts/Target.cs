using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour, IDamageable
{
    public float health;
    public float startHealth;
    public Image healthbar;

    public void TakeDamage(float damage)
    {
        
        health -= damage;
        healthbar.fillAmount=health/startHealth;

        Debug.Log(health);
        if (health <= 0)
            Destroy(gameObject);

    }
}
