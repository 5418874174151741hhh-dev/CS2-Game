using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 粒子特效对象池 - 管理特效生成和回收
/// </summary>
public class ParticlePoolManager : SingletonManager<ParticlePoolManager>
{
    [SerializeField] private ParticleSystem muzzleFlashPrefab;
    [SerializeField] private ParticleSystem bloodParticlePrefab;
    [SerializeField] private ParticleSystem impactParticlePrefab;
    [SerializeField] private int initialPoolSize = 10;

    private Queue<ParticleSystem> muzzleFlashPool;
    private Queue<ParticleSystem> bloodParticlePool;
    private Queue<ParticleSystem> impactParticlePool;

    protected override void Awake()
    {
        base.Awake();
        InitializePools();
    }

    /// <summary>
    /// 初始化对象池
    /// </summary>
    private void InitializePools()
    {
        muzzleFlashPool = new Queue<ParticleSystem>();
        bloodParticlePool = new Queue<ParticleSystem>();
        impactParticlePool = new Queue<ParticleSystem>();

        for (int i = 0; i < initialPoolSize; i++)
        {
            if (muzzleFlashPrefab != null)
            {
                ParticleSystem particle = Instantiate(muzzleFlashPrefab);
                particle.gameObject.SetActive(false);
                muzzleFlashPool.Enqueue(particle);
            }
        }

        Debug.Log($"[ParticlePoolManager] 对象池已初始化，初始大小: {initialPoolSize}");
    }

    /// <summary>
    /// 获取枪口闪光特效
    /// </summary>
    public ParticleSystem GetMuzzleFlash(Vector3 position)
    {
        ParticleSystem particle = muzzleFlashPool.Count > 0 ? muzzleFlashPool.Dequeue() : Instantiate(muzzleFlashPrefab);
        particle.transform.position = position;
        particle.gameObject.SetActive(true);
        return particle;
    }

    /// <summary>
    /// 归还枪口闪光特效
    /// </summary>
    public void ReturnMuzzleFlash(ParticleSystem particle)
    {
        particle.gameObject.SetActive(false);
        muzzleFlashPool.Enqueue(particle);
    }
}
