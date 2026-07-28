using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 地图配置 - 定义地图的关键位置和规则
/// </summary>
public class MapConfig : MonoBehaviour
{
    [SerializeField] private string mapName = "Dust2";
    [SerializeField] private Transform ctSpawnArea;
    [SerializeField] private Transform tSpawnArea;
    [SerializeField] private Transform bombSiteA;
    [SerializeField] private Transform bombSiteB;
    [SerializeField] private List<Transform> bombPlantZones = new List<Transform>();

    private SpawnPoint[] ctSpawns;
    private SpawnPoint[] tSpawns;

    private void Awake()
    {
        CollectSpawnPoints();
    }

    /// <summary>
    /// 收集所有生成点
    /// </summary>
    private void CollectSpawnPoints()
    {
        SpawnPoint[] allSpawns = FindObjectsOfType<SpawnPoint>();
        List<SpawnPoint> ctSpawnList = new List<SpawnPoint>();
        List<SpawnPoint> tSpawnList = new List<SpawnPoint>();

        foreach (SpawnPoint spawn in allSpawns)
        {
            if (spawn.GetTeamId() == Constants.Team.TEAM_CT)
                ctSpawnList.Add(spawn);
            else if (spawn.GetTeamId() == Constants.Team.TEAM_T)
                tSpawnList.Add(spawn);
        }

        ctSpawns = ctSpawnList.ToArray();
        tSpawns = tSpawnList.ToArray();

        Debug.Log($"[MapConfig] 地图 {mapName} 已加载。CT生成点: {ctSpawns.Length}, T生成点: {tSpawns.Length}");
    }

    /// <summary>
    /// 获取队伍的随机生成点
    /// </summary>
    public Vector3 GetRandomSpawnPoint(int teamId)
    {
        SpawnPoint[] spawns = teamId == Constants.Team.TEAM_CT ? ctSpawns : tSpawns;

        if (spawns.Length == 0)
        {
            Debug.LogError($"[MapConfig] 找不到队伍 {teamId} 的生成点");
            return Vector3.zero;
        }

        SpawnPoint randomSpawn = spawns[Random.Range(0, spawns.Length)];
        return randomSpawn.transform.position;
    }

    /// <summary>
    /// 获取地图名称
    /// </summary>
    public string GetMapName() => mapName;

    /// <summary>
    /// 获取炸弹点A
    /// </summary>
    public Transform GetBombSiteA() => bombSiteA;

    /// <summary>
    /// 获取炸弹点B
    /// </summary>
    public Transform GetBombSiteB() => bombSiteB;
}
