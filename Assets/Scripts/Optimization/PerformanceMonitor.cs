using UnityEngine;

/// <summary>
/// 性能监控 - 监控FPS和内存使用
/// </summary>
public class PerformanceMonitor : SingletonManager<PerformanceMonitor>
{
    [SerializeField] private bool showPerformanceStats = true;
    [SerializeField] private int targetFrameRate = 60;

    private int frameCount = 0;
    private float deltaTime = 0f;
    private float fps = 0f;
    private float updateInterval = 0.5f;
    private float lastUpdateTime = 0f;

    protected override void Awake()
    {
        base.Awake();
        Application.targetFrameRate = targetFrameRate;
    }

    private void Update()
    {
        frameCount++;
        deltaTime += Time.deltaTime;

        if (deltaTime >= updateInterval)
        {
            fps = frameCount / deltaTime;
            frameCount = 0;
            deltaTime = 0f;
        }
    }

    private void OnGUI()
    {
        if (!showPerformanceStats)
            return;

        GUILayout.BeginArea(new Rect(Screen.width - 150, 10, 140, 100));
        GUILayout.Label($"FPS: {fps:F1}", new GUIStyle(GUI.skin.label) { fontSize = 12, normal = { textColor = Color.white } });
        GUILayout.Label($"内存: {System.GC.GetTotalMemory(false) / (1024 * 1024)}MB");
        GUILayout.Label($"目标FPS: {targetFrameRate}");
        GUILayout.EndArea();
    }

    /// <summary>
    /// 获取当前FPS
    /// </summary>
    public float GetFPS() => fps;
}
