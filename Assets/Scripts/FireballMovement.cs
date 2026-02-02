using UnityEngine;

public class FireballMovement : MonoBehaviour
{

    public float missileSpeed = 10.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Move in the direction the object is rotated to face (2D: up is forward)
        transform.position += transform.up * missileSpeed * Time.deltaTime;
    }
}
