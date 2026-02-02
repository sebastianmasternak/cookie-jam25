using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Collider2D))]
public class BaseWall : MonoBehaviour
{
    private Collider2D col;

    [Header("Base Settings")]
    [SerializeField] private float baseHealth = 100f;

    private float currentHealth;


    void Start()
    {
        col = GetComponent<Collider2D>();
        currentHealth = baseHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log($"BaseWall took {amount} damage. Current Health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Debug.Log("BaseWall Destroyed!");
            Destroy(gameObject);
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > baseHealth)
        {
            currentHealth = baseHealth;
        }
        Debug.Log($"BaseWall healed by {amount}. Current Health: {currentHealth}");
    }
}
