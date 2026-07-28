using UnityEngine;
using Photon.Pun;

/// <summary>
/// 回合网络同步 - 同步回合状态
/// </summary>
public class RoundNetworkSync : MonoBehaviourPun
{
    /// <summary>
    /// 同步回合开始
    /// </summary>
    public void SyncRoundStart(int roundNumber)
    {
        photonView.RPC(nameof(OnRoundStart), RpcTarget.AllBuffered, roundNumber);
    }

    /// <summary>
    /// 同步回合结束
    /// </summary>
    public void SyncRoundEnd(int winningTeam)
    {
        photonView.RPC(nameof(OnRoundEnd), RpcTarget.AllBuffered, winningTeam);
    }

    /// <summary>
    /// 同步比赛开始
    /// </summary>
    public void SyncMatchStart()
    {
        photonView.RPC(nameof(OnMatchStart), RpcTarget.AllBuffered);
    }

    /// <summary>
    /// 同步比赛结束
    /// </summary>
    public void SyncMatchEnd(int winningTeam)
    {
        photonView.RPC(nameof(OnMatchEnd), RpcTarget.AllBuffered, winningTeam);
    }

    // ===== RPC 回调 =====

    [PunRPC]
    private void OnRoundStart(int roundNumber)
    {
        Debug.Log($"[RoundNetworkSync] 回合 {roundNumber} 开始");
        GameManager.Instance.SetGameState(Constants.GameState.PLAYING);
    }

    [PunRPC]
    private void OnRoundEnd(int winningTeam)
    {
        string teamName = winningTeam == Constants.Team.TEAM_CT ? "CT" : "T";
        Debug.Log($"[RoundNetworkSync] {teamName} 队赢得回合");
        RoundManager.Instance.ForceEndRound(winningTeam);
    }

    [PunRPC]
    private void OnMatchStart()
    {
        Debug.Log("[RoundNetworkSync] 比赛开始");
        if (MatchManager.Instance != null)
        {
            MatchManager.Instance.StartMatch();
        }
    }

    [PunRPC]
    private void OnMatchEnd(int winningTeam)
    {
        string teamName = winningTeam == Constants.Team.TEAM_CT ? "CT" : "T";
        Debug.Log($"[RoundNetworkSync] 比赛结束！{teamName} 队获胜");
        if (MatchManager.Instance != null)
        {
            MatchManager.Instance.EndMatch(winningTeam);
        }
    }
}
