using UnityEngine;

/// <summary>
/// 玩家角色 - 基础玩家类
/// </summary>
public class Player : MonoBehaviour
{
    [SerializeField] private int teamId = Constants.Team.TEAM_CT;
    [SerializeField] private int playerId = 0;
    
    private PlayerStats stats;
    private bool isAlive = true;

    private void Awake()
    {
        stats = new PlayerStats();
    }

    private void Start()
    {
        Debug.Log($"[Player] 玩家 {playerId} (队伍 {teamId}) 已生成");
    }

    /// <summary>
    /// 玩家受到伤害
    /// </summary>
    public void TakeDamage(float damage, int damageType = 0)
    {
        if (!isAlive)
            return;

        float actualDamage = damage;

        // 护甲减伤
        if (stats.Armor > 0)
        {
            float armorReduction = actualDamage * 0.75f;
            float armorDamage = Mathf.Min(stats.Armor, armorReduction);
            stats.Armor -= (int)armorDamage;
            actualDamage *= 0.25f; // 25% 的伤害穿过护甲
        }

        stats.Health -= (int)actualDamage;
        stats.RoundDamageReceived += actualDamage;

        Debug.Log($"[Player] 玩家 {playerId} 受到 {actualDamage} 点伤害，剩余血量: {stats.Health}");

        if (stats.Health <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// 玩家死亡
    /// </summary>
    public void Die()
    {
        isAlive = false;
        stats.Deaths++;
        Debug.Log($"[Player] 玩家 {playerId} 已死亡");
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 玩家复活
    /// </summary>
    public void Respawn(Vector3 spawnPosition)
    {
        isAlive = true;
        stats.Health = (int)Constants.Player.MAX_HEALTH;
        stats.Armor = 0;
        transform.position = spawnPosition;
        gameObject.SetActive(true);
        Debug.Log($"[Player] 玩家 {playerId} 已复活");
    }

    /// <summary>
    /// 增加金币
    /// </summary>
    public void AddMoney(float amount)
    {
        stats.Money += amount;
        stats.Money = Mathf.Min(stats.Money, Constants.Economy.MAX_MONEY);
        Debug.Log($"[Player] 玩家 {playerId} 获得 {amount} 金币，当前: {stats.Money}");
    }

    /// <summary>
    /// 消费金币
    /// </summary>
    public bool SpendMoney(float amount)
    {
        if (stats.Money >= amount)
        {
            stats.Money -= amount;
            Debug.Log($"[Player] 玩家 {playerId} 花费 {amount} 金币，剩余: {stats.Money}");
            return true;
        }
        Debug.LogWarning($"[Player] 玩家 {playerId} 金币不足！需要: {amount}, 拥有: {stats.Money}");
        return false;
    }

    /// <summary>
    /// 增加击杀
    /// </summary>
    public void AddKill(bool isHeadshot = false)
    {
        stats.Kills++;
        if (isHeadshot)
        {
            stats.HeadshotKills++;
        }
        AddMoney(Constants.Economy.KILL_REWARD);
    }

    /// <summary>
    /// 增加助攻
    /// </summary>
    public void AddAssist()
    {
        stats.Assists++;
        AddMoney(Constants.Economy.ASSIST_REWARD);
    }

    /// <summary>
    /// 获取玩家统计
    /// </summary>
    public PlayerStats GetStats() => stats;

    /// <summary>
    /// 获取玩家ID
    /// </summary>
    public int GetPlayerId() => playerId;

    /// <summary>
    /// 获取队伍ID
    /// </summary>
    public int GetTeamId() => teamId;

    /// <summary>
    /// 是否活着
    /// </summary>
    public bool IsAlive() => isAlive;
}
