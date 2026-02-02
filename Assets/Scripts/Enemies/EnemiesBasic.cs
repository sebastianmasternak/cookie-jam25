using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemiesBasic : MonoBehaviour
{
    public enum EnemyType
    {
        Melee,
        Ranged
    }

    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float direction = 1f;

    [SerializeField] private float damage = 10f;
    [SerializeField] private float damageInterval = 1f;

    private SpriteRenderer spriteRenderer;
    private bool isStopped = false;


    [Header("Enemy Type")]
    [SerializeField] private EnemyType enemyType = EnemyType.Melee; 
    [SerializeField] private float RangeDistanceFromWall = 5f;

    private BaseWall target;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        target = FindFirstObjectByType<BaseWall>();
    }

    void Update()
    {
        if (!isStopped)
        {
            if (enemyType == EnemyType.Ranged)
            {
                Vector2 position = target.transform.position;
                Debug.Log(Mathf.Abs(transform.position.y - target.transform.position.y));
                if (Mathf.Abs(transform.position.y - target.transform.position.y) < RangeDistanceFromWall)
                {
                    isStopped = true;
                    speed = 0;
                }
            }

            transform.Translate(Vector3.down * speed * direction * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleBaseCollision(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HandleBaseCollision(other.gameObject);
    }

    private void HandleBaseCollision(GameObject obj)
    {
        if (isStopped) return;

        if (obj.TryGetComponent<BaseWall>(out BaseWall baseWall))
        {
            isStopped = true;
            StartCoroutine(AttackRoutine(baseWall));
        }
    }

    private IEnumerator AttackRoutine(BaseWall baseWall)
    {

        while (baseWall != null)
        {
            baseWall.TakeDamage(damage);
            yield return new WaitForSeconds(damageInterval);
        }

        isStopped = false;
    }
}
