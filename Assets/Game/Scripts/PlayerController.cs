using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerController : MonoBehaviour
{
    public PlayerState CurrentState { get; private set; } = PlayerState.Idle;

    [SerializeField] PlayerAnimController animController;
    [SerializeField] Rigidbody playerRB;
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float sprintSpeed = 9f;
    [SerializeField] float accelerationTime = 0.1f;
    [SerializeField] Transform cameraRotation;

    bool _moving;
    private Vector3 _currentVelocity; // Required by SmoothDamp
    private PlayerInputHandler _inputHandler;
    private bool _isSprinting;
    private bool _movementRestricted = false;

    private void Awake()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();
        
    }

    private void FixedUpdate()
    {
        if (!_movementRestricted)
        {
            Move();
            if (_moving) 
            {
                PlayerRotation();
            }
            
        } 
    }
    private void Move()
    {
        Vector2 moveInput = _inputHandler.MoveInput;
        Vector3 moveDirection = transform.forward * moveInput.y + transform.right * moveInput.x;

        // If no movement input, smoothly slow down to 0
        if (moveDirection.sqrMagnitude < 0.01f)
        {
            Vector3 targetVelocity = new Vector3(0, playerRB.linearVelocity.y, 0);
            playerRB.linearVelocity = Vector3.SmoothDamp(playerRB.linearVelocity, targetVelocity, ref _currentVelocity, accelerationTime);
            animController.UpdateSprintAnim(false);
            _moving = false;
            animController.UpdateMoveAnim(0, 0);
            return;
        }

        float targetSpeed = (_inputHandler.IsMovingForward && _inputHandler.SprintTriggered) ? sprintSpeed : walkSpeed;
        Vector3 targetMoveVelocity = moveDirection.normalized * targetSpeed;
        Vector3 desiredVelocity = new Vector3(targetMoveVelocity.x, playerRB.linearVelocity.y, targetMoveVelocity.z);
        _moving = true;
        playerRB.linearVelocity = Vector3.SmoothDamp(playerRB.linearVelocity, desiredVelocity, ref _currentVelocity, accelerationTime);

        animController.UpdateSprintAnim(targetSpeed == sprintSpeed);
        animController.UpdateMoveAnim(moveInput.y, moveInput.x);
    }
    void PlayerRotation()
    {
        Quaternion targetRotation = cameraRotation.rotation;

        // Extract only the Y rotation
        Vector3 euler = targetRotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0f, euler.y, 0f); // Only apply Y
    }

    public void MovementRestriction()
    {
        _movementRestricted = !_movementRestricted;
    }
}

public enum PlayerState
{
    Idle,
    Walking,
    Sprinting,
    Dodging,
    Attacking
}