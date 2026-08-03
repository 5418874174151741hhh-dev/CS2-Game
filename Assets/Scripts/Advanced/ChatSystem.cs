using UnityEngine;

/// <summary>
/// 文字聊天系统
/// </summary>
public class ChatSystem : MonoBehaviourPun
{
    [SerializeField] private int maxChatMessages = 50;
    private Queue<ChatMessage> chatMessages = new Queue<ChatMessage>();
    private bool chatPanelActive = false;
    private string inputText = "";

    [System.Serializable]
    public class ChatMessage
    {
        public string playerName;
        public string message;
        public string timestamp;
        public int team = -1; // -1全局，0-CT，1-T
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            chatPanelActive = !chatPanelActive;
            Debug.Log("[ChatSystem] 聊天面板" + (chatPanelActive ? "打开" : "关闭"));
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            chatPanelActive = !chatPanelActive;
            // Y键为队伍聊天
            Debug.Log("[ChatSystem] 队伍聊天面板" + (chatPanelActive ? "打开" : "关闭"));
        }
    }

    /// <summary>
    /// 发送全局聊天
    /// </summary>
    public void SendGlobalMessage(string playerName, string message)
    {
        photonView.RPC(nameof(ReceiveMessage), RpcTarget.AllBuffered, playerName, message, -1);
    }

    /// <summary>
    /// 发送队伍聊天
    /// </summary>
    public void SendTeamMessage(string playerName, string message, int team)
    {
        photonView.RPC(nameof(ReceiveMessage), RpcTarget.AllBuffered, playerName, message, team);
    }

    /// <summary>
    /// 接收消息
    /// </summary>
    [PunRPC]
    private void ReceiveMessage(string playerName, string message, int team)
    {
        ChatMessage chatMsg = new ChatMessage
        {
            playerName = playerName,
            message = message,
            timestamp = System.DateTime.Now.ToString("HH:mm:ss"),
            team = team
        };

        chatMessages.Enqueue(chatMsg);
        if (chatMessages.Count > maxChatMessages)
            chatMessages.Dequeue();

        string teamTag = team == -1 ? "[全局]" : team == 0 ? "[CT]" : "[T]";
        Debug.Log($"[ChatSystem] {teamTag} {playerName}: {message}");
    }

    /// <summary>
    /// 获取聊天消息
    /// </summary>
    public List<ChatMessage> GetChatMessages()
    {
        return new List<ChatMessage>(chatMessages);
    }
}
