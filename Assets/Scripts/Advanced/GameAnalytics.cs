using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 游戏分析系统 - 统计游戏数据
/// </summary>
public class GameAnalytics : SingletonManager<GameAnalytics>
{
    [System.Serializable]
    public class MatchStats
    {
        public float matchDuration;
        public int totalRounds;
        public int ctWins;
        public int tWins;
        public int totalKills;
        public int totalDeaths;
        public int totalHeadshots;
        public List<PlayerMatchStats> playerStats = new List<PlayerMatchStats>();
    }

    [System.Serializable]
    public class PlayerMatchStats
    {
        public int playerId;
        public string playerName;
        public int kills;
        public int deaths;
        public int assists;
        public float damage;
        public int headshotCount;
        public float kda; // Kill/Death Ratio
    }

    private MatchStats currentMatch;
    private float matchStartTime;

    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// 启动比赛统计
    /// </summary>
    public void StartMatchAnalysis()
    {
        currentMatch = new MatchStats();
        matchStartTime = Time.time;
        Debug.Log("[GameAnalytics] 比赛统计已启动");
    }

    /// <summary>
    /// 记录击杀
    /// </summary>
    public void RecordKill(int killerId, int victimId, bool isHeadshot = false)
    {
        if (currentMatch == null)
            return;

        currentMatch.totalKills++;
        if (isHeadshot)
            currentMatch.totalHeadshots++;

        Debug.Log($"[GameAnalytics] 记录击杀: 玩家{killerId} -> 玩家{victimId} (爆头: {isHeadshot})");
    }

    /// <summary>
    /// 结束比赛统计
    /// </summary>
    public MatchStats EndMatchAnalysis()
    {
        if (currentMatch == null)
            return null;

        currentMatch.matchDuration = Time.time - matchStartTime;
        Debug.Log($"[GameAnalytics] 比赛统计已完成: {currentMatch.matchDuration}秒");
        Debug.Log($"总击杀: {currentMatch.totalKills}, 爆头: {currentMatch.totalHeadshots}");

        return currentMatch;
    }

    /// <summary>
    /// 获取当前比赛统计
    /// </summary>
    public MatchStats GetCurrentMatchStats() => currentMatch;
}
