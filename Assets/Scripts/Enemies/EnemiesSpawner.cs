using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Collider2D))]
public class EnemiesSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject enemyPrefab;


    [Header("Ustawienia Czasu")]
    [SerializeField] private float minSpawnTime = 1f; 
    [SerializeField] private float maxSpawnTime = 3f;


    private float minX;
    private float maxX;
    private float yPos;

    void Start()
    {
        CalculateBounds();
        StartCoroutine(SpawnRoutine());
    }

    private void CalculateBounds()
    {
        Collider2D col = GetComponent<Collider2D>();
        Vector2 bounds = col.bounds.extents;
        minX = col.bounds.min.x;
        maxX = col.bounds.max.x;
        yPos = col.bounds.max.y;
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        float spawnX = Random.Range(minX, maxX);
        Vector3 spawnPos = new Vector3(spawnX, yPos, 0);
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}
