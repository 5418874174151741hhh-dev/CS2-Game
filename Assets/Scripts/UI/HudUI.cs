using UnityEngine;
using UnityEngine.UI;

/// <summary>
HUD显示 - 游戏页面信息
/// </summary>
public class HudUI : MonoBehaviour
{
    [SerializeField] private Text healthText;
    [SerializeField] private Text armorText;
    [SerializeField] private Text moneyText;
    [SerializeField] private Text ammoText;
    [SerializeField] private Text roundText;
    [SerializeField] private Text timerText;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image armorBar;

    private Player localPlayer;
    private float roundTimer = 0f;

    private void Start()
    {
        // 找到本地玩家
        localPlayer = FindObjectOfType<Player>();
        if (localPlayer == null)
        {
            Debug.LogError("[HudUI] 找不到本地玩家！");
        }
        Debug.Log("[HudUI] HUD已初始化");
    }

    private void Update()
    {
        if (localPlayer == null || !localPlayer.IsAlive())
            return;

        UpdateHUD();
        roundTimer += Time.deltaTime;
    }

    /// <summary>
    /// 更新HUD显示
    /// </summary>
    private void UpdateHUD()
    {
        PlayerStats stats = localPlayer.GetStats();

        // 更新血量
        if (healthText != null)
            healthText.text = $"HP: {stats.Health}";
        if (healthBar != null)
            healthBar.fillAmount = stats.Health / Constants.Player.MAX_HEALTH;

        // 更新护甲
        if (armorText != null)
            armorText.text = $"Armor: {stats.Armor}";
        if (armorBar != null)
            armorBar.fillAmount = stats.Armor / Constants.Player.MAX_ARMOR;

        // 更新金币
        if (moneyText != null)
            moneyText.text = $"${stats.Money:F0}";

        // 更新回合信息
        if (roundText != null)
        {
            int ctWins = GameManager.Instance.GetTeamRoundsWon(Constants.Team.TEAM_CT);
            int tWins = GameManager.Instance.GetTeamRoundsWon(Constants.Team.TEAM_T);
            roundText.text = $"Round {GameManager.Instance.GetCurrentRound()} | CT: {ctWins} - T: {tWins}";
        }

        // 更新计时器
        if (timerText != null)
        {
            int minutes = (int)roundTimer / 60;
            int seconds = (int)roundTimer % 60;
            timerText.text = $"{minutes:D2}:{seconds:D2}";
        }
    }
}
