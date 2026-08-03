using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 军备竞赛模式 - 每次击杀升级武器
/// </summary>
public class ArmsRaceMode : MonoBehaviour
{
    [SerializeField] private List<string> weaponProgression = new List<string>
    {
        "Glock", "USP", "P250", "Deagle",
        "AK-47", "M4A1", "AWP", "Knife"
    };

    [SerializeField] private int killsNeededToWin = 10;

    private Dictionary<int, int> playerWeaponIndex = new Dictionary<int, int>();
    private Dictionary<int, int> playerKillCount = new Dictionary<int, int>();
    private int matchWinner = -1;

    /// <summary>
    /// 启动军备竞赛模式
    /// </summary>
    public void StartMode()
    {
        Debug.Log("[ArmsRaceMode] 军备竞赛模式开始");
    }

    /// <summary>
    /// 玩家击杀事件
    /// </summary>
    public void OnPlayerKill(int playerId)
    {
        if (!playerKillCount.ContainsKey(playerId))
        {
            playerKillCount[playerId] = 0;
            playerWeaponIndex[playerId] = 0;
        }

        playerKillCount[playerId]++;

        // 升级武器
        if (playerWeaponIndex[playerId] < weaponProgression.Count - 1)
        {
            playerWeaponIndex[playerId]++;
            string nextWeapon = weaponProgression[playerWeaponIndex[playerId]];
            Debug.Log($"[ArmsRaceMode] 玩家 {playerId} 升级到: {nextWeapon}");
        }

        // 检查胜利
        if (playerKillCount[playerId] >= killsNeededToWin)
        {
            EndMode(playerId);
        }
    }

    /// <summary>
    /// 结束模式
    /// </summary>
    private void EndMode(int winnerId)
    {
        matchWinner = winnerId;
        Debug.Log($"[ArmsRaceMode] 玩家 {winnerId} 赢得军备竞赛！");
    }

    /// <summary>
    /// 获取玩家当前武器
    /// </summary>
    public string GetPlayerWeapon(int playerId)
    {
        if (!playerWeaponIndex.ContainsKey(playerId))
            return weaponProgression[0];
        return weaponProgression[playerWeaponIndex[playerId]];
    }
}
