using UnityEngine;

/// <summary>
/// 玩家数据管理 - 保存/加载玩家数据
/// </summary>
public class PlayerData : SingletonManager<PlayerData>
{
    [System.Serializable]
    public class SaveData
    {
        public string playerName;
        public int totalKills;
        public int totalDeaths;
        public int totalAssists;
        public int totalMoney;
        public int eloRating;
        public int level;
        public string lastPlayed;
        public List<string> ownedSkins = new List<string>();
        public Dictionary<string, bool> achievements = new Dictionary<string, bool>();
    }

    private Dictionary<int, SaveData> playerDataCache = new Dictionary<int, SaveData>();
    private string savePath = "Assets/StreamingAssets/PlayerData/";

    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// 保存玩家数据
    /// </summary>
    public void SavePlayerData(int playerId, Player player)
    {
        PlayerStats stats = player.GetStats();
        SaveData data = new SaveData
        {
            playerName = $"Player_{playerId}",
            totalKills = stats.Kills,
            totalDeaths = stats.Deaths,
            totalAssists = stats.Assists,
            totalMoney = (int)stats.Money,
            lastPlayed = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };

        playerDataCache[playerId] = data;
        string json = JsonUtility.ToJson(data, true);
        
        // 这里实现实际的存储逻辑
        Debug.Log($"[PlayerData] 玩家 {playerId} 数据已保存\n{json}");
    }

    /// <summary>
    /// 加载玩家数据
    /// </summary>
    public SaveData LoadPlayerData(int playerId)
    {
        if (playerDataCache.ContainsKey(playerId))
        {
            return playerDataCache[playerId];
        }

        // 这里实现实际的加载逻辑
        SaveData data = new SaveData();
        playerDataCache[playerId] = data;
        Debug.Log($"[PlayerData] 玩家 {playerId} 数据已加载");
        return data;
    }

    /// <summary>
    /// 删除玩家数据
    /// </summary>
    public void DeletePlayerData(int playerId)
    {
        if (playerDataCache.ContainsKey(playerId))
        {
            playerDataCache.Remove(playerId);
            Debug.Log($"[PlayerData] 玩家 {playerId} 数据已删除");
        }
    }
}
