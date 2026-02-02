using UnityEngine;

[CreateAssetMenu(fileName = "DamageEnemiesInTile", menuName = "Cards/Effect/DamageEnemiesInTile")]
public class DamageEnemiesInTile : CardEffect
{
    public int damage;
   
    public override void ApplyEffect()
    {
        Debug.Log("Apply Damage in Tile");
        GameManger gameManager = FindFirstObjectByType<GameManger>();
        if (gameManager != null)
        {
            gameManager.StartTileSelection((tilePos) => ApplyDamageToTile(gameManager, tilePos));
        }
        else
        {
            Debug.LogError("DamageEnemiesInTile: GameManager not found.");
        }
    }

    private void ApplyDamageToTile(GameManger gm, Vector3Int tilePos)
    {
        Vector3 worldPos = gm.GetTileCenterWorld(tilePos);
        // Assuming tiles are approx 1x1 unit. Adjust size if necessary.
        Collider2D[] colliders = Physics2D.OverlapBoxAll(worldPos, new Vector2(1f, 1f), 0f);

        foreach (var col in colliders)
        {
            Entity entity = col.GetComponent<Entity>();
            if (entity != null && entity.type == Entity.EntityType.EnemyNPC)
            {
                entity.TakeDamage(damage, Entity.EntityType.Player);
                Debug.Log($"Damaged {entity.name} in tile {tilePos}");
            }
        }
    }
}
