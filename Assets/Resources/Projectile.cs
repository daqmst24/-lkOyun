  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile  : MonoBehaviour
{


    Rigidbody2D bulletBody;
    public float bulletsped;
    void Start()
    {
        bulletBody = GetComponent<Rigidbody2D>();
        bulletBody.AddForce(new Vector2(bulletsped,0),ForceMode2D.Impulse);
        Invoke("SelfDestroy",5);
    }
    void SelfDestroy()
    {

        Destroy(gameObject);


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Ground")
        {
            Destroy(gameObject);
        }

    }
}
