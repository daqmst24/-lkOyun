using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    int maxEnemyHealth=100;
  public  float currentEnemyHealth;
    internal bool gotDamage;
    GiveDamageToEnemy Give;

  

    void Start()
    {
        Give=GetComponent<GiveDamageToEnemy>();
        currentEnemyHealth = maxEnemyHealth;
        

   
    }


    void Update()
    {
        if (gotDamage)
        {
            currentEnemyHealth -= Give.damage;
            gotDamage = false;


        }
       
    }
}
