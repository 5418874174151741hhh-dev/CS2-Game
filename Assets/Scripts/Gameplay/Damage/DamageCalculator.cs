using UnityEngine;

/// <summary>
/// 伤害计算器 - 计算最终伤害值
/// </summary>
public class DamageCalculator
{
    /// <summary>
    /// 计算伤害值
    /// </summary>
    public static float CalculateDamage(float baseDamage, Vector3 hitPosition, Vector3 targetPosition, RaycastHit hit)
    {
        float damage = baseDamage;

        // 部位伤害倍数
        string hitBone = hit.collider.name.ToLower();
        if (hitBone.Contains("head"))
        {
            damage *= Constants.Damage.HEAD_MULTIPLIER;
            Debug.Log($"[DamageCalculator] 头部打中！偏正值: x{Constants.Damage.HEAD_MULTIPLIER}");
        }
        else if (hitBone.Contains("leg") || hitBone.Contains("foot"))
        {
            damage *= Constants.Damage.LEG_MULTIPLIER;
            Debug.Log($"[DamageCalculator] 腿部打中。偏正值: x{Constants.Damage.LEG_MULTIPLIER}");
        }
        else
        {
            damage *= Constants.Damage.BODY_MULTIPLIER;
            Debug.Log($"[DamageCalculator] 躯体打中。偏正值: x{Constants.Damage.BODY_MULTIPLIER}");
        }

        // 距离衰减
        float distance = Vector3.Distance(hitPosition, targetPosition);
        if (distance > Constants.Damage.DISTANCE_FALLOFF)
        {
            float falloffMultiplier = Constants.Damage.DISTANCE_FALLOFF / distance;
            damage *= falloffMultiplier;
            Debug.Log($"[DamageCalculator] 距离衰减: {distance:F2}m, 倍数: {falloffMultiplier:F2}x");
        }

        return Mathf.Max(damage, 1f); // 最低1点伤害
    }

    /// <summary>
    /// 计算护甲衰减后的伤害
    /// </summary>
    public static float ApplyArmorReduction(float damage, int armor)
    {
        if (armor <= 0)
            return damage;

        float armorReduction = damage * 0.75f;
        float actualDamage = damage * 0.25f; // 仄25%的伤害穿过护甲

        Debug.Log($"[DamageCalculator] 护甲衰减: {damage:F2} -> {actualDamage:F2} (护甲减少: {Mathf.Min(armor, (int)armorReduction)})");
        return actualDamage;
    }
}
