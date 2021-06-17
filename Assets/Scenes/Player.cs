using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public Rigidbody2D body2D;

    //coliderler
    BoxCollider2D box2D;
    CircleCollider2D cir2D;




    /*hız yürüme*/
    [Tooltip("karakterin ne kadar hızlı gidiceğini belirler")]
    public float PlayerSpeed;

    /*zıplama */
    [Tooltip("karakterin ne kadar yükseğe zıplayıcağını belirler")]
    public float jumpPower;
    /* çif zıplama*/
    public float doubleJumpPower;
    internal bool canDoubleJump;
    /*ANİMATÖR*/
    Animator Anim;
    public float knocBackForce;


    /*karkater döndürme*/
    bool faceRight = true;
    //CheckPoint

    //sound
    AudioSource auSource;
    AudioClip au_jump;
   AudioClip au_hurt;
   AudioClip au_PickupCoin;
    AudioClip au_shot;




    /* yeri bulma*/
    [Tooltip("Karakterin yere değip değmediğini belirler")]
    public bool isGrouned;
    public Transform grounCheck;
    const float GroundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    /*Player can*/

    internal int maxPlayerHealth = 100;
    public int currentPlayerHealth;
    internal bool isHurt;
    GiveDamage giveDamage;
    // oyuncuyu öldür
    internal bool isDead;
    public float deadForce;
    /// Game manger

    GameManager GameManager;
    //oyunuc puan
    internal int CurrrentPoint;
    internal int point = 10;
    //ateş etmee
    Transform firePoint;
    GameObject bullet;
    public float bulletsped;

    void Start()
    {
    
        body2D = GetComponent<Rigidbody2D>();
        body2D.freezeRotation = true;
        body2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        grounCheck = transform.Find("GroundCheck");
        /*bi dahikine adam akılı kontrol et*/
        Anim = GetComponent<Animator>();

        /*CANI MAXCANA ESİTLE*/
        currentPlayerHealth = maxPlayerHealth;
        giveDamage = FindObjectOfType<GiveDamage>();

        box2D = GetComponent<BoxCollider2D>();
        cir2D = GetComponent<CircleCollider2D>();
        GameManager = FindObjectOfType <GameManager>();

        // loud SOund efeect
        auSource = GetComponent<AudioSource>();
        au_jump = Resources.Load("SoundEffect/_ump") as AudioClip;
        au_hurt = Resources.Load("SoundEffect/_hot") as AudioClip;
        au_PickupCoin = Resources.Load("SoundEffect/_oinUp") as AudioClip;
        au_shot = Resources.Load("SoundEffect/shot") as AudioClip;
        //ateş etme
        firePoint = transform.Find("FirePoint");
        bullet = Resources.Load("Bullet") as GameObject;

    } 
    void Update()
    {
       

        UpdateAnimtaions();
        ReduceHealth();
        // eğer canımız max candan yüksekse canımızı max cana eşitle*/
        isDead = currentPlayerHealth <= 0;

       
        KillPlayer();



    }

    void FixedUpdate()
    {

        isGrouned = Physics2D.OverlapCircle(grounCheck.position, GroundCheckRadius, groundLayer);
        float h = Input.GetAxis("Horizontal");
        body2D.velocity = new Vector2(h * PlayerSpeed, body2D.velocity.y);
        Flip(h);

    }
    public void Jump()
    {
        /*yükske zıplama*/
        body2D.AddForce(new Vector2(0, jumpPower));
        auSource.PlayOneShot(au_jump);
        auSource.pitch=Random.Range(0.8f,1.1f);
       
        

    }
    public void DoubleJump()
    {

        body2D.AddForce(new Vector2(0, doubleJumpPower), ForceMode2D.Impulse);

    }
    void Flip(float h)
    {
        if (h > 0 && !faceRight || h < 0 && faceRight)
        {
            faceRight = !faceRight;
            Vector2 theScale = transform.localScale;
            transform.localScale = new Vector2(transform.localScale.x*-1,transform.localScale.y);
            theScale.x *= -1;
            transform.localScale = theScale;

        }
    }
    void UpdateAnimtaions()
    {

        Anim.SetFloat("Velocity", Mathf.Abs(body2D.velocity.x));
        Anim.SetBool("isGrouned", isGrouned);
        Anim.SetFloat("VelocityY", body2D.velocity.y);
        Anim.SetBool("isDead",isDead);
        if (isHurt&&!isDead)
            Anim.SetTrigger("isHurt");
          
     



    }
    // can azaltma
    void ReduceHealth()
    {

        if (isHurt)
        {
            currentPlayerHealth -= 20;
            isHurt = false;



            if (faceRight && !isGrouned)
                body2D.AddForce(new Vector2(-knocBackForce,knocBackForce/1.5f),ForceMode2D.Force);
            if (!faceRight && !isGrouned)
                body2D.AddForce(new Vector2(knocBackForce, knocBackForce / 1.5f), ForceMode2D.Force);
            if (faceRight && isGrouned)
                body2D.AddForce(new Vector2(-knocBackForce, 0), ForceMode2D.Force);
            if (!faceRight && isGrouned)
                body2D.AddForce(new Vector2(knocBackForce, 0), ForceMode2D.Force);
            auSource.PlayOneShot(au_hurt);
            auSource.pitch = Random.Range(0.8f, 1.1f);



        }

    }

    void KillPlayer()
    {

        if (isDead)
        {
            isHurt = false;
            body2D.AddForce(new Vector2(0,deadForce),ForceMode2D.Impulse);
            deadForce -= Time.deltaTime * 15;
            body2D.constraints = RigidbodyConstraints2D.FreezePositionX;
            box2D.enabled = false;
            cir2D.enabled = false;

        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Coin")
        {
            CurrrentPoint += point;
            Destroy(collision.gameObject);
            auSource.PlayOneShot(au_PickupCoin);
            auSource.pitch = Random.Range(0.8f, 1.1f);
        }
     
    }
    public void ShootProject()
    {

        GameObject b = Instantiate(bullet) as GameObject;
        b.transform.position = firePoint.transform.position;
        b.transform.rotation = firePoint.transform.rotation;
        auSource.PlayOneShot(au_shot);
    
        if (transform.localScale.x<0)
        {
            b.GetComponent<Projectile>().bulletsped *= -1;
            b.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            b.GetComponent<Projectile>().bulletsped *= 1;
            b.GetComponent<SpriteRenderer>().flipX = false;
        }
   
        Vector3 bulletScale = b.transform.localScale;
        if (transform.localScale.x < 0)
        {

            bulletScale.x = -1;
        }
        else bulletScale.x = 1;

    }
}
