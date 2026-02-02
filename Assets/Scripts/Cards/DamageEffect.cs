using UnityEngine;

[CreateAssetMenu(fileName = "DamageEffect", menuName = "Cards/Effect/Damage")]
public class DamageEffect : CardEffect
{
    public int damage;
   
    public override void ApplyEffect()
    {
        Debug.Log("Apply Damage");
        var entities = FindObjectsByType<Entity>(FindObjectsSortMode.None);
        foreach (var entity in entities)
        {
            if (entity.type == Entity.EntityType.EnemyNPC)
            {
                entity.TakeDamage(damage, Entity.EntityType.Player);
            }
        }
    }
}
