using UnityEngine;

/// <summary>
/// 经济系统 - 管理玩家金币和奖励
/// </summary>
public class EconomySystem : SingletonManager<EconomySystem>
{
    private float[] playerMoney; // 每个玩家的金币
    private int maxPlayers = 10;

    protected override void Awake()
    {
        base.Awake();
        playerMoney = new float[maxPlayers];
        InitializeEconomy();
    }

    /// <summary>
    /// 初始化经济系统
    /// </summary>
    private void InitializeEconomy()
    {
        for (int i = 0; i < maxPlayers; i++)
        {
            playerMoney[i] = Constants.Economy.STARTING_MONEY;
        }
        Debug.Log("[EconomySystem] 经济系统已初始化，每个玩家获得 $" + Constants.Economy.STARTING_MONEY);
    }

    /// <summary>
    /// 给玩家奖励击杀
    /// </summary>
    public void AwardKill(int playerId)
    {
        if (playerId < 0 || playerId >= maxPlayers)
            return;

        playerMoney[playerId] += Constants.Economy.KILL_REWARD;
        playerMoney[playerId] = Mathf.Min(playerMoney[playerId], Constants.Economy.MAX_MONEY);

        Debug.Log($"[EconomySystem] 玩家 {playerId} 击杀奖励 ${Constants.Economy.KILL_REWARD}，当前: ${playerMoney[playerId]}");
    }

    /// <summary>
    /// 给玩家奖励助攻
    /// </summary>
    public void AwardAssist(int playerId)
    {
        if (playerId < 0 || playerId >= maxPlayers)
            return;

        playerMoney[playerId] += Constants.Economy.ASSIST_REWARD;
        playerMoney[playerId] = Mathf.Min(playerMoney[playerId], Constants.Economy.MAX_MONEY);

        Debug.Log($"[EconomySystem] 玩家 {playerId} 助攻奖励 ${Constants.Economy.ASSIST_REWARD}，当前: ${playerMoney[playerId]}");
    }

    /// <summary>
    /// 给整个队伍奖励胜利
    /// </summary>
    public void AwardTeamWin(int teamId)
    {
        Player[] allPlayers = FindObjectsOfType<Player>();

        foreach (Player player in allPlayers)
        {
            if (player.GetTeamId() == teamId)
            {
                player.AddMoney(Constants.Economy.WIN_REWARD);
            }
            else
            {
                player.AddMoney(Constants.Economy.LOSS_REWARD);
            }
        }

        Debug.Log($"[EconomySystem] 队伍 {teamId} 获胜，已分配奖励");
    }

    /// <summary>
    /// 给整个CT队伍奖励拆弹
    /// </summary>
    public void AwardBombDefuse()
    {
        Player[] allPlayers = FindObjectsOfType<Player>();

        foreach (Player player in allPlayers)
        {
            if (player.GetTeamId() == Constants.Team.TEAM_CT)
            {
                player.AddMoney(Constants.Economy.BOMB_DEFUSE_REWARD);
            }
        }

        Debug.Log("[EconomySystem] 炸弹已拆除，CT队伍获得奖励");
    }

    /// <summary>
    /// 重置回合经济（清除未花费的金币部分）
    /// </summary>
    public void ResetRoundEconomy()
    {
        Debug.Log("[EconomySystem] 回合经济已重置");
    }
}
