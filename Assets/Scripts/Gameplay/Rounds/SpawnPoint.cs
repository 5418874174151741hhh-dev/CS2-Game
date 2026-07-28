using UnityEngine;

/// <summary>
/// 玩家生成点 - 定义玩家出生位置
/// </summary>
public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private int teamId = Constants.Team.TEAM_CT;
    [SerializeField] private int spawnIndex = 0;
    [SerializeField] private bool occupied = false;

    private void OnDrawGizmos()
    {
        // 在编辑器中显示生成点
        Color color = teamId == Constants.Team.TEAM_CT ? Color.blue : Color.red;
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, 0.5f);
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
    }

    /// <summary>
    /// 获取队伍ID
    /// </summary>
    public int GetTeamId() => teamId;

    /// <summary>
    /// 获取生成点索引
    /// </summary>
    public int GetSpawnIndex() => spawnIndex;

    /// <summary>
    /// 设置占用状态
    /// </summary>
    public void SetOccupied(bool isOccupied) => occupied = isOccupied;

    /// <summary>
    /// 是否被占用
    /// </summary>
    public bool IsOccupied() => occupied;
}
