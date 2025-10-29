using UnityEngine;
using System;
public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInputs _playerInputs;
    public Vector2 MoveInput { get; private set; }
    public bool SprintTriggered { get; private set; }
    public bool IsMovingForward { get; private set; }

    public event Action OnWeaponDrawn; // event for draw
    public event Action OnLightAttack;
    //public event Action OnWeaponSheathed; // optional if you want toggling

    private void OnEnable()
    {
        _playerInputs = new PlayerInputs();
        _playerInputs.Enable();

        _playerInputs.Player.Move.performed += ctx => HandleMoveInput(ctx.ReadValue<Vector2>());
        _playerInputs.Player.Move.canceled += ctx => HandleMoveCancel();
        _playerInputs.Player.Sprint.performed += ctx => HandleSprintInput();
        _playerInputs.Player.DrawWeapon.performed += ctx => HandleDrawWeaponInput();
        _playerInputs.Player.Attack.performed += ctx => HandleLightAttack();
    }

    private void OnDisable()
    {
        _playerInputs.Disable();

        _playerInputs.Player.Move.performed -= ctx => HandleMoveInput(ctx.ReadValue<Vector2>());
        _playerInputs.Player.Move.canceled -= ctx => HandleMoveCancel();
        _playerInputs.Player.Sprint.performed -= ctx => HandleSprintInput();
        _playerInputs.Player.DrawWeapon.performed -= ctx => HandleDrawWeaponInput();
        _playerInputs.Player.Attack.performed -= ctx => HandleLightAttack();
    }

    void HandleMoveInput(Vector2 input)
    {
        MoveInput = input.normalized;
        IsMovingForward = input.y > 0.71f;
    }

    void HandleMoveCancel()
    {
        MoveInput = Vector2.zero;
        IsMovingForward = false;
        SprintTriggered = false;
    }

    void HandleSprintInput()
    {
        if (IsMovingForward)
        {
            SprintTriggered = true;
        }
        else
        {
            SprintTriggered = false;
        }
    }
    void HandleDrawWeaponInput() 
    {
        OnWeaponDrawn?.Invoke();
    }
    void HandleLightAttack()
    {
        OnLightAttack?.Invoke();
    }
}
