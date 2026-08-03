using UnityEngine;

/// <summary>
/// 死斗模式 - 无限复活，仅计分
/// </summary>
public class DeathmatchMode : MonoBehaviour
{
    [SerializeField] private float respawnTime = 5f;
    [SerializeField] private bool friendlyFire = false;
    [SerializeField] private int roundTimeLimit = 600; // 10分钟

    private int ctKills = 0;
    private int tKills = 0;
    private float modeTimer = 0f;
    private bool modeActive = false;

    private void Start()
    {
        Debug.Log("[DeathmatchMode] 死斗模式已启动");
    }

    private void Update()
    {
        if (!modeActive)
            return;

        modeTimer += Time.deltaTime;
        if (modeTimer >= roundTimeLimit)
        {
            EndMode();
        }
    }

    /// <summary>
    /// 启动死斗模式
    /// </summary>
    public void StartMode()
    {
        modeActive = true;
        modeTimer = 0f;
        ctKills = 0;
        tKills = 0;
        Debug.Log("[DeathmatchMode] 死斗模式开始");
    }

    /// <summary>
    /// 玩家被杀死
    /// </summary>
    public void OnPlayerKilled(int killerId, int victimId, int team)
    {
        if (team == Constants.Team.TEAM_CT)
            ctKills++;
        else
            tKills++;

        // 玩家自动复活
        Player victim = FindObjectsOfType<Player>().FirstOrDefault(p => p.GetPlayerId() == victimId);
        if (victim != null)
        {
            Invoke(nameof(RespawnPlayer), respawnTime);
        }

        Debug.Log($"[DeathmatchMode] CT: {ctKills} | T: {tKills}");
    }

    /// <summary>
    /// 复活玩家
    /// </summary>
    private void RespawnPlayer()
    {
        // 复活逻辑
    }

    /// <summary>
    /// 结束模式
    /// </summary>
    private void EndMode()
    {
        modeActive = false;
        string winner = ctKills > tKills ? "CT" : "T";
        Debug.Log($"[DeathmatchMode] 模式结束！{winner}队获胜！CT: {ctKills} | T: {tKills}");
    }
}
