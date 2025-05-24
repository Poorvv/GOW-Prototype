/*using UnityEngine;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    PlayerInputs _playerInputs;
    [SerializeField] Rigidbody playerRB;
    [SerializeField] float walkSpeed;
    [SerializeField] float sprintSpeed;
    private Vector2 _moveInput;
    private float currentSpeed;
    private bool _isMovingForward;
    private bool _isSprinting;
    private void OnEnable()
    {
        _playerInputs = new PlayerInputs();
        _playerInputs.Enable();
        _playerInputs.Player.Move.performed += ctx => HandleMoveInput(ctx.ReadValue<Vector2>());
        _playerInputs.Player.Move.canceled += ctx => HandleMoveCancelInput(ctx.ReadValue<Vector2>());
        _playerInputs.Player.Sprint.performed += ctx => HandleSprintInput();
        
    }
    private void OnDisable()
    {
        _playerInputs.Disable();
        _playerInputs.Player.Move.performed -= ctx => HandleMoveInput(ctx.ReadValue<Vector2>());
        _playerInputs.Player.Move.canceled -= ctx => HandleMoveCancelInput(ctx.ReadValue<Vector2>());
        _playerInputs.Player.Sprint.performed -= ctx => HandleSprintInput();


    }
    private void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        Vector3 moveVelocity;
        Vector3 moveDirection = transform.forward * _moveInput.y + transform.right * _moveInput.x;
        if (_isMovingForward && _isSprinting)
        {
            moveVelocity = moveDirection.normalized * sprintSpeed;
        }
        else
        {
            moveVelocity = moveDirection.normalized * walkSpeed;
        }
        print(moveVelocity);
        playerRB.linearVelocity = new Vector3(moveVelocity.x, playerRB.linearVelocity.y, moveVelocity.z);
    }
    void HandleMoveInput(Vector2 input)
    {
        _moveInput = input;
        _isMovingForward = _moveInput.y > 0.71f;
    }
    void HandleMoveCancelInput(Vector2 input)
    {
        _moveInput = input;
        _isMovingForward = false;
        _isSprinting = false;
    }
    void HandleSprintInput()
    {
        if (_isMovingForward)
        {
            _isSprinting = true;
        }
    }
}*/
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerController : MonoBehaviour
{
    public PlayerState CurrentState { get; private set; } = PlayerState.Idle;
    [SerializeField] Rigidbody playerRB;
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float sprintSpeed = 9f;
    [SerializeField] float dodgeCooldown;


    private PlayerInputHandler _inputHandler;
    private float _lastDodgeTime = -999f;
    private bool _isSprinting;

    private void Awake()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();
    }

    private void FixedUpdate()
    {
        Move();
        if (_inputHandler.DodgePressed)
        {
            TriggerDodge(_inputHandler.DodgeDirection);
            _inputHandler.ResetDodge();
        }
    }

    private void Move()
    {
        Vector2 _moveInput = _inputHandler.MoveInput;
        Vector3 moveVelocity;
        Vector3 moveDirection = transform.forward * _moveInput.y + transform.right * _moveInput.x;
        if (_inputHandler.IsMovingForward && _inputHandler.SprintTriggered)
        {
            moveVelocity = moveDirection.normalized * sprintSpeed;
        }
        else
        {
            moveVelocity = moveDirection.normalized * walkSpeed;
        }
        playerRB.linearVelocity = new Vector3(moveVelocity.x, playerRB.linearVelocity.y, moveVelocity.z);
    }
    void TriggerDodge(Vector2 dir)
    {
        if (Time.time < _lastDodgeTime + dodgeCooldown || CurrentState == PlayerState.Dodging)
            return;
        print("Dodging in : " + dir);
        //animController.TriggerDodge(dir);
        StartCoroutine(EndDodgeAfterSeconds(0.6f)); // Match animation duration
    }

    private IEnumerator EndDodgeAfterSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        CurrentState = PlayerState.Idle;
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