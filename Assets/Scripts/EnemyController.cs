using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    public float attackRange = 1.5f;
    public Transform playerPosition;
    public Animator enemyAnim;

    public int maxHealth = 100;
    public int currentHealth;
    
    public Slider currentHealthBar;


    [SerializeField]
    private float speed = 1.5f;


    // Start is called before the first frame update
    void Start() //initializing components
    {
        enemyAnim = GetComponent<Animator>();
        player = GameObject.Find("SwordWarrior");
        playerPosition = GameObject.Find("SwordWarrior").transform;

        currentHealthBar = Slider.FindObjectOfType<Slider>();
        currentHealthBar.value =  currentHealth = maxHealth;
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) > attackRange)
        {
        //Enemies follow Player
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        }

        Vector3 direction = Vector3.RotateTowards(Vector3.forward, player.transform.position - transform.position, 2f, 0f);
        //Enemy faces player direction
        transform.rotation = Quaternion.LookRotation(direction);
        //Update health bar UI
        currentHealthBar.value = currentHealth;
    }

    void Attack()
    {
        enemyAnim.SetTrigger("Slash");
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
