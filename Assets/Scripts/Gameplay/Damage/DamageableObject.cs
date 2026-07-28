using UnityEngine;

/// <summary>
/// 态、应用了伤害的对象基类
/// </summary>
public class DamageableObject : MonoBehaviour
{
    [SerializeField] protected float health = 100f;
    [SerializeField] protected float maxHealth = 100f;
    protected bool isDead = false;

    /// <summary>
    /// 接受伤害
    /// </summary>
    public virtual void TakeDamage(float damage)
    {
        if (isDead)
            return;

        health -= damage;
        Debug.Log($"[DamageableObject] {gameObject.name} 接受 {damage:F2} 点伤害，剩余血量: {health:F2}");

        if (health <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// 死亡
    /// </summary>
    public virtual void Die()
    {
        isDead = true;
        Debug.Log($"[DamageableObject] {gameObject.name} 已死亡");
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 获取当前血量
    /// </summary>
    public float GetHealth() => health;

    /// <summary>
    /// 获取最大血量
    /// </summary>
    public float GetMaxHealth() => maxHealth;

    /// <summary>
    /// 是否已死亡
    /// </summary>
    public bool IsDead() => isDead;

    /// <summary>
    /// 恢复血量
    /// </summary>
    public void Heal(float amount)
    {
        health = Mathf.Min(health + amount, maxHealth);
        Debug.Log($"[DamageableObject] {gameObject.name} 恢复 {amount} 血量，当前: {health}");
    }
}
