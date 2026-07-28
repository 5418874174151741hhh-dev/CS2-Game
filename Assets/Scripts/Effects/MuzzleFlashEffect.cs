using UnityEngine;

/// <summary>
/// 枪口闪光特效
/// </summary>
public class MuzzleFlashEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private Light muzzleLight;
    [SerializeField] private float lightDuration = 0.05f;

    private float lightTimer = 0f;

    private void Update()
    {
        if (muzzleLight != null && muzzleLight.enabled)
        {
            lightTimer -= Time.deltaTime;
            if (lightTimer <= 0)
            {
                muzzleLight.enabled = false;
            }
        }
    }

    /// <summary>
    /// 播放枪口闪光
    /// </summary>
    public void PlayMuzzleFlash()
    {
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        if (muzzleLight != null)
        {
            muzzleLight.enabled = true;
            lightTimer = lightDuration;
        }

        Debug.Log("[MuzzleFlashEffect] 枪口闪光");
    }
}
