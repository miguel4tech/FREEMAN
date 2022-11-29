using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour
{
    #region VARIABLES
    public static int score;
    public TextMeshProUGUI scoreText; 

    public Animator anim;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public int attackDamage = 30;

    public float attackRate = 2f;
    private float nextAttackTime = 0f;


    public int maxHealth = 100;
    private int currentHealth;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttackTime)
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

            if (Input.GetKeyDown(KeyCode.B))
            {
                Blocking();
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
        }
    }

    //Block damage
    public void Blocking()
    {
        anim.SetTrigger("Block");
    }
    void Die()
    {
        //Play died animation
        anim.SetBool("IsDead", true);

        //Disable enemy so no interaction is allowed
        GetComponent<Collider>().enabled = false;

        //Disable the this script
        this.enabled = false;

        //Debug message
        Debug.Log("GAME Over!");
    }


    // if( FindObjectsOfType<Enemy>().Length ==0)
    //     {
    //         //Win Level
    //         winLevel = true;
    //         timer += Time.deltaTime;
    //         if(timer > 5)
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
