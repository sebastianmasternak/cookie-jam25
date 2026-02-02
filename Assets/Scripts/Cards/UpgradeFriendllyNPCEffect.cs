using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeFriendllyNPCEffect", menuName = "Cards/Effect/UpgradeFriendllyNPCEffect")]
public class UpgradeFriendllyNPCEffect : CardEffect
{
    [Header("Upgrade Values")]
    public int attackUpgradeAmount = 0;
    public int defenseUpgradeAmount = 0;
    public int healthAmmount = 0;
    public int maxHealthUpgradeAmount = 0;

    public override void ApplyEffect()
    {
        Debug.Log("Applying Upgrade to Friendly NPCs");

        var entities = FindObjectsByType<Entity>(FindObjectsSortMode.None);
        foreach (var entity in entities)
        {
            if (entity.type == Entity.EntityType.FriendlyNPC || entity.type == Entity.EntityType.Player)
            {
                entity.attackPower += attackUpgradeAmount;
                entity.defense += defenseUpgradeAmount;
                entity.maxHealth += maxHealthUpgradeAmount;
                
                // Heal them by the amount of max health added so they benefit immediately
                entity.Heal(healthAmmount);

                Debug.Log($"Upgraded {entity.name}: ATK+{attackUpgradeAmount}, DEF+{defenseUpgradeAmount}, HP+{maxHealthUpgradeAmount}");
            }
        }
    }
}
