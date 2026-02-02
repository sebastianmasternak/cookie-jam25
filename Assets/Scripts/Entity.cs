using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
   // public Slider hpBar;

    public enum EntityType
    {
        Player,
        EnemyNPC,
        FriendlyNPC
    }
    public EntityType type;

    public int maxHealth;
    public int health;
    public int attackPower;
    public int defense;
    public float attackTick = 0.5f;
    public float currentTick = 0f;
    public Collider2D collider2d;

    void Start()
    {
        if(collider2d == null)
        {
            collider2d = GetComponent<Collider2D>();
        }
        goldReward = 2;
    }

    void Update()
    {
        if(health <= 0)
        {
            Die();
        }
        currentTick += Time.deltaTime;

       /* if (currentTick > attackTick)
        {
            collider2d.enabled = false;
            collider2d.enabled = true;
        }*/
    }

    public void TakeDamage(int damage, EntityType attackerType)
    {
        if (attackerType == EntityType.EnemyNPC && (type == EntityType.FriendlyNPC || type == EntityType.Player))
        {
            int damageTaken = damage - defense;
            if (damageTaken < 0)
            {
                damageTaken = 0;
            }
            health -= damageTaken;
        }

        if (type == EntityType.EnemyNPC && (attackerType == EntityType.FriendlyNPC || attackerType == EntityType.Player))
        {
            int damageTaken = damage - defense;
            if (damageTaken < 0)
            {
                damageTaken = 0;
            }
            health -= damageTaken;
        }
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public int goldReward = 2;

    void Die()
    {
        if (type == EntityType.Player)
        {
            Debug.Log("Player has died. Game Over.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            if (type == EntityType.EnemyNPC)
            {
                GameManger gm = FindFirstObjectByType<GameManger>();
                if (gm != null)
                {
                    gm.AddCoins(goldReward);
                    Debug.Log($"Enemy died, awarded {goldReward} coins.");
                }
            }
            Destroy(gameObject);
        }
    }

   /* private void OnTriggerEnter2D(Collider2D other)
    {
        if (currentTick >= attackTick)
        {
            currentTick = 0f;
            Debug.Log("Entity hit: " + other.gameObject.name);
            Entity entity = other.GetComponent<Entity>();
            if (entity != null)
            {
                entity.TakeDamage(attackPower, type);
            }
            
            // Reset collider immediately after attack to allow next trigger detection
            if (collider2d != null)
            {
                collider2d.enabled = false;
                collider2d.enabled = true;
            }
        }
    } */

    private void OnTriggerStay2D(Collider2D other)
    {  
        if (currentTick >= attackTick)
        {
            
            Debug.Log("Entity hit: " + other.gameObject.name);
            Entity entity = other.GetComponent<Entity>();
            if (entity != null)
            {
                if (type == EntityType.EnemyNPC && (entity.type == EntityType.FriendlyNPC || entity.type == EntityType.Player))
                    currentTick = 0f;
                else if (entity.type == EntityType.EnemyNPC && (type == EntityType.FriendlyNPC || type == EntityType.Player))
                    currentTick = 0f;

                entity.TakeDamage(attackPower, type);
            }

            // Reset collider immediately after attack to allow next trigger detection
            if (collider2d != null)
            {
                collider2d.enabled = false;
                collider2d.enabled = true;
            }
        }
    }
}
