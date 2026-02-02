using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int numberOfWaves = 5;
    int currentWave = 0;
    public GameObject[] enemiesPrefab;
    public int[] numberOfWaveEnemies;
    public float timeBetweenWaves = 5.0f;
    float currentTime = 0f;
    bool _active = false;
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void StartWaves()
    {
        _active = true;
    }

    public bool IsActive()
    {
        return _active;
    }

    public void End()
    {
        _active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentWave >= numberOfWaves)
        {
            End();
            return;
        }

        if (_active)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= timeBetweenWaves)
            {
                SpawnEnemies();
                currentTime = 0f;
            }
        }
    }

    public void SpawnEnemies()
    {
        int ememiesToSpawn = numberOfWaveEnemies[currentWave];

        for(int i = 0; i < ememiesToSpawn; i++)
        {
            Vector2 spawnPos = new Vector2(Random.Range(spawnAreaMin.x, spawnAreaMax.x), Random.Range(spawnAreaMin.y, spawnAreaMax.y));
            int enemyIndex = Random.Range(0, enemiesPrefab.Length);
            Instantiate(enemiesPrefab[enemyIndex], spawnPos, Quaternion.identity);
        }

        currentWave++;
    }
}
