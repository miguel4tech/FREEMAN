using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    public Animator enemyAnim;

    public int maxHealth = 100;
    private int currentHealth;

    [SerializeField]
    private float speed = 5f;


    // Start is called before the first frame update
    void Start()
    {
        enemyAnim = GetComponent<Animator>();

        currentHealth = maxHealth;
    }

    void Update()
    {
        //Causes enemies to charge at Player
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;

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

    void Die()
    {
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
