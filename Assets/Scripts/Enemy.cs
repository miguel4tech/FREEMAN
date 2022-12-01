using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private string currentState = "IdleState";
    private Transform target;
    public Transform enemyAttackPoint;
    public LayerMask playerLayer;

    public float chaseRange = 5;
    public float attackRange = 2;
    public int attackDamage = 2;
    public float speed = 3;

    public float attackRate = 2f;
    private float nextAttackTime = 0f;

    public int currentHealth;
    public int maxHealth = 100;
    public Slider currentHealthBar;

    public Animator enemyAnim;

    void Start() //Initializing components
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        enemyAttackPoint = GameObject.Find("enemyAttackPoint").transform;
        currentHealthBar = FindObjectOfType<Slider>();
        currentHealthBar.value = currentHealth = maxHealth;
        enemyAnim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Update health bar UI
        currentHealthBar.value = currentHealth;
        // if (PlayerCombat.gameOver)
        // {
        //     enemyAnim.enabled = false;
        //     this.enabled = false;
        // }

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
            if (Time.time >= nextAttackTime)
            {
                enemyAnim.SetBool("isAttacking", true);
                //Detect player in attack range
                Collider[] playerInRange = Physics.OverlapSphere(enemyAttackPoint.position, attackRange, playerLayer);
                //Damage Player and Add-Ons
                foreach (Collider Player in playerInRange)
                {
                    Player.GetComponent<PlayerCombat>().TakeDamage(attackDamage);

                    //Message
                    Debug.Log(Player.name + " was stuck");
                }
                //Limits attacks to twice per time
                nextAttackTime = nextAttackTime + 1f / attackRange;

            }
                if (distance > attackRange)
                currentState = "ChaseState";
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
        Object.Destroy(gameObject, 20f );
    }

    void OnDrawGizmosSelected()
    {
        if (enemyAttackPoint == null)
            return;
        Gizmos.DrawWireSphere(enemyAttackPoint.position, attackRange);
    }

}