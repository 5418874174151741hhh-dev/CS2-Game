using UnityEngine;

/// <summary>
/// 健康系统 - 管理玩家血量、护甲、恢复
/// </summary>
public class HealthSystem : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float healthRegenInterval = 0.5f;
    [SerializeField] private float healthRegenAmount = 1f;
    
    private float lastHealthRegenTime = 0f;

    private void Awake()
    {
        if (player == null)
        {
            player = GetComponent<Player>();
        }
    }

    private void Update()
    {
        // 死了就不管
        if (!player.IsAlive())
            return;

        // 每个间隔恢复一些血量（需要盔甲下子且没有受伤）
        if (Time.time - lastHealthRegenTime > healthRegenInterval)
        {
            // 这里可以添加回复逻辑
            lastHealthRegenTime = Time.time;
        }
    }

    /// <summary>
    /// 应用护甲
    /// </summary>
    public void ApplyArmor(int armorAmount)
    {
        PlayerStats stats = player.GetStats();
        stats.Armor += armorAmount;
        stats.Armor = Mathf.Min(stats.Armor, (int)Constants.Player.MAX_ARMOR);
        Debug.Log($"[HealthSystem] 应用护甲: {armorAmount}, 当前: {stats.Armor}");
    }

    /// <summary>
    /// 恢复血量
    /// </summary>
    public void Heal(int amount)
    {
        PlayerStats stats = player.GetStats();
        stats.Health = Mathf.Min(stats.Health + amount, (int)Constants.Player.MAX_HEALTH);
        Debug.Log($"[HealthSystem] 恢复 {amount} 血量, 当前: {stats.Health}");
    }
}
