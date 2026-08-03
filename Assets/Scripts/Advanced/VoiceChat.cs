using UnityEngine;
using Photon.Pun;

/// <summary>
/// 语音聊天系统 - 支持队伍和全局语音
/// </summary>
public class VoiceChat : MonoBehaviourPun
{
    [SerializeField] private bool voiceEnabled = true;
    [SerializeField] private float masterVolume = 1f;
    [SerializeField] private bool isTransmitting = false;

    private AudioSource voiceAudioSource;
    private Microphone mic;

    private void Start()
    {
        voiceAudioSource = gameObject.AddComponent<AudioSource>();
        voiceAudioSource.spatialBlend = 1f; // 3D音频
        voiceAudioSource.maxDistance = 50f;

        if (voiceEnabled && Microphone.devices.Length > 0)
        {
            Debug.Log($"[VoiceChat] 可用麦克风: {string.Join(", ", Microphone.devices)}");
        }
    }

    private void Update()
    {
        // 按V键启用语音传输
        if (Input.GetKeyDown(KeyCode.V))
        {
            isTransmitting = !isTransmitting;
            if (isTransmitting)
                StartVoiceTransmission();
            else
                StopVoiceTransmission();
        }
    }

    /// <summary>
    /// 启动语音传输
    /// </summary>
    private void StartVoiceTransmission()
    {
        if (Microphone.devices.Length > 0)
        {
            Debug.Log("[VoiceChat] 开始语音传输...");
            // 这里集成Photon Voice功能
        }
    }

    /// <summary>
    /// 停止语音传输
    /// </summary>
    private void StopVoiceTransmission()
    {
        Debug.Log("[VoiceChat] 停止语音传输");
    }

    /// <summary>
    /// 播放远程语音
    /// </summary>
    [PunRPC]
    public void PlayRemoteVoice(byte[] audioData)
    {
        if (!voiceEnabled)
            return;

        AudioClip voiceClip = AudioClip.Create("RemoteVoice", audioData.Length, 1, 16000);
        // 转换字节数据为浮点数据
        float[] floatData = new float[audioData.Length / 2];
        System.Buffer.BlockCopy(audioData, 0, floatData, 0, audioData.Length);
        voiceClip.SetData(floatData, 0);

        voiceAudioSource.clip = voiceClip;
        voiceAudioSource.Play();
    }
}
