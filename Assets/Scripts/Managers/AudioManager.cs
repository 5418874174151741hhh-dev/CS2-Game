using UnityEngine;

/// <summary>
/// 音频管理器 - 处理所有游戏音效和音乐
/// </summary>
public class AudioManager : SingletonManager<AudioManager>
{
    [SerializeField] private float masterVolume = 1f;
    [SerializeField] private float sfxVolume = 0.7f;
    [SerializeField] private float musicVolume = 0.5f;

    private AudioSource musicSource;
    private AudioSource sfxSource;

    protected override void Awake()
    {
        base.Awake();
        InitializeAudioSources();
    }

    private void InitializeAudioSources()
    {
        // 创建音乐音源
        GameObject musicObj = new GameObject("MusicSource");
        musicObj.transform.SetParent(transform);
        musicSource = musicObj.AddComponent<AudioSource>();
        musicSource.loop = true;

        // 创建SFX音源
        GameObject sfxObj = new GameObject("SFXSource");
        sfxObj.transform.SetParent(transform);
        sfxSource = sfxObj.AddComponent<AudioSource>();

        Debug.Log("[AudioManager] 音频系统已初始化");
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip == null)
        {
            Debug.LogWarning("[AudioManager] 尝试播放空音效剪辑");
            return;
        }

        sfxSource.PlayOneShot(clip, volume * sfxVolume * masterVolume);
    }

    /// <summary>
    /// 播放音乐
    /// </summary>
    public void PlayMusic(AudioClip clip, float volume = 1f)
    {
        if (clip == null)
        {
            Debug.LogWarning("[AudioManager] 尝试播放空音乐剪辑");
            return;
        }

        musicSource.clip = clip;
        musicSource.volume = volume * musicVolume * masterVolume;
        musicSource.Play();
    }

    /// <summary>
    /// 停止音乐
    /// </summary>
    public void StopMusic()
    {
        musicSource.Stop();
    }

    /// <summary>
    /// 设置主音量
    /// </summary>
    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);
    }

    /// <summary>
    /// 设置音效音量
    /// </summary>
    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
    }

    /// <summary>
    /// 设置音乐音量
    /// </summary>
    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
    }
}
