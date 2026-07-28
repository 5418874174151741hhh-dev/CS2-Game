using UnityEngine;
using Photon.Pun;

/// <summary>
/// 网络初始化器 - 初始化网络游戏流程
/// </summary>
public class NetworkInitializer : MonoBehaviour
{
    [SerializeField] private string playerPrefabName = "Player";
    [SerializeField] private bool autoStartMatch = true;

    private void Start()
    {
        if (autoStartMatch && NetworkManager.Instance.IsMasterClient())
        {
            Debug.Log("[NetworkInitializer] 作为主客户端，启动比赛...");
            Invoke(nameof(StartNetworkMatch), 2f);
        }
    }

    /// <summary>
    /// 启动网络比赛
    /// </summary>
    private void StartNetworkMatch()
    {
        if (MatchManager.Instance != null)
        {
            MatchManager.Instance.StartMatch();
        }
    }

    /// <summary>
    /// 在网络上生成玩家
    /// </summary>
    public void SpawnNetworkPlayer(Vector3 spawnPosition, int teamId)
    {
        GameObject playerInstance = PhotonNetwork.Instantiate(
            playerPrefabName,
            spawnPosition,
            Quaternion.identity
        );

        Debug.Log($"[NetworkInitializer] 网络玩家已生成在 {spawnPosition}");
    }
}
