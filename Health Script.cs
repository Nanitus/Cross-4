using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_LifeBar : MonoBehaviour
{
    private Animator Animation;                 // Enables animations

    private void Start()
    {
        // What object needs to be animated to look for
        Animation = GameObject.FindWithTag("Life").GetComponent<Animator>();
    }

    private void Update()
    {

        // If health is 3, play specific animation
        if (GameManager.instance.Health == 3)
        {
            Animation.SetTrigger("Fully Healed");
        }

        // If health is 2, play specific animation
        if (GameManager.instance.Health == 2)
        {
            Animation.SetTrigger("Damaged");
        }

        // If health is 1, play specific animation
        if (GameManager.instance.Health == 1)
        {
            Animation.SetTrigger("Damaged2");
        }

        // If health is 0, play specific animation
        if (GameManager.instance.Health == 0)
        {
            Animation.SetTrigger("Damaged3");
        }
    }
}
