using UnityEngine;

/// <summary>
/// 网络优化器 - 减少网络流量
/// </summary>
public class NetworkOptimizer
{
    private float updateInterval = 0.1f; // 10次/秒
    private float lastUpdateTime = 0f;
    private float positionThreshold = 0.1f; // 位置变化阈值
    private float rotationThreshold = 2f; // 旋转变化阈值

    private Vector3 lastSyncPosition = Vector3.zero;
    private Quaternion lastSyncRotation = Quaternion.identity;

    /// <summary>
    /// 检查是否应该更新网络数据
    /// </summary>
    public bool ShouldUpdate(Vector3 currentPosition, Quaternion currentRotation)
    {
        // 检查时间间隔
        if (Time.time - lastUpdateTime < updateInterval)
            return false;

        // 检查位置变化
        if (Vector3.Distance(currentPosition, lastSyncPosition) < positionThreshold)
            return false;

        // 检查旋转变化
        if (Quaternion.Angle(currentRotation, lastSyncRotation) < rotationThreshold)
            return false;

        lastUpdateTime = Time.time;
        lastSyncPosition = currentPosition;
        lastSyncRotation = currentRotation;
        return true;
    }
}
