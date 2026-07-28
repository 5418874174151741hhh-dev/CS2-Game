using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 游戏管理器 - 主要游戏逻辑控制
/// </summary>
public class GameManager : SingletonManager<GameManager>
{
    [SerializeField] private string currentGameState = Constants.GameState.MENU;
    [SerializeField] private int currentRound = 1;
    [SerializeField] private int ctRoundsWon = 0;
    [SerializeField] private int tRoundsWon = 0;

    private Dictionary<string, System.Action> gameStateCallbacks = new Dictionary<string, System.Action>();

    protected override void Awake()
    {
        base.Awake();
        InitializeGame();
    }

    private void Start()
    {
        Debug.Log("[GameManager] 游戏已初始化");
    }

    /// <summary>
    /// 初始化游戏
    /// </summary>
    private void InitializeGame()
    {
        Debug.Log("[GameManager] 初始化游戏...");
        SetGameState(Constants.GameState.MENU);
    }

    /// <summary>
    /// 设置游戏状态
    /// </summary>
    public void SetGameState(string newState)
    {
        if (currentGameState == newState)
            return;

        Debug.Log($"[GameManager] 游戏状态变更: {currentGameState} -> {newState}");
        currentGameState = newState;

        // 触发状态变更回调
        if (gameStateCallbacks.ContainsKey(newState))
        {
            gameStateCallbacks[newState]?.Invoke();
        }
    }

    /// <summary>
    /// 获取当前游戏状态
    /// </summary>
    public string GetGameState() => currentGameState;

    /// <summary>
    /// 注册状态变更回调
    /// </summary>
    public void RegisterStateCallback(string state, System.Action callback)
    {
        if (!gameStateCallbacks.ContainsKey(state))
        {
            gameStateCallbacks[state] = null;
        }
        gameStateCallbacks[state] += callback;
    }

    /// <summary>
    /// 开始新回合
    /// </summary>
    public void StartNewRound()
    {
        Debug.Log($"[GameManager] 开始第 {currentRound} 回合");
        SetGameState(Constants.GameState.PLAYING);
    }

    /// <summary>
    /// 结束当前回合
    /// </summary>
    public void EndRound(int winningTeam)
    {
        if (winningTeam == Constants.Team.TEAM_CT)
        {
            ctRoundsWon++;
            Debug.Log($"[GameManager] CT 队赢得回合！ CT: {ctRoundsWon} vs T: {tRoundsWon}");
        }
        else if (winningTeam == Constants.Team.TEAM_T)
        {
            tRoundsWon++;
            Debug.Log($"[GameManager] T 队赢得回合！ CT: {ctRoundsWon} vs T: {tRoundsWon}");
        }

        SetGameState(Constants.GameState.ROUND_END);

        // 检查比赛是否结束
        if (ctRoundsWon >= Constants.Round.ROUNDS_TO_WIN)
        {
            EndGame(Constants.Team.TEAM_CT);
        }
        else if (tRoundsWon >= Constants.Round.ROUNDS_TO_WIN)
        {
            EndGame(Constants.Team.TEAM_T);
        }
        else
        {
            currentRound++;
            Invoke(nameof(StartNewRound), Constants.Round.ROUND_END_TIME);
        }
    }

    /// <summary>
    /// 结束游戏
    /// </summary>
    public void EndGame(int winningTeam)
    {
        string teamName = winningTeam == Constants.Team.TEAM_CT ? "CT" : "T";
        Debug.Log($"[GameManager] 游戏结束！{teamName} 队获胜！");
        SetGameState(Constants.GameState.GAME_OVER);
    }

    /// <summary>
    /// 获取当前回合
    /// </summary>
    public int GetCurrentRound() => currentRound;

    /// <summary>
    /// 获取队伍赢得的回合数
    /// </summary>
    public int GetTeamRoundsWon(int team)
    {
        return team == Constants.Team.TEAM_CT ? ctRoundsWon : tRoundsWon;
    }
}
