using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    #region VARIABLES
    private string currentState = "IdleState";
    private bool isDead;

    public float chaseRange = 3;
    public float attackRange = 2;
    public float speed = 3;

    public int currentHealth;
    public int maxHealth = 100;

    public Slider currentHealthBar;
    public Animator enemyAnim;
    
    private Transform target;
    private GameManager gameManager;
    #endregion
    void Start() //Initializing components
    {
        gameManager = FindObjectOfType<GameManager>();

        target = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealthBar = FindObjectOfType<Slider>();
        enemyAnim = GetComponentInChildren<Animator>();
        currentHealthBar.value = currentHealth = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        //Update health bar UI
        currentHealthBar.value = currentHealth;

        #region MOVEMENT
        float distance = Vector3.Distance(transform.position, target.position);

        if (currentState == "IdleState")
        {
            if (distance < chaseRange)
                currentState = "ChaseState";
        }
        else if (currentState == "ChaseState")
        {
            //play the run animation
            enemyAnim.SetTrigger("chase");
            enemyAnim.SetBool("isAttacking", false);

            if (distance < attackRange)
                currentState = "AttackState";

            //move towards the player
            if (target.position.x > transform.position.x)
            {
                //move right
                transform.Translate(transform.right * speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                //move left
                transform.Translate(-transform.right * speed * Time.deltaTime);
                transform.rotation = Quaternion.identity;
            }

        }
        else if (currentState == "AttackState")
        {
            enemyAnim.SetBool("isAttacking", true);
            if (distance > attackRange)
                currentState = "ChaseState";
            //Ensuring the object stays on track
            if(transform.position.z != 0)
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                
        }
        #endregion
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentState = "ChaseState";
        //Play impact animation
        enemyAnim.SetTrigger("Impact");

        if(currentHealth <= 0 && !isDead)
        {
            Die();
            isDead = true;
        }
    }
    

    private void Die()
    {
        //Play a die animation
        enemyAnim.SetBool("isDead", true);
        //Disable enemy so no interaction is allowed
        GetComponent<Collider>().enabled = false;
        //Increments the body count
        gameManager.totalEnemyKilled += 1;
        //Destroy enemy after 10secs
        Object.Destroy(gameObject, 10f );
    }

}