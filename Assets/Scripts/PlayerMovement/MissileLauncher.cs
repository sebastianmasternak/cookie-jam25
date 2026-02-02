using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
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
       // Debug.Log("Player Missile Launched!");
       
        if (missilePrefab != null && launchPoint != null)
        {
            Instantiate(missilePrefab, launchPoint.position, launchPoint.rotation);
        }
    }
}