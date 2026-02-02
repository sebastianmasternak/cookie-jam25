using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(ParticleSystem))]
public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private ParticleSystem movementParticles;

    [Header("Movement Settings")]
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float accelerationTime = 0.02f; // Much faster start
    [SerializeField] private float decelerationTime = 0.05f; // Quick stop

    private Vector2 moveInput;
    private Vector2 currentVelocityRef;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        movementParticles = GetComponent<ParticleSystem>();

        // Ensure physics settings are correct for top-down collision
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        // Normalize input so diagonal movement isn't faster
        Vector2 input = new Vector2(moveHorizontal, moveVertical);
        moveInput = input.normalized;

        //animator.SetFloat("MoveX", moveHorizontal);
        //animator.SetFloat("MoveY", moveVertical);

        HandleParticles();
    }

    private void HandleParticles()
    {
        if (movementParticles != null)
        {
            // Check if there is movement input
            if (rb.linearVelocity.sqrMagnitude > 0.2f)
            {
                if (!movementParticles.isPlaying) movementParticles.Play();
            }
            else
            {
                if (movementParticles.isPlaying) movementParticles.Stop();
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 targetVelocity = moveInput * maxSpeed;

        // Choose smoothing time based on whether we are accelerating (input present) or stopping
        float smoothTime = (moveInput.sqrMagnitude > 0.001f) ? accelerationTime : decelerationTime;

        rb.linearVelocity = Vector2.SmoothDamp(
            rb.linearVelocity,
            targetVelocity,
            ref currentVelocityRef,
            smoothTime
        );
    }

    // Debug collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Player collided with: {collision.gameObject.name}");
    }
}
