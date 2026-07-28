using UnityEngine;

/// <summary>
/// 玩家控制器 - 处理玩家输入和移动
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = Constants.Player.PLAYER_SPEED;
    [SerializeField] private float sprintSpeed = Constants.Player.PLAYER_SPRINT_SPEED;
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float maxLookAngle = 90f;

    private Player player;
    private Rigidbody rb;
    private Camera playerCamera;
    private float currentYRotation = 0f;
    private float currentXRotation = 0f;
    private Vector3 moveDirection = Vector3.zero;

    private void Awake()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();

        if (playerCamera == null)
        {
            Debug.LogError("[PlayerController] 未找到摄像机组件！");
        }
    }

    private void Update()
    {
        if (!player.IsAlive())
            return;

        HandleMovementInput();
        HandleCameraRotation();
    }

    private void FixedUpdate()
    {
        if (!player.IsAlive())
            return;

        ApplyMovement();
    }

    /// <summary>
    /// 处理移动输入
    /// </summary>
    private void HandleMovementInput()
    {
        moveDirection = Vector3.zero;

        if (InputManager.Instance.IsKeyHeld("Forward"))
            moveDirection += transform.forward;
        if (InputManager.Instance.IsKeyHeld("Backward"))
            moveDirection -= transform.forward;
        if (InputManager.Instance.IsKeyHeld("Left"))
            moveDirection -= transform.right;
        if (InputManager.Instance.IsKeyHeld("Right"))
            moveDirection += transform.right;

        moveDirection.Normalize();

        // 检查冲刺
        float currentSpeed = moveSpeed;
        if (InputManager.Instance.IsKeyHeld("Sprint"))
        {
            currentSpeed = sprintSpeed;
        }

        moveDirection *= currentSpeed;
    }

    /// <summary>
    /// 处理相机旋转
    /// </summary>
    private void HandleCameraRotation()
    {
        Vector2 mouseDelta = InputManager.Instance.GetMouseDelta();

        // 水平旋转 (Y轴)
        currentYRotation += mouseDelta.x * mouseSensitivity;
        transform.rotation = Quaternion.Euler(0, currentYRotation, 0);

        // 垂直旋转 (X轴)
        currentXRotation -= mouseDelta.y * mouseSensitivity;
        currentXRotation = Mathf.Clamp(currentXRotation, -maxLookAngle, maxLookAngle);

        if (playerCamera != null)
        {
            playerCamera.transform.localRotation = Quaternion.Euler(currentXRotation, 0, 0);
        }
    }

    /// <summary>
    /// 应用移动
    /// </summary>
    private void ApplyMovement()
    {
        if (rb != null)
        {
            Vector3 velocityChange = moveDirection;
            velocityChange.y = rb.velocity.y; // 保持Y轴速度（重力）
            rb.velocity = velocityChange;
        }
    }
}
