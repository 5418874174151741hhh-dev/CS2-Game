using UnityEngine;

/// <summary>
/// 游戏插件速记 - 便于需要部分预制体名称
/// </summary>
public class GameInitializer : MonoBehaviour
{
    public static void InitializeGame()
    {
        Debug.Log("[GameInitializer] 游戏正在初始化...");

        // 确保存在GameManager
        if (GameManager.Instance == null)
        {
            Debug.LogError("[GameInitializer] GameManager 发隙了！");
            return;
        }

        // 确保存在InputManager
        if (InputManager.Instance == null)
        {
            Debug.LogError("[GameInitializer] InputManager 发隙了！");
            return;
        }

        // 确保存在AudioManager
        if (AudioManager.Instance == null)
        {
            Debug.LogError("[GameInitializer] AudioManager 发隙了！");
            return;
        }

        Debug.Log("[GameInitializer] 游戏初始化完成！每个管理器都已就位。");
    }
}
