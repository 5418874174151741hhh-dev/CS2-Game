using UnityEngine;

/// <summary>
/// 武器管理器 - 管理玩家的武器
/// </summary>
public class WeaponManager : MonoBehaviour
{
    [SerializeField] private int maxWeapons = Constants.Weapon.MAX_WEAPONS;
    [SerializeField] private Transform weaponHolderTransform; // 武器位置
    
    private Weapon[] weapons;
    private int currentWeaponIndex = -1;

    private void Awake()
    {
        weapons = new Weapon[maxWeapons];
        Debug.Log("[WeaponManager] 武器管理器已初始化");
    }

    /// <summary>
    /// 添加武器
    /// </summary>
    public bool AddWeapon(Weapon weapon, int slotIndex = -1)
    {
        if (weapon == null)
        {
            Debug.LogWarning("[WeaponManager] 尝试添加空武器");
            return false;
        }

        // 找一个空位置
        int targetSlot = slotIndex >= 0 ? slotIndex : FindEmptySlot();
        
        if (targetSlot < 0)
        {
            Debug.LogWarning("[WeaponManager] 武器槽已满！");
            return false;
        }

        weapons[targetSlot] = weapon;
        weapon.Initialize();

        // 设置武器父转换
        if (weaponHolderTransform != null)
        {
            weapon.transform.SetParent(weaponHolderTransform);
        }

        // 只有选中的武器才比较星
        weapon.gameObject.SetActive(false);

        Debug.Log($"[WeaponManager] 添加武器: {weapon.GetWeaponName()} (slot {targetSlot})");
        
        // 如果这是第一个武器，自动选中
        if (currentWeaponIndex < 0)
        {
            SelectWeapon(targetSlot);
        }

        return true;
    }

    /// <summary>
    /// 选择武器
    /// </summary>
    public void SelectWeapon(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= maxWeapons || weapons[slotIndex] == null)
        {
            Debug.LogWarning($"[WeaponManager] 武器slot {slotIndex} 不存在");
            return;
        }

        // 隐藏当前武器
        if (currentWeaponIndex >= 0 && weapons[currentWeaponIndex] != null)
        {
            weapons[currentWeaponIndex].gameObject.SetActive(false);
        }

        // 显示新武器
        currentWeaponIndex = slotIndex;
        weapons[currentWeaponIndex].gameObject.SetActive(true);
        Debug.Log($"[WeaponManager] 选中武器: {weapons[currentWeaponIndex].GetWeaponName()}");
    }

    /// <summary>
    /// 开箱
    /// </summary>
    public bool Fire(Vector3 firePosition, Vector3 fireDirection)
    {
        if (currentWeaponIndex < 0 || weapons[currentWeaponIndex] == null)
        {
            return false;
        }
        return weapons[currentWeaponIndex].Fire(firePosition, fireDirection);
    }

    /// <summary>
    /// 装弹
    /// </summary>
    public void Reload()
    {
        if (currentWeaponIndex < 0 || weapons[currentWeaponIndex] == null)
        {
            return;
        }
        weapons[currentWeaponIndex].StartReload();
    }

    /// <summary>
    /// 找空位置
    /// </summary>
    private int FindEmptySlot()
    {
        for (int i = 0; i < maxWeapons; i++)
        {
            if (weapons[i] == null)
                return i;
        }
        return -1;
    }

    /// <summary>
    /// 获取当前武器
    /// </summary>
    public Weapon GetCurrentWeapon()
    {
        if (currentWeaponIndex < 0)
            return null;
        return weapons[currentWeaponIndex];
    }
}
