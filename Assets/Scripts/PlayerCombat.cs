using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour
{
    #region VARIABLES
    private PlayerControllerExample movement; //Reference to the movement script controlling the button UI
    private GameManager gameManager;
    public Slider HealthPoint;

    public Animator anim;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRate = 2f;
    public float attackRange = 1.5f;
    public int attackDamage = 20;

    private float nextAttackTime = 0f;

    private int maxHealth = 100;
    public int currentHealth;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        movement = FindObjectOfType<PlayerControllerExample>();
        gameManager = FindObjectOfType<GameManager>();

        anim = GetComponent<Animator>();
        attackPoint = GameObject.FindGameObjectWithTag("Target Point").transform;
        HealthPoint.value = currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Update health bar UI
        HealthPoint.value = currentHealth;

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
        if (currentHealth <= 0)
        {
            Die();
            gameManager.gameOver= true;
        }
    }

    public void Attack()
    {
        //Play attack slash animation
        anim.SetTrigger("Slash");

        Audiomanager.instance.PlaySFX("Woosh");

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
        //anim.SetTrigger("Impact");

    }

    void Die()
    {
        //Play died animation
        anim.SetBool("IsDead", true);

        //Disable enemy so no interaction is allowed
        GetComponent<Collider>().enabled = false;

        //Disable attached scripts
        movement.enabled = false;

        currentHealth = maxHealth;
        this.enabled = false;

        //Debug message
        Debug.Log("GAME OVER!");
    }

    public void LevelComplete()
    {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.Euler(0, 180, 0);
            anim.SetBool("Victory", true);

            //disables movement script
            movement.enabled = false;
            
    }

    void OnDrawGizmosSelected()//Displays in scene editor a sphere gizmos to show the range of attack w.r.t the enemy
    {
        if(attackPoint == null)
        return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
