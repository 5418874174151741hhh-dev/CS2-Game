using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 输入管理器 - 处理所有玩家输入
/// </summary>
public class InputManager : SingletonManager<InputManager>
{
    private Dictionary<string, KeyCode> keyBindings = new Dictionary<string, KeyCode>();

    protected override void Awake()
    {
        base.Awake();
        InitializeKeyBindings();
    }

    private void InitializeKeyBindings()
    {
        // 初始化默认按键绑定
        keyBindings["Forward"] = KeyCode.W;
        keyBindings["Backward"] = KeyCode.S;
        keyBindings["Left"] = KeyCode.A;
        keyBindings["Right"] = KeyCode.D;
        keyBindings["Jump"] = KeyCode.Space;
        keyBindings["Crouch"] = KeyCode.LeftControl;
        keyBindings["Sprint"] = KeyCode.LeftShift;
        keyBindings["Fire"] = KeyCode.Mouse0;
        keyBindings["Aim"] = KeyCode.Mouse1;
        keyBindings["Reload"] = KeyCode.R;
        keyBindings["Buy"] = KeyCode.B;
        keyBindings["Knife"] = KeyCode.K;
        keyBindings["Grenade"] = KeyCode.G;
        keyBindings["Escape"] = KeyCode.Escape;

        Debug.Log("[InputManager] 按键绑定已初始化");
    }

    /// <summary>
    /// 检查按键是否被按下
    /// </summary>
    public bool IsKeyDown(string action)
    {
        if (keyBindings.ContainsKey(action))
        {
            return Input.GetKeyDown(keyBindings[action]);
        }
        Debug.LogWarning($"[InputManager] 未找到动作: {action}");
        return false;
    }

    /// <summary>
    /// 检查按键是否被按住
    /// </summary>
    public bool IsKeyHeld(string action)
    {
        if (keyBindings.ContainsKey(action))
        {
            return Input.GetKey(keyBindings[action]);
        }
        Debug.LogWarning($"[InputManager] 未找到动作: {action}");
        return false;
    }

    /// <summary>
    /// 检查按键是否被抬起
    /// </summary>
    public bool IsKeyUp(string action)
    {
        if (keyBindings.ContainsKey(action))
        {
            return Input.GetKeyUp(keyBindings[action]);
        }
        Debug.LogWarning($"[InputManager] 未找到动作: {action}");
        return false;
    }

    /// <summary>
    /// 获取鼠标位置
    /// </summary>
    public Vector3 GetMousePosition() => Input.mousePosition;

    /// <summary>
    /// 获取鼠标Delta
    /// </summary>
    public Vector2 GetMouseDelta() => new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

    /// <summary>
    /// 重新绑定按键
    /// </summary>
    public void RebindKey(string action, KeyCode newKey)
    {
        if (keyBindings.ContainsKey(action))
        {
            keyBindings[action] = newKey;
            Debug.Log($"[InputManager] {action} 已重新绑定为 {newKey}");
        }
    }

    /// <summary>
    /// 获取按键绑定
    /// </summary>
    public KeyCode GetKeyBinding(string action)
    {
        return keyBindings.ContainsKey(action) ? keyBindings[action] : KeyCode.None;
    }
}
