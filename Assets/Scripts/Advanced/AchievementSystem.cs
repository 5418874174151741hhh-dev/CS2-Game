using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 成就系统 - 管理玩家成就和徽章
/// </summary>
public class AchievementSystem : SingletonManager<AchievementSystem>
{
    [SerializeField] private bool debugMode = false;

    public enum AchievementType
    {
        FirstKill,      // 首次击杀
        DoubleKill,     // 双杀
        TripleKill,     // 三杀
        MultiKill,      // 五连杀
        Headshot,       // 爆头
        Perfect,        // 完美一轮
        Defuser,        // 拆除专家
        Clutcher,       // 翻盘王
    }

    [System.Serializable]
    public class Achievement
    {
        public AchievementType type;
        public string title;
        public string description;
        public bool unlocked = false;
        public int unlockedTime = 0;
    }

    private Dictionary<int, Dictionary<AchievementType, Achievement>> playerAchievements = new Dictionary<int, Dictionary<AchievementType, Achievement>>();

    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// 注册玩家成就系统
    /// </summary>
    public void RegisterPlayerAchievements(int playerId)
    {
        if (!playerAchievements.ContainsKey(playerId))
        {
            playerAchievements[playerId] = new Dictionary<AchievementType, Achievement>();
            InitializeAchievements(playerId);
        }
    }

    /// <summary>
    /// 初始化所有成就
    /// </summary>
    private void InitializeAchievements(int playerId)
    {
        playerAchievements[playerId][AchievementType.FirstKill] = new Achievement 
        { type = AchievementType.FirstKill, title = "初来乍到", description = "获得第一次击杀" };
        playerAchievements[playerId][AchievementType.DoubleKill] = new Achievement 
        { type = AchievementType.DoubleKill, title = "双杀", description = "在一轮内获得2次击杀" };
        playerAchievements[playerId][AchievementType.TripleKill] = new Achievement 
        { type = AchievementType.TripleKill, title = "三杀", description = "在一轮内获得3次击杀" };
        playerAchievements[playerId][AchievementType.MultiKill] = new Achievement 
        { type = AchievementType.MultiKill, title = "五连杀", description = "在一轮内获得5次击杀" };
        playerAchievements[playerId][AchievementType.Headshot] = new Achievement 
        { type = AchievementType.Headshot, title = "爆头专家", description = "获得10个爆头击杀" };
        playerAchievements[playerId][AchievementType.Perfect] = new Achievement 
        { type = AchievementType.Perfect, title = "完美一轮", description = "一轮内不死亡并获得击杀" };
        playerAchievements[playerId][AchievementType.Defuser] = new Achievement 
        { type = AchievementType.Defuser, title = "拆除专家", description = "成功拆除5个炸弹" };
        playerAchievements[playerId][AchievementType.Clutcher] = new Achievement 
        { type = AchievementType.Clutcher, title = "翻盘王", description = "在1v5的局面下赢得回合" };
    }

    /// <summary>
    /// 解锁成就
    /// </summary>
    public void UnlockAchievement(int playerId, AchievementType type)
    {
        if (!playerAchievements.ContainsKey(playerId))
            RegisterPlayerAchievements(playerId);

        if (playerAchievements[playerId].ContainsKey(type))
        {
            Achievement achievement = playerAchievements[playerId][type];
            if (!achievement.unlocked)
            {
                achievement.unlocked = true;
                achievement.unlockedTime = (int)Time.time;
                Debug.Log($"[AchievementSystem] 玩家 {playerId} 解锁成就: {achievement.title}");
            }
        }
    }

    /// <summary>
    /// 获取玩家成就列表
    /// </summary>
    public Dictionary<AchievementType, Achievement> GetPlayerAchievements(int playerId)
    {
        return playerAchievements.ContainsKey(playerId) ? playerAchievements[playerId] : null;
    }

    /// <summary>
    /// 获取已解锁的成就数
    /// </summary>
    public int GetUnlockedCount(int playerId)
    {
        if (!playerAchievements.ContainsKey(playerId))
            return 0;
        return playerAchievements[playerId].Values.Count(a => a.unlocked);
    }
}
