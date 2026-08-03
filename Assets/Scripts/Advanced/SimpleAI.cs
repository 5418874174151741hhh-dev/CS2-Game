using UnityEngine;

/// <summary>
/// 简单AI - 基础AI对手逻辑
/// </summary>
public class SimpleAI : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float detectionRange = 50f;
    [SerializeField] private float shootRange = 30f;
    [SerializeField] private float reactionTime = 0.5f;
    [SerializeField] private int difficulty = 1; // 1-简单，2-中等，3-困难

    private Player aiPlayer;
    private Rigidbody rb;
    private Player targetPlayer;
    private float lastReactionTime = 0f;
    private Vector3 randomDirection = Vector3.zero;
    private float directionChangeTimer = 0f;

    private void Start()
    {
        aiPlayer = GetComponent<Player>();
        rb = GetComponent<Rigidbody>();
        Debug.Log($"[SimpleAI] AI玩家已初始化，难度: {difficulty}");
    }

    private void Update()
    {
        if (!aiPlayer.IsAlive())
            return;

        SearchForTarget();
        
        if (targetPlayer != null && targetPlayer.IsAlive())
        {
            EngageTarget();
        }
        else
        {
            Patrol();
        }
    }

    /// <summary>
    /// 搜索目标
    /// </summary>
    private void SearchForTarget()
    {
        Player[] allPlayers = FindObjectsOfType<Player>();
        float closestDistance = detectionRange;

        foreach (Player player in allPlayers)
        {
            if (player == aiPlayer || player.GetTeamId() == aiPlayer.GetTeamId())
                continue;

            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < closestDistance && player.IsAlive())
            {
                closestDistance = distance;
                targetPlayer = player;
            }
        }
    }

    /// <summary>
    /// 巡逻模式
    /// </summary>
    private void Patrol()
    {
        directionChangeTimer -= Time.deltaTime;
        if (directionChangeTimer <= 0)
        {
            randomDirection = Random.onUnitSphere;
            randomDirection.y = 0;
            directionChangeTimer = Random.Range(2f, 5f);
        }

        rb.velocity = new Vector3(randomDirection.x * moveSpeed, rb.velocity.y, randomDirection.z * moveSpeed);
    }

    /// <summary>
    /// 攻击目标
    /// </summary>
    private void EngageTarget()
    {
        Vector3 directionToTarget = (targetPlayer.transform.position - transform.position).normalized;
        float distanceToTarget = Vector3.Distance(transform.position, targetPlayer.transform.position);

        // 移动
        if (distanceToTarget > shootRange)
        {
            rb.velocity = new Vector3(directionToTarget.x * moveSpeed, rb.velocity.y, directionToTarget.z * moveSpeed);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        // 射击
        if (Time.time - lastReactionTime > reactionTime / difficulty)
        {
            WeaponManager weaponManager = GetComponent<WeaponManager>();
            if (weaponManager != null)
            {
                weaponManager.Fire(transform.position, directionToTarget);
                lastReactionTime = Time.time;
                Debug.Log("[SimpleAI] AI射击");
            }
        }
    }
}
