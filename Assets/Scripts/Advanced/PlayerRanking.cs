using UnityEngine;

/// <summary>
/// 玩家排名系统 - ELO等级管理
/// </summary>
public class PlayerRanking : SingletonManager<PlayerRanking>
{
    [SerializeField] private int initialElo = 1000;
    [SerializeField] private float kFactor = 32f; // ELO计算因子
    [SerializeField] private string[] rankTitles = { "青铜", "白银", "黄金", "白金", "钻石", "传奇" };

    private Dictionary<int, PlayerRankData> playerRankings = new Dictionary<int, PlayerRankData>();

    /// <summary>
    /// 玩家排名数据
    /// </summary>
    [System.Serializable]
    public class PlayerRankData
    {
        public int playerId;
        public string playerName;
        public int eloRating = 1000;
        public int wins = 0;
        public int losses = 0;
        public int winStreak = 0;
        public string currentRank = "青铜";
        public int rankPoints = 0; // 段位积分
    }

    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// 添加玩家到排名系统
    /// </summary>
    public void RegisterPlayer(int playerId, string playerName)
    {
        if (!playerRankings.ContainsKey(playerId))
        {
            playerRankings[playerId] = new PlayerRankData
            {
                playerId = playerId,
                playerName = playerName,
                eloRating = initialElo,
                currentRank = rankTitles[0]
            };
            Debug.Log($"[PlayerRanking] 玩家 {playerName} 已注册，初始ELO: {initialElo}");
        }
    }

    /// <summary>
    /// 更新玩家ELO等级
    /// </summary>
    public void UpdateElo(int winnerId, int loserId)
    {
        if (!playerRankings.ContainsKey(winnerId) || !playerRankings.ContainsKey(loserId))
            return;

        PlayerRankData winner = playerRankings[winnerId];
        PlayerRankData loser = playerRankings[loserId];

        // 计算ELO变化
        float winnerExpected = 1f / (1f + Mathf.Pow(10f, (loser.eloRating - winner.eloRating) / 400f));
        float loserExpected = 1f / (1f + Mathf.Pow(10f, (winner.eloRating - loser.eloRating) / 400f));

        int winnerGain = Mathf.RoundToInt(kFactor * (1f - winnerExpected));
        int loserLoss = Mathf.RoundToInt(kFactor * (0f - loserExpected));

        // 更新数据
        winner.eloRating += winnerGain;
        winner.wins++;
        winner.winStreak++;
        loser.eloRating += loserLoss;
        loser.losses++;
        loser.winStreak = 0;

        // 更新段位
        UpdateRank(winner);
        UpdateRank(loser);

        Debug.Log($"[PlayerRanking] {winner.playerName} +{winnerGain} ELO (现在: {winner.eloRating}) | {loser.playerName} {loserLoss} ELO (现在: {loser.eloRating})");
    }

    /// <summary>
    /// 更新玩家段位
    /// </summary>
    private void UpdateRank(PlayerRankData player)
    {
        int rankIndex = Mathf.Clamp(player.eloRating / 200, 0, rankTitles.Length - 1);
        player.currentRank = rankTitles[rankIndex];
    }

    /// <summary>
    /// 获取玩家排名数据
    /// </summary>
    public PlayerRankData GetPlayerRankData(int playerId)
    {
        return playerRankings.ContainsKey(playerId) ? playerRankings[playerId] : null;
    }

    /// <summary>
    /// 获取排行榜前N名
    /// </summary>
    public List<PlayerRankData> GetTopPlayers(int topCount = 10)
    {
        List<PlayerRankData> sorted = new List<PlayerRankData>(playerRankings.Values);
        sorted.Sort((a, b) => b.eloRating.CompareTo(a.eloRating));
        return sorted.Take(topCount).ToList();
    }
}
