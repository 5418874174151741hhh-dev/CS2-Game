using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 计分板UI - 显示玩家统计和队伍信息
/// </summary>
public class ScoreboardUI : MonoBehaviour
{
    [SerializeField] private Text ctTeamScoreText;
    [SerializeField] private Text tTeamScoreText;
    [SerializeField] private Transform playerListContainer;
    [SerializeField] private GameObject playerScoreboardPrefab;
    [SerializeField] private bool showScoreboard = false;

    private void Update()
    {
        // 按TAB键显示/隐藏计分板
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleScoreboard();
        }

        if (showScoreboard)
        {
            UpdateScoreboard();
        }
    }

    /// <summary>
    /// 切换计分板显示
    /// </summary>
    private void ToggleScoreboard()
    {
        showScoreboard = !showScoreboard;
        gameObject.SetActive(showScoreboard);
        Debug.Log($"[ScoreboardUI] 计分板 {(showScoreboard ? "显示" : "隐藏")}");
    }

    /// <summary>
    /// 更新计分板
    /// </summary>
    private void UpdateScoreboard()
    {
        // 更新队伍分数
        int ctWins = GameManager.Instance.GetTeamRoundsWon(Constants.Team.TEAM_CT);
        int tWins = GameManager.Instance.GetTeamRoundsWon(Constants.Team.TEAM_T);

        if (ctTeamScoreText != null)
            ctTeamScoreText.text = $"CT: {ctWins}";
        if (tTeamScoreText != null)
            tTeamScoreText.text = $"T: {tWins}";

        // 更新玩家列表
        Player[] allPlayers = FindObjectsOfType<Player>();
        
        // 清空旧列表
        foreach (Transform child in playerListContainer)
        {
            Destroy(child.gameObject);
        }

        // 添加新玩家信息
        foreach (Player player in allPlayers)
        {
            if (playerScoreboardPrefab != null)
            {
                GameObject playerScoreLine = Instantiate(playerScoreboardPrefab, playerListContainer);
                // 这里应该有一个脚本来显示玩家信息
                // 暂时只是创建
            }
        }
    }
}
