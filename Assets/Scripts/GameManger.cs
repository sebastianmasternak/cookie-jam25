using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using TMPro;

public class GameManger : MonoBehaviour
{
    [SerializeField] private Tilemap gameTilemap;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject prefabToSpawn;

    [SerializeField] private GameObject cursorHighlightPrefab;
    private GameObject currentCursorInstance;

    [SerializeField] private int coinCount = 0;

    public TextMeshProUGUI coinText;
    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    private System.Action<Vector3Int> onTileSelected;

    private int goldMultiplier = 1;
    private float goldMultiplierTimer = 0f;

    void Update()
    {
        // ... (existing update logic) ...
        if (goldMultiplierTimer > 0)
        {
            goldMultiplierTimer -= Time.deltaTime;
            if (goldMultiplierTimer <= 0)
            {
                goldMultiplier = 1;
                Debug.Log("Gold multiplier expired.");
            }
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            // ... (existing logic) ...
            Vector3Int tilePos = GetMouseTilePosition();
            Debug.Log($"Clicked Tile Position: {tilePos}");

            if (prefabToSpawn != null && gameTilemap != null)
            {
                Vector3 spawnPos = gameTilemap.GetCellCenterWorld(tilePos);
                Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
            }
            else
            {
                if (prefabToSpawn == null) Debug.LogWarning("PrefabToSpawn is not assigned!");
            }
        }

        if (onTileSelected != null)
        {
             // ... (existing logic) ...
            // Update cursor position
            if (currentCursorInstance != null)
            {
                Vector3Int mouseTilePos = GetMouseTilePosition();
                Vector3 worldPos = GetTileCenterWorld(mouseTilePos);
                currentCursorInstance.transform.position = worldPos;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Vector3Int tilePos = GetMouseTilePosition();
                onTileSelected.Invoke(tilePos);
                onTileSelected = null; // Reset after selection

                // Cleanup visual
                if (currentCursorInstance != null)
                {
                    Destroy(currentCursorInstance);
                    currentCursorInstance = null;
                }
            }
        }

        coinText.text = "Złoto: " + coinCount.ToString();
    }

    public void StartTileSelection(System.Action<Vector3Int> callback)
    {
        onTileSelected = callback;
        Debug.Log("Select a tile...");

        if (cursorHighlightPrefab != null)
        {
            if (currentCursorInstance != null) Destroy(currentCursorInstance);
            Vector3Int tilePos = GetMouseTilePosition();
            Vector3 worldPos = GetTileCenterWorld(tilePos);
            currentCursorInstance = Instantiate(cursorHighlightPrefab, worldPos, Quaternion.identity);
        }
    }

    public Vector3 GetTileCenterWorld(Vector3Int tilePos)
    {
         if (gameTilemap != null)
         {
             return gameTilemap.GetCellCenterWorld(tilePos);
         }
         return Vector3.zero;
    }

    public Vector3Int GetMouseTilePosition()
    {
        if (gameTilemap == null)
        {
            Debug.LogError("GameTilemap is not assigned in GameManger!");
            return Vector3Int.zero;
        }

        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0; 
 
        return gameTilemap.WorldToCell(mouseWorldPos);
    }

    public void AddCoins(int amount)
    {
        coinCount += amount * goldMultiplier;
    }

    public void ActivateGoldMultiplier(int multiplier, float duration)
    {
        goldMultiplier = multiplier;
        goldMultiplierTimer = duration;
        Debug.Log($"Gold multiplier x{multiplier} activated for {duration} seconds!");
    }

    public int GetCoins()
    {
        return coinCount;
    }

    public bool RemoveCoins(int amount)
    {
        if (coinCount < amount)
        {
            return false;
        }
        coinCount -= amount;
        return true;
    }

    // Inventory System
    public List<CardData> deck = new List<CardData>();

    public void AddCard(CardData card)
    {
        if (card == null) return;
        deck.Add(card);
        Debug.Log($"Added card: {card.cardName} to GameManager inventory.");

        if (card.cardEffects != null)
        {
            card.cardEffects.ApplyEffect();
        }
    }
}
