using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health : MonoBehaviour
{
    private float currentHealth;
    public float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float dmgAmount, Pawn source)
    {
        currentHealth -= dmgAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log(source.name + " did " + dmgAmount + " damage to " + gameObject.name);

        if (currentHealth <= 0)
        {
            Die(source);
        }

    }

    public void Die(Pawn source)
    {
        Debug.Log(source.name + " destroyed " + gameObject.name);
        Destroy(gameObject);
    }
    
}
