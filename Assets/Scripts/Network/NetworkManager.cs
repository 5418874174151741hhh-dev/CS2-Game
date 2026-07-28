using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

/// <summary>
/// Photon网络管理器 - 处理服务器连接和房间管理
/// </summary>
public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private string gameVersion = "1.0";
    [SerializeField] private int maxPlayersPerRoom = 10;
    [SerializeField] private bool autoConnect = true;

    private static NetworkManager instance;
    public static NetworkManager Instance => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (autoConnect && !PhotonNetwork.IsConnected)
        {
            ConnectToPhoton();
        }
    }

    /// <summary>
    /// 连接到Photon服务器
    /// </summary>
    public void ConnectToPhoton()
    {
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("[NetworkManager] 已连接到Photon");
            return;
        }

        Debug.Log("[NetworkManager] 正在连接到Photon...");
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    /// <summary>
    /// 创建或加入房间
    /// </summary>
    public void CreateOrJoinRoom(string roomName = "DefaultRoom")
    {
        if (!PhotonNetwork.IsConnected)
        {
            Debug.LogError("[NetworkManager] 未连接到Photon");
            return;
        }

        RoomOptions roomOptions = new RoomOptions
        {
            MaxPlayers = (byte)maxPlayersPerRoom,
            IsVisible = true,
            IsOpen = true,
            CustomProperties = new Hashtable
            {
                { "gameMode", "Competitive" },
                { "map", "Dust2" }
            }
        };

        Debug.Log($"[NetworkManager] 正在加入房间: {roomName}");
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
    }

    /// <summary>
    /// 断开连接
    /// </summary>
    public void Disconnect()
    {
        Debug.Log("[NetworkManager] 断开连接");
        PhotonNetwork.Disconnect();
    }

    // ===== Photon 回调 =====

    public override void OnConnected()
    {
        Debug.Log("[NetworkManager] 已连接到Photon服务器");
    }

    public override void OnConnectedToPhoton()
    {
        Debug.Log("[NetworkManager] 已连接到Photon Cloud");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarning($"[NetworkManager] 断开连接: {cause}");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"[NetworkManager] 成功加入房间: {PhotonNetwork.CurrentRoom.Name}");
        Debug.Log($"[NetworkManager] 房间玩家数: {PhotonNetwork.CurrentRoom.PlayerCount}/{PhotonNetwork.CurrentRoom.MaxPlayers}");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"[NetworkManager] 玩家加入房间: {newPlayer.NickName}");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer, DisconnectCause cause)
    {
        Debug.Log($"[NetworkManager] 玩家离开房间: {otherPlayer.NickName}");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError($"[NetworkManager] 加入房间失败: {message}");
    }

    public override void OnConnectFailed(DisconnectCause cause)
    {
        Debug.LogError($"[NetworkManager] 连接失败: {cause}");
    }

    /// <summary>
    /// 获取是否是房间主人
    /// </summary>
    public bool IsMasterClient() => PhotonNetwork.IsMasterClient;

    /// <summary>
    /// 获取房间中的玩家数
    /// </summary>
    public int GetPlayerCount() => PhotonNetwork.CurrentRoom?.PlayerCount ?? 0;

    /// <summary>
    /// 获取所有玩家
    /// </summary>
    public Player[] GetAllPlayers() => PhotonNetwork.CurrentRoom?.Players.Values.ToArray() ?? new Player[0];
}
