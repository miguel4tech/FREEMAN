using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Animator enemyAnim;
    public int maxHealth = 100;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnim = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    // Inflict damage on the enemy
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //Play impact animation
        enemyAnim.SetTrigger("Impact");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die(){
        //Play died animation
        enemyAnim.SetBool("IsDead", true);

        //Disable enemy so no interaction is allowed
        GetComponent<Collider>().enabled = false;

        //Disable the enemy script
        this.enabled = false;

        //Debug message
        Debug.Log("Enemy died!");
    }
}
