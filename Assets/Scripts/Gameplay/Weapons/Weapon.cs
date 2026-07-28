using UnityEngine;

/// <summary>
/// 武器基础类 - 所有武器的父类
/// </summary>
public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected string weaponName = "Unknown";
    [SerializeField] protected float damage = 25f;
    [SerializeField] protected float fireRate = 0.1f;
    [SerializeField] protected int maxAmmo = 30;
    [SerializeField] protected int currentAmmo = 30;
    [SerializeField] protected float reloadTime = 2.5f;
    [SerializeField] protected int price = 2900;
    [SerializeField] protected float range = 100f;

    protected bool isReloading = false;
    protected float lastFireTime = 0f;

    protected virtual void Start()
    {
        Initialize();
    }

    /// <summary>
    /// 初始化武器
    /// </summary>
    public virtual void Initialize()
    {
        currentAmmo = maxAmmo;
        Debug.Log($"[Weapon] {weaponName} 已初始化");
    }

    /// <summary>
    /// 射击
    /// </summary>
    public virtual bool Fire(Vector3 firePosition, Vector3 fireDirection)
    {
        if (isReloading)
        {
            Debug.LogWarning($"[Weapon] {weaponName} 正在装弹！");
            return false;
        }

        if (currentAmmo <= 0)
        {
            Debug.LogWarning($"[Weapon] {weaponName} 弹药焦尽！");
            StartReload();
            return false;
        }

        if (Time.time - lastFireTime < fireRate)
        {
            return false; // 射击不足频
        }

        lastFireTime = Time.time;
        currentAmmo--;

        Debug.Log($"[Weapon] {weaponName} 射击！ 剩余子弹: {currentAmmo}");
        return true;
    }

    /// <summary>
    /// 开始装弹
    /// </summary>
    public virtual void StartReload()
    {
        if (isReloading || currentAmmo == maxAmmo)
            return;

        isReloading = true;
        Debug.Log($"[Weapon] {weaponName} 开始装弹... (需要 {reloadTime} 秒)");
        Invoke(nameof(CompleteReload), reloadTime);
    }

    /// <summary>
    /// 完成装弹
    /// </summary>
    protected virtual void CompleteReload()
    {
        currentAmmo = maxAmmo;
        isReloading = false;
        Debug.Log($"[Weapon] {weaponName} 装弹完成！");
    }

    /// <summary>
    /// 获取子弹数
    /// </summary>
    public int GetCurrentAmmo() => currentAmmo;

    /// <summary>
    /// 获取最大子弹
    /// </summary>
    public int GetMaxAmmo() => maxAmmo;

    /// <summary>
    /// 获取武器伤害
    /// </summary>
    public float GetDamage() => damage;

    /// <summary>
    /// 获取武器名称
    /// </summary>
    public string GetWeaponName() => weaponName;

    /// <summary>
    /// 获取武器价格
    /// </summary>
    public int GetPrice() => price;

    /// <summary>
    /// 获取武器范围
    /// </summary>
    public float GetRange() => range;

    /// <summary>
    /// 是否正在装弹
    /// </summary>
    public bool IsReloading() => isReloading;
}
