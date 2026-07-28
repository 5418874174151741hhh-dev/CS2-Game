using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 回合管理器 - 管理游戏回合流程
/// </summary>
public class RoundManager : SingletonManager<RoundManager>
{
    [SerializeField] private float buyPhaseTime = Constants.Round.BUY_TIME;
    [SerializeField] private float battlePhaseTime = Constants.Round.BATTLE_TIME;
    [SerializeField] private float roundEndPhaseTime = Constants.Round.ROUND_END_TIME;

    private enum RoundPhase { BUY, BATTLE, END }
    private RoundPhase currentPhase = RoundPhase.BUY;
    private float phaseTimer = 0f;
    private bool roundActive = false;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        if (!roundActive)
            return;

        phaseTimer += Time.deltaTime;
        UpdatePhase();
    }

    /// <summary>
    /// 开始新回合
    /// </summary>
    public void StartRound()
    {
        roundActive = true;
        currentPhase = RoundPhase.BUY;
        phaseTimer = 0f;

        Debug.Log($"[RoundManager] 第 {GameManager.Instance.GetCurrentRound()} 回合开始 - 购买阶段");
        GameManager.Instance.SetGameState(Constants.GameState.PLAYING);
    }

    /// <summary>
    /// 更新回合阶段
    /// </summary>
    private void UpdatePhase()
    {
        switch (currentPhase)
        {
            case RoundPhase.BUY:
                if (phaseTimer >= buyPhaseTime)
                {
                    TransitionToBattle();
                }
                break;

            case RoundPhase.BATTLE:
                if (phaseTimer >= buyPhaseTime + battlePhaseTime)
                {
                    TransitionToEnd();
                }
                break;

            case RoundPhase.END:
                if (phaseTimer >= buyPhaseTime + battlePhaseTime + roundEndPhaseTime)
                {
                    EndRound();
                }
                break;
        }
    }

    /// <summary>
    /// 转换到战斗阶段
    /// </summary>
    private void TransitionToBattle()
    {
        currentPhase = RoundPhase.BATTLE;
        Debug.Log("[RoundManager] 进入战斗阶段");
    }

    /// <summary>
    /// 转换到回合结束阶段
    /// </summary>
    private void TransitionToEnd()
    {
        currentPhase = RoundPhase.END;
        Debug.Log("[RoundManager] 进入回合结束阶段");
    }

    /// <summary>
    /// 结束回合
    /// </summary>
    public void EndRound()
    {
        roundActive = false;
        Debug.Log("[RoundManager] 回合已结束");
        GameManager.Instance.SetGameState(Constants.GameState.ROUND_END);
    }

    /// <summary>
    /// 获取当前阶段时间
    /// </summary>
    public float GetPhaseTimeRemaining()
    {
        switch (currentPhase)
        {
            case RoundPhase.BUY:
                return Mathf.Max(0, buyPhaseTime - phaseTimer);
            case RoundPhase.BATTLE:
                return Mathf.Max(0, battlePhaseTime - (phaseTimer - buyPhaseTime));
            case RoundPhase.END:
                return Mathf.Max(0, roundEndPhaseTime - (phaseTimer - buyPhaseTime - battlePhaseTime));
        }
        return 0f;
    }

    /// <summary>
    /// 获取当前阶段
    /// </summary>
    public string GetCurrentPhase()
    {
        return currentPhase.ToString();
    }

    /// <summary>
    /// 强制结束为某队获胜
    /// </summary>
    public void ForceEndRound(int winningTeam)
    {
        roundActive = false;
        GameManager.Instance.EndRound(winningTeam);
    }
}
