using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Movement : MonoBehaviour
{
    public GameObject Bullet;                   // Bullet to shoot

    public Rigidbody2D rb;                      // Rigid body 2D

    public Transform Buster;                    // Shoot bullet from

    public float Blast;                         // Speed of bullet
    public float Speed;                         // Speed of player
    public float jump;                          // Jump of player
    public float BulletTime;                    // Amount of time before bullet dissapears
    public float FireRate;                      // Amount of time before bullet can be fired again

    public bool isGrounded;                     // Checks if player is grounded
    public Transform groundCheck;               // Ditto   
    public float groundCheckRadius;             // Ditto
    public LayerMask isGroundLayer;             // Ditto

    bool isFired = false;                       // Bool to check if bullet is being shot
    bool facingright = true;                    // Bool to check what direction the character is facing

    private Animator Animation;                 // Enables animations
    public Animator Death;                      // Death results in not being able to move

    public GameObject CollidedObject = null;    // Ensures the collision codes work

    private void Start()
    {
        // What object needs to be animated to look for
        Animation = GameObject.FindWithTag("Player").GetComponent<Animator>();
    }

    private void Update()
    {
        // Bool is set to true when player dies, it stops player movement so they can no longer move after death
        if (Death.GetBool("Dead") == false)
        {

            // Move left
            if (Input.GetKey(KeyCode.D))
            {
                if (!facingright)
                {
                    float xscale = transform.localScale.x;
                    float yscale = transform.localScale.y;
                    transform.localScale = new Vector3(xscale *= -1, yscale, transform.localScale.z);
                    facingright = true;
                }

                Vector3 Dir = new Vector3(Speed, 0, 0);
                transform.position += Dir * Time.deltaTime;
            }

            // Move right
            if (Input.GetKey(KeyCode.A))
            {
                if (facingright)
                {
                    float xscale = transform.localScale.x;
                    float yscale = transform.localScale.y;
                    float zscale = transform.localScale.z;
                    transform.localScale = new Vector3(xscale *= -1, yscale, zscale);
                    facingright = false;
                }

                Vector3 Dir = new Vector3(-Speed, 0, 0);
                transform.position += Dir * Time.deltaTime;
            }

            // Shoot
            if (Input.GetKey(KeyCode.K) && !isFired)
            {
                GameObject bullet = Instantiate(Bullet, Buster.position, Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(new Vector2(Blast * transform.localScale.x, 0.0f));
                Destroy(bullet, BulletTime);
                isFired = true;
                StartCoroutine(BulletTimer());
            }

            // Jump
            if (groundCheck)
            {
                isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
            }

            if (isGrounded)
            {
                if ((Input.GetKey(KeyCode.Space)))
                {
                    rb.AddForce(new Vector2(0, jump * Time.deltaTime));
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If player collides with the melee enemy, damage the player
        if (collision.gameObject.tag == "Enemy_Melee")
        {
            Animation.SetTrigger("Damaged");
            GameManager.instance.Health -= 1;
        }

        // If player collides with the shooting enemy, damage the player
        if (collision.gameObject.tag == "Enemy_Shooter")
        {
            Animation.SetTrigger("Damaged");
            GameManager.instance.Health -= 1;
        }

        // If player collides with a missile, damage the player
        if (collision.gameObject.tag == "Missile")
        {
            Destroy(collision.gameObject);
            Animation.SetTrigger("Damaged");
            GameManager.instance.Health -= 1;
        }

        // If player collides with health orb, give health to player
        if (collision.gameObject.tag == "HealthPowerUp")
        {
            Destroy(collision.gameObject);
            GameManager.instance.Health += 1;
        }

        // If player collides with life orb, give life to player
        if (collision.gameObject.tag == "LifePowerUp")
        {
            Destroy(collision.gameObject);
            GameManager.instance.Lives += 1;
        }

        // If player collides with weapon orb, give weapon to player
        if (collision.gameObject.tag == "WeaponPowerUp")
        {
            Destroy(collision.gameObject);
        }

        // If player collides with ability orb, give ability to player
        if (collision.gameObject.tag == "AbilityPowerUp")
        {
            Destroy(collision.gameObject);
        }
    }

    // Shooting timer
    IEnumerator BulletTimer()
    {
        yield return new WaitForSeconds(FireRate);
        isFired = false;
        StopAllCoroutines();
    }
}