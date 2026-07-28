using UnityEngine;
using Photon.Pun;

/// <summary>
/// 事件同步 - 网络广播游戏事件
/// </summary>
public class EventSync : MonoBehaviourPun
{
    /// <summary>
    /// 广播射击事件
    /// </summary>
    public void BroadcastFireEvent(Vector3 firePosition, Vector3 fireDirection)
    {
        photonView.RPC(nameof(OnPlayerFired), RpcTarget.AllBuffered, firePosition, fireDirection);
    }

    /// <summary>
    /// 广播伤害事件
    /// </summary>
    public void BroadcastDamageEvent(int attackerId, int victimId, float damage, bool isHeadshot)
    {
        photonView.RPC(nameof(OnPlayerDamaged), RpcTarget.AllBuffered, attackerId, victimId, damage, isHeadshot);
    }

    /// <summary>
    /// 广播死亡事件
    /// </summary>
    public void BroadcastDeathEvent(int victimId, int killerId, bool isHeadshot)
    {
        photonView.RPC(nameof(OnPlayerDied), RpcTarget.AllBuffered, victimId, killerId, isHeadshot);
    }

    /// <summary>
    /// 广播爆弹事件
    /// </summary>
    public void BroadcastBombEvent(string eventType, Vector3 bombPosition)
    {
        photonView.RPC(nameof(OnBombEvent), RpcTarget.AllBuffered, eventType, bombPosition);
    }

    // ===== RPC 回调 =====

    [PunRPC]
    private void OnPlayerFired(Vector3 firePosition, Vector3 fireDirection)
    {
        Debug.Log($"[EventSync] 玩家在 {firePosition} 射击，方向 {fireDirection}");
        // 播放枪口闪光、音效等
    }

    [PunRPC]
    private void OnPlayerDamaged(int attackerId, int victimId, float damage, bool isHeadshot)
    {
        string damageType = isHeadshot ? "头部" : "身体";
        Debug.Log($"[EventSync] 玩家 {attackerId} 伤害了玩家 {victimId} ({damageType}): {damage:F2} 伤害");
    }

    [PunRPC]
    private void OnPlayerDied(int victimId, int killerId, bool isHeadshot)
    {
        string deathType = isHeadshot ? "爆头" : "击杀";
        Debug.Log($"[EventSync] 玩家 {victimId} 被玩家 {killerId} {deathType}了");
    }

    [PunRPC]
    private void OnBombEvent(string eventType, Vector3 bombPosition)
    {
        Debug.Log($"[EventSync] 爆弹事件: {eventType} 在 {bombPosition}");
        // 播放爆炸音效、特效等
    }
}
