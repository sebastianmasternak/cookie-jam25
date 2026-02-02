using UnityEngine;

public class FireballLauncher : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float tickRate = 1.0f; // Time in seconds between each missile launch
    private float tickTimer = 0f;

    public GameObject missilePrefab;
    public Transform launchPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        tickTimer += Time.deltaTime;
        if (tickTimer >= tickRate)
        {
            LaunchMissile();
            tickTimer = 0f;
        }
    }

    void LaunchMissile()
    {
        // Debug.Log("Player Fireball Launched!");
        // choose a random rotation around Z (2D)
        float angle = Random.Range(0f, 360f);
        Debug.Log(angle);
        launchPoint.rotation = Quaternion.Euler(0f, 0f, angle);

        Instantiate(missilePrefab, launchPoint.position, launchPoint.rotation);
    }
}
