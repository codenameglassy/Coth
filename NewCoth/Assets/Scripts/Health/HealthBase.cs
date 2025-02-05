using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour, IDamageable
{
    public EntityHealthData healthData;

    private float currentHealth;

    // Start is called before the first frame update
    public virtual void Start()
    {
        currentHealth = healthData.maxHealth;

    }

    public virtual void Takedamage(float damageAmount)
    {
        Debug.Log(gameObject.name + "took damage");
        currentHealth -= damageAmount;
        Hurt();

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Death();
        }
    }

    public virtual void Death()
    {
        Destroy(gameObject);
    }

    public virtual void Hurt()
    {

    }

    public float CurrentHealth()
    {
        return currentHealth;
    }

}
