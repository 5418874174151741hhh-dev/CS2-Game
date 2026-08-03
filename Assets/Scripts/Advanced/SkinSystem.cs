using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 皮肤系统 - 管理角色和武器皮肤
/// </summary>
public class SkinSystem : SingletonManager<SkinSystem>
{
    [System.Serializable]
    public class Skin
    {
        public string skinId;
        public string skinName;
        public int price = 0; // 0表示默认，>0表示付费
        public string meshPath;
        public string texturePath;
        public string rarity = "普通"; // 普通、稀有、传说
        public bool purchased = false;
    }

    private Dictionary<string, Skin> availableSkins = new Dictionary<string, Skin>();
    private Dictionary<int, Dictionary<string, string>> playerSkins = new Dictionary<int, Dictionary<string, string>>(); // playerId -> (type -> skinId)

    protected override void Awake()
    {
        base.Awake();
        InitializeSkins();
    }

    /// <summary>
    /// 初始化可用皮肤
    /// </summary>
    private void InitializeSkins()
    {
        // 添加示例皮肤
        availableSkins["char_default"] = new Skin 
        { skinId = "char_default", skinName = "默认角色", rarity = "普通" };
        
        availableSkins["char_military"] = new Skin 
        { skinId = "char_military", skinName = "军装", price = 500, rarity = "稀有" };
        
        availableSkins["weapon_default"] = new Skin 
        { skinId = "weapon_default", skinName = "默认武器", rarity = "普通" };
        
        availableSkins["weapon_dragon"] = new Skin 
        { skinId = "weapon_dragon", skinName = "龙纹", price = 1000, rarity = "传说" };

        Debug.Log($"[SkinSystem] 已加载 {availableSkins.Count} 个皮肤");
    }

    /// <summary>
    /// 购买皮肤
    /// </summary>
    public bool PurchaseSkin(int playerId, string skinId, int playerMoney)
    {
        if (!availableSkins.ContainsKey(skinId))
        {
            Debug.LogWarning($"[SkinSystem] 皮肤 {skinId} 不存在");
            return false;
        }

        Skin skin = availableSkins[skinId];
        if (skin.price > 0 && playerMoney < skin.price)
        {
            Debug.LogWarning($"[SkinSystem] 玩家金币不足，需要 {skin.price}，拥有 {playerMoney}");
            return false;
        }

        skin.purchased = true;
        Debug.Log($"[SkinSystem] 玩家 {playerId} 购买皮肤 {skin.skinName} 成功");
        return true;
    }

    /// <summary>
    /// 装备皮肤
    /// </summary>
    public void EquipSkin(int playerId, string type, string skinId)
    {
        if (!playerSkins.ContainsKey(playerId))
            playerSkins[playerId] = new Dictionary<string, string>();

        playerSkins[playerId][type] = skinId;
        Debug.Log($"[SkinSystem] 玩家 {playerId} 装备皮肤 {skinId}");
    }

    /// <summary>
    /// 获取玩家装备的皮肤
    /// </summary>
    public string GetEquippedSkin(int playerId, string type)
    {
        if (!playerSkins.ContainsKey(playerId) || !playerSkins[playerId].ContainsKey(type))
            return null;
        return playerSkins[playerId][type];
    }

    /// <summary>
    /// 获取皮肤详情
    /// </summary>
    public Skin GetSkinDetails(string skinId)
    {
        return availableSkins.ContainsKey(skinId) ? availableSkins[skinId] : null;
    }
}
