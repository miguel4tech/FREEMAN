using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour
{
    #region VARIABLES
    private PlayerControllerExample movement; //Reference to the movement script controlling the button UI
    public Slider HealthPoint;

    public Animator anim;

    public Transform attackPoint;
    public LayerMask enemyLayers;
    public int enemiesCount;
    public TextMeshProUGUI enemiesCountText;

    public float attackRange = 1.5f;
    public int attackDamage = 20;

    public float attackRate = 2f;
    private float nextAttackTime = 0f;
    private float timer;

    private int maxHealth = 100;
    public static int currentHealth;
    public static bool gameOver;
    public static bool levelComplete;
    public AudioSource audioSource;
    public AudioClip attackSound;   


    #endregion
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        attackPoint = GameObject.FindGameObjectWithTag("Target Point").transform;
        HealthPoint.value = currentHealth = maxHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Update health bar UI
        HealthPoint.value = currentHealth;

        enemiesCount = GameObject.FindGameObjectsWithTag("Enemies").Length;
        enemiesCountText.text = "Enemies left: " + enemiesCount.ToString();

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
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage + 5);

        //Message
        Debug.Log("We hit with heavy " + enemy.name);
        }
    }

    // Inflict damage on the player
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //Play impact animation
        //anim.SetTrigger("Impact");

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
        currentHealth = maxHealth;

        //Disable enemy so no interaction is allowed
        GetComponent<Collider>().enabled = false;

        //Disable attached scripts
        // movement.enabled = false;
        // this.enabled = false;

        //Debug message
        Debug.Log("GAME OVER!");
    }
    
    public void LevelComplete()
    {
        if( enemiesCount == 0)
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.Euler(0, 180, 0);
            anim.SetBool("Victory", true);
            //Victory
            levelComplete = true;
            timer += Time.deltaTime; 
            if(timer > 5) // 5 seconds
            {
                int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
                if (nextLevel == 4)
                {
                    SceneManager.LoadScene(1); //Returns to MainMenu
                    return;
                }

                if (PlayerPrefs.GetInt("CurrentLevel", 1) < nextLevel)
                    PlayerPrefs.SetInt("CurrentLevel", nextLevel);

                SceneManager.LoadScene(nextLevel);
            }
        }
    }

    void OnDrawGizmosSelected()//Displays in scene editor a sphere gizmos to show the range of attack w.r.t the enemy
    {
        if(attackPoint == null)
        return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
