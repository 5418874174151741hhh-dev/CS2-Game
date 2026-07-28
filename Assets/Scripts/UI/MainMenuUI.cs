using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 主菜单UI
/// </summary>
public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private GameObject settingsPanel;

    private void Start()
    {
        if (startButton != null)
            startButton.onClick.AddListener(OnStartGame);
        if (settingsButton != null)
            settingsButton.onClick.AddListener(OnOpenSettings);
        if (exitButton != null)
            exitButton.onClick.AddListener(OnExit);

        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        Debug.Log("[MainMenuUI] 主菜单UI已初始化");
    }

    /// <summary>
    /// 开始游戏
    /// </summary>
    private void OnStartGame()
    {
        Debug.Log("[MainMenuUI] 开始游戏！");
        GameManager.Instance.StartNewRound();
    }

    /// <summary>
    /// 打开设置
    /// </summary>
    private void OnOpenSettings()
    {
        Debug.Log("[MainMenuUI] 打开设置");
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    /// <summary>
    /// 退出游戏
    /// </summary>
    private void OnExit()
    {
        Debug.Log("[MainMenuUI] 退出游戏");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
