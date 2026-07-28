using UnityEngine;

/// <summary>
/// 枪支简单实现
/// </summary>
public class Gun : Weapon
{
    [SerializeField] protected int bulletsPerShot = 1; // 每次射击的子弹数
    [SerializeField] protected float accuracy = 0.95f; // 精准度 (0-1)
    [SerializeField] protected float spreadAngle = 5f; // 散布角度

    /// <summary>
    /// 枪支射击
    /// </summary>
    public override bool Fire(Vector3 firePosition, Vector3 fireDirection)
    {
        if (!base.Fire(firePosition, fireDirection))
            return false;

        // 为每一个子弹计算射线
        for (int i = 0; i < bulletsPerShot; i++)
        {
            Vector3 bulletDirection = fireDirection;

            // 应用精准度和散布
            if (Random.value > accuracy)
            {
                float randomAngle = Random.Range(-spreadAngle, spreadAngle);
                bulletDirection = Quaternion.AngleAxis(randomAngle, Vector3.up) * fireDirection;
            }

            // 发射子弹
            FireBullet(firePosition, bulletDirection);
        }

        return true;
    }

    /// <summary>
    /// 发射单个子弹
    /// </summary>
    protected virtual void FireBullet(Vector3 startPos, Vector3 direction)
    {
        // 使用 Raycast 检测是否打中了什么
        RaycastHit hit;
        if (Physics.Raycast(startPos, direction, out hit, range))
        {
            Debug.Log($"[Gun] {weaponName} 打中了: {hit.collider.name} 于 {hit.distance:F2}m");
            
            // 检查是否是玩家
            Player targetPlayer = hit.collider.GetComponent<Player>();
            if (targetPlayer != null)
            {
                targetPlayer.TakeDamage(damage);
            }
        }
        else
        {
            Debug.Log($"[Gun] {weaponName} 未打中任何扩象");
        }
    }
}
