using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour
{
    #region VARIABLES
    public static int score;
    public TextMeshProUGUI scoreText;

    public Slider currentHealthPoint;

    public Animator anim;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 1.5f;
    public int attackDamage = 20;

    public float attackRate = 2f;
    private float nextAttackTime = 0f;


    private int maxHealth = 100;
    public int currentHealth;
    
    public PlayerControllerExample movement;

    public bool gameOver;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        attackPoint = GameObject.FindGameObjectWithTag("Target Point").transform;
        currentHealthPoint.value = currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Update health bar UI
        currentHealthPoint.value = currentHealth;

        if (Time.time >= nextAttackTime)
        {
            if(Input.GetKeyDown(KeyCode.K))
            {
                Attack();
                //Slash twice per second
                nextAttackTime = nextAttackTime + 1f/attackRange;
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                ComboAttack();
                //Attack twice per second
                nextAttackTime = nextAttackTime + 1f / attackRange;
            }

        }
    }

    public void Attack()
    {
        //Play attack slash animation
        anim.SetTrigger("Slash");

        //Detect enemies in attack range
        Collider [] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        //Damage Enemies
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            
            //Message
            Debug.Log("We hit " + enemy.name);
        }

    }

    public void ComboAttack()
    {
        //Play attack slash-heavy animation
        anim.SetTrigger("Slash-Heavy");

        //Detect enemies in attack range
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        //Damage Enemies
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);

        //Message
        Debug.Log("We hit with heavy " + enemy.name);
        }
    }

    // Inflict damage on the player
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //Play impact animation
        anim.SetTrigger("Impact");

        if (currentHealth <= 0)
        {
            Die();
            gameOver = true;
        }
    }

    void Die()
    {
        //Play died animation
        anim.SetBool("IsDead", true);
        gameOver= true;

        //Disable enemy so no interaction is allowed
        GetComponent<Collider>().enabled = false;

        //Disable attached scripts
        movement.enabled = false;
        this.enabled = false;

        //Debug message
        Debug.Log("GAME OVER!");
    }


    // if( FindObjectsOfType<Enemy>().Length ==0)
    //     {
    //         //Win Level
    //         winLevel = true;
    //         timer += Time.deltaTime;
    //         if(timer > 5) //less than 5 seconds
    //         {
    //             int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
    //             if (nextLevel == 4)
    //                 SceneManager.LoadScene(0);

    //             if(PlayerPrefs.GetInt("ReachedLevel", 1) < nextLevel)
    //                 PlayerPrefs.SetInt("ReachedLevel", nextLevel);

    //             SceneManager.LoadScene(nextLevel);
    //         }

    //Displays in scene editor a sphere gizmos to show the range of attack w.r.t the enemy
    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
