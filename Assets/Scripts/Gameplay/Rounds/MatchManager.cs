using UnityEngine;

/// <summary>
/// 比赛管理器 - 管理整个比赛流程
/// </summary>
public class MatchManager : SingletonManager<MatchManager>
{
    private int[] teamScores = new int[2]; // [CT, T]
    private bool matchActive = false;
    private int matchWinner = -1;

    protected override void Awake()
    {
        base.Awake();
        teamScores[0] = 0;
        teamScores[1] = 0;
    }

    /// <summary>
    /// 开始新比赛
    /// </summary>
    public void StartMatch()
    {
        matchActive = true;
        teamScores[0] = 0;
        teamScores[1] = 0;
        matchWinner = -1;

        Debug.Log("[MatchManager] 新比赛已开始");
        StartNewRound();
    }

    /// <summary>
    /// 开始新回合
    /// </summary>
    public void StartNewRound()
    {
        if (!matchActive)
            return;

        RoundManager.Instance.StartRound();
    }

    /// <summary>
    /// 回合结束处理
    /// </summary>
    public void OnRoundEnd(int winningTeam)
    {
        if (!matchActive)
            return;

        // 增加队伍分数
        teamScores[winningTeam]++;
        Debug.Log($"[MatchManager] 队伍 {winningTeam} 赢得回合。当前比分: CT {teamScores[0]} - T {teamScores[1]}");

        // 分配经济奖励
        EconomySystem.Instance.AwardTeamWin(winningTeam);

        // 检查比赛是否结束
        if (teamScores[winningTeam] >= Constants.Round.ROUNDS_TO_WIN)
        {
            EndMatch(winningTeam);
        }
        else
        {
            // 延迟后开始新回合
            Invoke(nameof(StartNewRound), Constants.Round.ROUND_END_TIME + 1);
        }
    }

    /// <summary>
    /// 结束比赛
    /// </summary>
    public void EndMatch(int winningTeam)
    {
        matchActive = false;
        matchWinner = winningTeam;

        string teamName = winningTeam == Constants.Team.TEAM_CT ? "CT" : "T";
        Debug.Log($"[MatchManager] 比赛结束！{teamName} 队伍获胜！");
        Debug.Log($"[MatchManager] 最终比分: CT {teamScores[0]} - T {teamScores[1]}");

        GameManager.Instance.SetGameState(Constants.GameState.GAME_OVER);
    }

    /// <summary>
    /// 获取队伍分数
    /// </summary>
    public int GetTeamScore(int teamId)
    {
        return teamId < 2 ? teamScores[teamId] : -1;
    }

    /// <summary>
    /// 获取比赛是否活跃
    /// </summary>
    public bool IsMatchActive() => matchActive;

    /// <summary>
    /// 获取比赛赢家
    /// </summary>
    public int GetMatchWinner() => matchWinner;
}
