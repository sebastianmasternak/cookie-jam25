using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /// Wersja ruchu gracza jeśli poruszanie się odbywa się co określony czas (tick)
    /*
    public float tickRate = 0.5f; // Time in seconds between each tick movement is processed
    private float tickTimer = 0f;
    public int numberOfTiles = 16;
    public int currentTileIndex = 3;
    public int tileSize = 1;

    public Transform playerTransform;

    public enum Direction
    {
        Left,
        Right,
        None
    }
    public Direction currentDirection = Direction.None;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdatePosition();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();

        tickTimer += Time.deltaTime;
        if (tickTimer >= tickRate)
        {
            Move();
            tickTimer = 0f;
        }
    }

    void HandleInput()
    {
        // If W or S is pressed (or held) we stop horizontal movement
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            currentDirection = Direction.None;
            return;
        }

        // A -> move left, D -> move right. Using GetKey allows holding the key.
        if (Input.GetKey(KeyCode.A))
        {
            currentDirection = Direction.Left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            currentDirection = Direction.Right;
        }
    }

    void Move()
    {
        if (currentDirection == Direction.Left && currentTileIndex > 0)
        {
            currentTileIndex--;
        }
        else if (currentDirection == Direction.Right && currentTileIndex < numberOfTiles - 1)
        {
            currentTileIndex++;
        }
        UpdatePosition();
    }

    void UpdatePosition()
    {
        playerTransform.position = new Vector3(currentTileIndex * tileSize, playerTransform.position.y, playerTransform.position.z);
    }
    */

    // Wersja ruchu gracza z płynnym poruszaniem się
    /* public float moveSpeed = 5f; // Speed of player movement
     public CharacterController controller;
     public enum Direction
     {
         Left,
         Right,
         None
     }
     public Direction currentDirection = Direction.None;

     void Start()
     { }

     private void Update()
     {
         HandleInput();
         Move();
     }

     void HandleInput()
     {
         currentDirection = Direction.None;
         // A -> move left, D -> move right. Using GetKey allows holding the key.
         if (Input.GetKey(KeyCode.A))
         {
             currentDirection = Direction.Left;
         }
         else if (Input.GetKey(KeyCode.D))
         {
             currentDirection = Direction.Right;
         }
     }

     void Move()
     {


         if (currentDirection == Direction.Left )
         {
             controller.Move(Vector3.left * moveSpeed * Time.deltaTime);

         }
         else if (currentDirection == Direction.Right)
         {
             controller.Move(Vector3.right * moveSpeed * Time.deltaTime);
         }
     }
    */

    public float moveSpeed = 5f; // Speed of player movement
    bool _canGoLeft = true;
    bool _canGoRight = true;

    void Start()
    { }

    private void Update()
    {
        bool isPressingA = Input.GetKey(KeyCode.A);
        bool isPressingD = Input.GetKey(KeyCode.D);

        if (isPressingA && _canGoLeft)
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        if (isPressingD && _canGoRight)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

        }
    }

}
