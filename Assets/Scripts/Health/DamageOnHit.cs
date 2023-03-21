using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour
{
    // Variables for the amount of damage done and the Pawn that fired the bullet
    public float damageDone;
    public Pawn owner;

    public void OnTriggerEnter(Collider other)
    {
        Health otherHealth = other.gameObject.GetComponent<Health>();

        if (otherHealth != null)
        {
            otherHealth.TakeDamage(damageDone, owner);
        }

        // Destroy self after any collision
        Destroy(gameObject);

    }

}
