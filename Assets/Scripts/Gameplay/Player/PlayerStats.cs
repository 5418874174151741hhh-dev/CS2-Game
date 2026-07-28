using UnityEngine;

/// <summary>
/// 玩家统计数据
/// </summary>
public class PlayerStats
{
    public int Kills { get; set; } = 0;
    public int Deaths { get; set; } = 0;
    public int Assists { get; set; } = 0;
    public float Money { get; set; } = Constants.Economy.STARTING_MONEY;
    public int Health { get; set; } = (int)Constants.Player.MAX_HEALTH;
    public int Armor { get; set; } = 0;
    public int HeadshotKills { get; set; } = 0;
    public int KnifeKills { get; set; } = 0;
    public float RoundDamageDealt { get; set; } = 0;
    public float RoundDamageReceived { get; set; } = 0;

    /// <summary>
    /// 重置每回合数据
    /// </summary>
    public void ResetRoundStats()
    {
        RoundDamageDealt = 0;
        RoundDamageReceived = 0;
    }

    /// <summary>
    /// 获取K/D比率
    /// </summary>
    public float GetKDRatio()
    {
        return Deaths == 0 ? Kills : (float)Kills / Deaths;
    }

    /// <summary>
    /// 打印统计数据
    /// </summary>
    public void PrintStats()
    {
        Debug.Log($"[PlayerStats] K:{Kills} D:{Deaths} A:{Assists} | 金币:{Money} | HP:{Health} | 护甲:{Armor}");
    }
}
