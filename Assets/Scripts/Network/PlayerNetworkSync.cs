using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// 玩家网络同步 - 同步玩家位置、旋转和动画
/// </summary>
public class PlayerNetworkSync : MonoBehaviourPun, IPunObservable
{
    [SerializeField] private Player player;
    [SerializeField] private PlayerController controller;
    [SerializeField] private float updateRate = 0.1f; // 10次/秒
    [SerializeField] private float positionSmoothness = 0.1f;

    private Vector3 networkPosition;
    private Quaternion networkRotation;
    private float lastUpdateTime = 0f;

    private void Start()
    {
        if (player == null)
            player = GetComponent<Player>();
        if (controller == null)
            controller = GetComponent<PlayerController>();

        networkPosition = transform.position;
        networkRotation = transform.rotation;
    }

    private void Update()
    {
        // 如果不是本地玩家，应用网络位置和旋转
        if (!photonView.IsMine)
        {
            transform.position = Vector3.Lerp(transform.position, networkPosition, positionSmoothness);
            transform.rotation = Quaternion.Lerp(transform.rotation, networkRotation, positionSmoothness);
        }
    }

    /// <summary>
    /// 实现IPunObservable接口 - 序列化数据到网络
    /// </summary>
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 发送本地玩家的数据
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(player?.IsAlive() ?? true);
            
            PlayerStats stats = player?.GetStats();
            if (stats != null)
            {
                stream.SendNext(stats.Health);
                stream.SendNext(stats.Armor);
            }
        }
        else
        {
            // 接收远程玩家的数据
            networkPosition = (Vector3)stream.ReceiveNext();
            networkRotation = (Quaternion)stream.ReceiveNext();
            bool isAlive = (bool)stream.ReceiveNext();
            
            if (!isAlive && player.IsAlive())
            {
                player.Die();
            }
            
            int health = (int)stream.ReceiveNext();
            int armor = (int)stream.ReceiveNext();
        }
    }

    /// <summary>
    /// 在网络上播放动画
    /// </summary>
    [PunRPC]
    public void PlayAnimationRPC(string animationName)
    {
        // 播放动画逻辑
        Debug.Log($"[PlayerNetworkSync] 播放动画: {animationName}");
    }
}
