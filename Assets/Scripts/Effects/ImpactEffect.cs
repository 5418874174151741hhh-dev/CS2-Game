using UnityEngine;

/// <summary>
/// 碰撞特效 - 子弹打中环境或物体
/// </summary>
public class ImpactEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem impactParticles;
    [SerializeField] private AudioClip impactSound;
    [SerializeField] private float particleLifetime = 2f;

    /// <summary>
    /// 在指定位置播放碰撞特效
    /// </summary>
    public void PlayImpactEffect(Vector3 position, Vector3 normal, Material surfaceMaterial = null)
    {
        if (impactParticles != null)
        {
            ParticleSystem particles = Instantiate(impactParticles, position, Quaternion.LookRotation(normal));
            Destroy(particles.gameObject, particleLifetime);
        }

        if (impactSound != null)
        {
            AudioSource.PlayClipAtPoint(impactSound, position, 1f);
        }

        Debug.Log($"[ImpactEffect] 碰撞特效在 {position}");
    }
}
