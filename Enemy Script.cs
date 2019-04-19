using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public int Health;                          // Enemy's health

    public GameObject CollidedObject = null;    // Ensures the collision codes work

    void OnCollisionEnter2D(Collision2D collision)
    {
        // If player's bullet collides with enemy, make enemy health go down one unit
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            Health -= 1;
        }

        // If enemy health reaches 0 then delete the enemy
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}