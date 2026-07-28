using UnityEngine;

/// <summary>
/// 血液溅洒特效
/// </summary>
public class BloodSplatter : MonoBehaviour
{
    [SerializeField] private ParticleSystem bloodParticles;
    [SerializeField] private GameObject bloodDecalPrefab;
    [SerializeField] private float decalLifetime = 30f;

    /// <summary>
    /// 在指定位置播放血液特效
    /// </summary>
    public void PlayBloodEffect(Vector3 position, Vector3 normal)
    {
        if (bloodParticles != null)
        {
            Instantiate(bloodParticles, position, Quaternion.LookRotation(normal));
        }

        if (bloodDecalPrefab != null)
        {
            GameObject decal = Instantiate(bloodDecalPrefab, position + normal * 0.01f, Quaternion.LookRotation(normal));
            Destroy(decal, decalLifetime);
        }

        Debug.Log($"[BloodSplatter] 血液特效在 {position}");
    }
}
