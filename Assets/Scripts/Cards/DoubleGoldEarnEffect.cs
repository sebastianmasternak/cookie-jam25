using UnityEngine;

[CreateAssetMenu(fileName = "DoubleGoldEarnEffect", menuName = "Cards/Effect/DoubleGoldEarnEffect")]
public class DoubleGoldEarnEffect : CardEffect
{
    [Header("Upgrade Values")]
    public int goldEarnMultiplier = 2;
public int timeDuration = 30;

    public override void ApplyEffect()
    {
        Debug.Log("Applying Double Gold Earn Effect");

        GameManger gm = FindFirstObjectByType<GameManger>();
        if (gm != null)
        {
            gm.ActivateGoldMultiplier(goldEarnMultiplier, timeDuration);
        }
        else
        {
            Debug.LogError("DoubleGoldEarnEffect: GameManager not found.");
        }
    }
}
