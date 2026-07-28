using UnityEngine;

/// <summary>
/// 炸弹对象 - C4炸弹逻辑
/// </summary>
public class Bomb : MonoBehaviour
{
    [SerializeField] private float plantTime = 3f; // 安装炸弹时间
    [SerializeField] private float explodeTime = 40f; // 爆炸倒计时
    [SerializeField] private float defuseTime = 40f; // 拆弹时间
    [SerializeField] private int bombDamage = 500; // 爆炸伤害
    [SerializeField] private float explosionRadius = 50f; // 爆炸范围

    private bool isPlanted = false;
    private bool hasExploded = false;
    private float plantedTime = 0f;

    private void Update()
    {
        if (!isPlanted || hasExploded)
            return;

        // 检查爆炸倒计时
        float timeRemaining = explodeTime - (Time.time - plantedTime);
        if (timeRemaining <= 0)
        {
            Explode();
        }
    }

    /// <summary>
    /// 安装炸弹
    /// </summary>
    public void Plant(Vector3 position)
    {
        isPlanted = true;
        plantedTime = Time.time;
        transform.position = position;
        gameObject.SetActive(true);
        Debug.Log($"[Bomb] 炸弹已安装，将在 {explodeTime} 秒后爆炸");
    }

    /// <summary>
    /// 引爆炸弹
    /// </summary>
    public void Explode()
    {
        hasExploded = true;
        isPlanted = false;

        Debug.Log($"[Bomb] 炸弹爆炸！");

        // 对范围内的所有玩家造成伤害
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider hit in hitColliders)
        {
            Player player = hit.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(bombDamage);
            }
        }

        // T队赢得该回合
        if (MatchManager.Instance != null)
        {
            MatchManager.Instance.OnRoundEnd(Constants.Team.TEAM_T);
        }
    }

    /// <summary>
    /// 拆除炸弹
    /// </summary>
    public void Defuse()
    {
        isPlanted = false;
        hasExploded = true;
        gameObject.SetActive(false);

        Debug.Log("[Bomb] 炸弹已被拆除");

        // CT队赢得该回合
        if (MatchManager.Instance != null)
        {
            EconomySystem.Instance.AwardBombDefuse();
            MatchManager.Instance.OnRoundEnd(Constants.Team.TEAM_CT);
        }
    }

    /// <summary>
    /// 是否已安装
    /// </summary>
    public bool IsPlanted() => isPlanted;

    /// <summary>
    /// 获取爆炸倒计时
    /// </summary>
    public float GetTimeToExplode()
    {
        if (!isPlanted)
            return -1f;
        return Mathf.Max(0, explodeTime - (Time.time - plantedTime));
    }
}
