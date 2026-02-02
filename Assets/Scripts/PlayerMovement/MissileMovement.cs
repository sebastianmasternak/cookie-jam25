using UnityEngine;

public class MissileMovement : MonoBehaviour
{
    public Transform transform;
    public float missileSpeed = 10.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * missileSpeed * Time.deltaTime;
    }
}
