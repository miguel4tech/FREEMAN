using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    #region VARIABLES
    private string currentState = "IdleState";

    public float chaseRange = 3;
    public float attackRange = 2;
    public float speed = 100;
    public int currentHealth;
    public int maxHealth = 100;
    public Slider currentHealthBar;
    public Animator enemyAnim;
    private Transform target;
    public Rigidbody rb;

    #endregion
    void Start() //Initializing components
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        Physics.gravity *= 2;
        currentHealthBar = FindObjectOfType<Slider>();
        enemyAnim = GetComponentInChildren<Animator>();
        currentHealthBar.value = currentHealth = maxHealth;

    }

    // Update is called once per frame
    void FixedUpdate()
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
            rb.isKinematic = false;

            if (distance < attackRange)
            {
                rb.isKinematic = true;
                currentState = "AttackState";
            }

            //move towards the player
            if (target.position.x > transform.position.x)
            {
                //move right
                rb.velocity = Vector3.right * speed * Time.fixedDeltaTime;
               // transform.Translate(transform.right * speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                //move left
                rb.velocity = Vector3.left * speed * Time.fixedDeltaTime;

               // transform.Translate(-transform.right * speed * Time.deltaTime);
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

        if(currentHealth <= 0)
        {
            Die();
            currentState = "DeathState";
        }
    }
    

    private void Die()
    {
        //Play a die animation
        enemyAnim.SetBool("isDead", true);

        //Disable enemy so no interaction is allowed
        GetComponent<Collider>().enabled = false;
        //Destroy enemy
        Object.Destroy(gameObject, 10f );
    }

}