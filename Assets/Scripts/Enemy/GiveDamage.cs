
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveDamage : MonoBehaviour
{
    public int damage;
   public Player player;
    void Star()
    {


        player = FindObjectOfType<Player>();

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag=="Player")
        {
            player.isHurt = true;
         
        }
    }
   


}
