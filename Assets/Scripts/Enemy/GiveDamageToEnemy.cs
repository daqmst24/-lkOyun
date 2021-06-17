using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveDamageToEnemy : MonoBehaviour
{
    public float damage;
    EnemyHealth EnemyHealth;

     void Start()
    {
        EnemyHealth = GetComponent<EnemyHealth>();
      
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="PlayerItem")
        {
            EnemyHealth.gotDamage = true;
        }
    }
}
