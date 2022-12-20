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
<<<<<<< HEAD
    public int enemiesCount;
    public TextMeshProUGUI enemiesCountText;
=======
>>>>>>> parent of 5ee6deb (FIX)

    public float attackRange = 1.5f;
    public int attackDamage = 20;

    public float attackRate = 2f;
    private float nextAttackTime = 0f;
<<<<<<< HEAD
=======

>>>>>>> parent of 5ee6deb (FIX)

    private int maxHealth = 100;
    public static int currentHealth;
    
    public PlayerControllerExample movement; //Reference to the movement script controlling the button UI

    public static bool gameOver;
<<<<<<< HEAD
    public AudioSource audioSource;
    public AudioClip attackSound;   


=======
    public GameObject gameOverPanel;
>>>>>>> parent of 5ee6deb (FIX)
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= 2;
        anim = GetComponent<Animator>();
        attackPoint = GameObject.FindGameObjectWithTag("Target Point").transform;
        currentHealthPoint.value = currentHealth = maxHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Update health bar UI
<<<<<<< HEAD
        HealthPoint.value = currentHealth;


=======
        currentHealthPoint.value = currentHealth;
>>>>>>> parent of 5ee6deb (FIX)
        //Ensuring the object stays on track
        if (transform.position.z != 0)
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
       

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

        audioSource.PlayOneShot(attackSound);

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
        gameOverPanel.SetActive(true);

        //Disable enemy so no interaction is allowed
        GetComponent<Collider>().enabled = false;


        //Disable attached scripts
        movement.enabled = false;
        this.enabled = false;

        //Debug message
        Debug.Log("GAME OVER!");
    }
<<<<<<< HEAD
    
   

    void OnDrawGizmosSelected()//Displays in scene editor a sphere gizmos to show the range of attack w.r.t the enemy
=======


    //Displays in scene editor a sphere gizmos to show the range of attack w.r.t the enemy
    void OnDrawGizmosSelected()
>>>>>>> parent of 5ee6deb (FIX)
    {
        if(attackPoint == null)
        return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


}
