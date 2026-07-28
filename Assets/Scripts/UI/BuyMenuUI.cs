using UnityEngine;

/// <summary>
/// 购买菜单UI
/// </summary>
public class BuyMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject buyMenuPanel;
    [SerializeField] private Transform weaponButtonContainer;
    [SerializeField] private float buyTimeRemaining = Constants.Round.BUY_TIME;
    
    private bool buyMenuActive = false;

    private void Start()
    {
        if (buyMenuPanel != null)
            buyMenuPanel.SetActive(false);
        Debug.Log("[BuyMenuUI] 购买菜单已初始化");
    }

    private void Update()
    {
        // 检查是否按了B键打开购买菜单
        if (InputManager.Instance.IsKeyDown("Buy"))
        {
            ToggleBuyMenu();
        }

        // 更新购买时间
        if (buyMenuActive)
        {
            buyTimeRemaining -= Time.deltaTime;
            if (buyTimeRemaining <= 0)
            {
                CloseBuyMenu();
            }
        }
    }

    /// <summary>
    /// 切换购买菜单
    /// </summary>
    private void ToggleBuyMenu()
    {
        buyMenuActive = !buyMenuActive;
        if (buyMenuPanel != null)
            buyMenuPanel.SetActive(buyMenuActive);

        Debug.Log($"[BuyMenuUI] 购买菜单 {(buyMenuActive ? "打开" : "关闭")}");
    }

    /// <summary>
    /// 关闭Buy菜单
    /// </summary>
    private void CloseBuyMenu()
    {
        buyMenuActive = false;
        if (buyMenuPanel != null)
            buyMenuPanel.SetActive(false);
        Debug.Log("[BuyMenuUI] 购买时间已结束");
    }

    /// <summary>
    /// 获取购买时间剩余
    /// </summary>
    public float GetTimeRemaining() => buyTimeRemaining;
}
