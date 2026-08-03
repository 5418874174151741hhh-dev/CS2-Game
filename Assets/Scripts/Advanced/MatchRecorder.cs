using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 比赛录制系统 - 记录并回放比赛
/// </summary>
public class MatchRecorder : SingletonManager<MatchRecorder>
{
    [System.Serializable]
    public class RecordedAction
    {
        public float time;
        public string action; // "fire", "damage", "death", "bomb_plant", etc
        public int playerId;
        public Vector3 position;
        public Vector3 targetPosition;
    }

    [System.Serializable]
    public class RecordedMatch
    {
        public string matchId;
        public float recordedDuration;
        public int roundCount;
        public List<RecordedAction> actions = new List<RecordedAction>();
    }

    private RecordedMatch currentRecording;
    private float recordingStartTime;
    private bool isRecording = false;

    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// 开始录制
    /// </summary>
    public void StartRecording()
    {
        currentRecording = new RecordedMatch
        {
            matchId = System.Guid.NewGuid().ToString(),
            recordedDuration = 0f
        };
        recordingStartTime = Time.time;
        isRecording = true;
        Debug.Log($"[MatchRecorder] 比赛录制已启动: {currentRecording.matchId}");
    }

    /// <summary>
    /// 记录动作
    /// </summary>
    public void RecordAction(string action, int playerId, Vector3 position, Vector3 targetPosition = default)
    {
        if (!isRecording || currentRecording == null)
            return;

        RecordedAction recordedAction = new RecordedAction
        {
            time = Time.time - recordingStartTime,
            action = action,
            playerId = playerId,
            position = position,
            targetPosition = targetPosition
        };

        currentRecording.actions.Add(recordedAction);
    }

    /// <summary>
    /// 停止录制
    /// </summary>
    public RecordedMatch StopRecording()
    {
        if (!isRecording || currentRecording == null)
            return null;

        isRecording = false;
        currentRecording.recordedDuration = Time.time - recordingStartTime;
        Debug.Log($"[MatchRecorder] 比赛录制已停止，时长: {currentRecording.recordedDuration}秒, 动作数: {currentRecording.actions.Count}");
        return currentRecording;
    }

    /// <summary>
    /// 保存录制
    /// </summary>
    public void SaveRecording(RecordedMatch match, string fileName)
    {
        string json = JsonUtility.ToJson(match, true);
        string path = $"Assets/StreamingAssets/Replays/{fileName}.json";
        System.IO.File.WriteAllText(path, json);
        Debug.Log($"[MatchRecorder] 录制已保存: {path}");
    }
}
