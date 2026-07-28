using UnityEngine;

/// <summary>
/// 游戏调试和控制面板
/// </summary>
public class GameDebugPanel : MonoBehaviour
{
    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 400));
        
        GUILayout.Label("=== CS2 游戏调试面板 ===", new GUIStyle(GUI.skin.label) { fontSize = 16 });
        
        // 游戏状态
        GUILayout.Label($"游戏状态: {GameManager.Instance.GetGameState()}");
        GUILayout.Label($"当前回合: {GameManager.Instance.GetCurrentRound()}");
        if (RoundManager.Instance != null)
        {
            GUILayout.Label($"当前阶段: {RoundManager.Instance.GetCurrentPhase()}");
            GUILayout.Label($"阶段时间: {RoundManager.Instance.GetPhaseTimeRemaining():F1}s");
        }
        
        GUILayout.Space(10);
        
        // 比分
        GUILayout.Label("=== 比分 ===");
        if (MatchManager.Instance != null)
        {
            int ctScore = MatchManager.Instance.GetTeamScore(Constants.Team.TEAM_CT);
            int tScore = MatchManager.Instance.GetTeamScore(Constants.Team.TEAM_T);
            GUILayout.Label($"CT: {ctScore} - T: {tScore}");
        }
        
        GUILayout.Space(10);
        
        // 控制按钮
        GUILayout.Label("=== 控制 ===");
        
        if (GUILayout.Button("开始新比赛", GUILayout.Height(30)))
        {
            if (MatchManager.Instance != null)
                MatchManager.Instance.StartMatch();
        }
        
        if (GUILayout.Button("结束当前回合 (CT赢)", GUILayout.Height(30)))
        {
            if (RoundManager.Instance != null)
                RoundManager.Instance.ForceEndRound(Constants.Team.TEAM_CT);
        }
        
        if (GUILayout.Button("结束当前回合 (T赢)", GUILayout.Height(30)))
        {
            if (RoundManager.Instance != null)
                RoundManager.Instance.ForceEndRound(Constants.Team.TEAM_T);
        }
        
        GUILayout.Space(10);
        
        // 玩家信息
        GUILayout.Label("=== 玩家信息 ===");
        Player[] allPlayers = FindObjectsOfType<Player>();
        foreach (Player player in allPlayers)
        {
            PlayerStats stats = player.GetStats();
            string teamName = player.GetTeamId() == Constants.Team.TEAM_CT ? "CT" : "T";
            GUILayout.Label($"玩家 {player.GetPlayerId()} ({teamName}): HP={stats.Health} 金币=${stats.Money}");
        }
        
        GUILayout.EndArea();
    }
}
