using UnityEngine;

[CreateAssetMenu(fileName = "SpawnFriendlyNPCOnTile", menuName = "Cards/Effect/SpawnFriendlyNPCOnTile")]
public class SpawnFriendlyNPCOnTile : CardEffect
{
    public GameObject friendlyNPCPrefab;
   
    public override void ApplyEffect()
    {
        Debug.Log("Apply Damage in Tile");
        GameManger gameManager = FindFirstObjectByType<GameManger>();
        if (gameManager != null)
        {
            gameManager.StartTileSelection((tilePos) => ApplySpawnFriendlyNPCOnTile(gameManager, tilePos));
        }
        else
        {
            Debug.LogError("SpawnFriendlyNPCOnTile: GameManager not found.");
        }
    }

    private void ApplySpawnFriendlyNPCOnTile(GameManger gm, Vector3Int tilePos)
    {
        Vector3 worldPos = gm.GetTileCenterWorld(tilePos);
        Instantiate(friendlyNPCPrefab, worldPos, Quaternion.identity);
        Debug.Log($"Spawned {friendlyNPCPrefab.name} in tile {tilePos}");
    }
}
