using UnityEngine;

/// <summary>
/// Ping系统 - 显示网络延迟和连接质量
/// </summary>
public class PingSystem : MonoBehaviourPun
{
    [SerializeField] private float updateInterval = 1f;
    private float lastPingTime = 0f;
    private int currentPing = 0;
    private int maxPing = 9999;

    private void Update()
    {
        if (Time.time - lastPingTime >= updateInterval)
        {
            UpdatePing();
            lastPingTime = Time.time;
        }
    }

    /// <summary>
    /// 更新Ping值
    /// </summary>
    private void UpdatePing()
    {
        if (PhotonNetwork.IsConnected)
        {
            currentPing = PhotonNetwork.GetPing();
            Debug.Log($"[PingSystem] 当前Ping: {currentPing}ms");
        }
    }

    /// <summary>
    /// 获取当前Ping
    /// </summary>
    public int GetCurrentPing() => currentPing;

    /// <summary>
    /// 获取连接质量
    /// </summary>
    public string GetConnectionQuality()
    {
        if (currentPing < 50)
            return "优秀";
        else if (currentPing < 100)
            return "良好";
        else if (currentPing < 200)
            return "中等";
        else if (currentPing < 300)
            return "较差";
        else
            return "非常差";
    }
}
