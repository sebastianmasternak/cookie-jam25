using UnityEngine;

[CreateAssetMenu(fileName = "HealthBaseEffect", menuName = "Cards/Effect/HealthBase")]
public class HealthBaseEffect : CardEffect
{
    public int healthBase;
   
    public override void ApplyEffect()
    {
        Debug.Log("Apply health base");

        BaseWall baseWall = FindFirstObjectByType<BaseWall>();
        if (baseWall != null)
        {
            baseWall.Heal(healthBase);
        }
        else
        {
            Debug.LogWarning("HealthBaseEffect: No BaseWall found to heal.");
        }
    }
}
