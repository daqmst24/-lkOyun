using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerEnemyController : MonoBehaviour
{
    Rigidbody2D rg;
   
    public float enemysped;
    /* yeri bulma*/
    [Tooltip("Karakterin yere değip değmediğini belirler")]
     bool isGrouned;
     Transform grounCheck;
    const float GroundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public bool moveRight;
    //Ucurumu bulma
    bool onEdge;
    Transform edgeCheck;
    Animator anim;
    //audio control
    AudioSource Audio;
   public AudioClip heat;

    public GameObject deathParticle;
    SpriteRenderer graph;
    CircleCollider2D cir2D;
    EnemyHealth EnemyHealth;
    void Start()
    {


        EnemyHealth = GetComponent<EnemyHealth>();
        cir2D = GetComponent<CircleCollider2D>();
        graph = GetComponent<SpriteRenderer>();
        edgeCheck = transform.Find("EdgeCheck");
        anim = GetComponent<Animator>();
        rg = GetComponent<Rigidbody2D>();
        grounCheck = transform.Find("GroundCheck");
        Audio = GetComponent<AudioSource>();
        heat = Resources.Load("SoundEffect/EnemyDead") as AudioClip;

    }

    void Update()
    {
        isGrouned = Physics2D.OverlapCircle(grounCheck.position, GroundCheckRadius, groundLayer);
        onEdge = Physics2D.OverlapCircle(edgeCheck.position, GroundCheckRadius, groundLayer);
        if (isGrouned||!onEdge)
        {
            moveRight = !moveRight;
        }
        rg.velocity = (moveRight) ? new Vector2(enemysped, 0) : new Vector2(-enemysped, 0);
        transform.localScale=(moveRight) ? new Vector2(-1, 1) : new Vector2(1, 1);

        if (EnemyHealth.currentEnemyHealth <= 0)
        {
            Audio.PlayOneShot(heat);
            Destroy(gameObject);
      
            
         

        }

    }
}
