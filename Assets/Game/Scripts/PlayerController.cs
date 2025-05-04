using UnityEngine;
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
}
