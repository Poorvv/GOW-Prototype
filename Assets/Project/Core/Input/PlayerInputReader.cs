using UnityEngine;
using System;

public class PlayerInputReader : MonoBehaviour
{
    private PlayerInputs _inputs;

    private InputIntent _currentIntent;
    public InputIntent CurrentIntent => _currentIntent;

    public event Action<InputIntent> OnInputIntent;

    private void Awake()
    {
        _inputs = new PlayerInputs();
    }

    private void OnEnable()
    {
        _inputs.Enable();

        _inputs.Player.Move.performed += OnMove;
        _inputs.Player.Move.canceled += OnMoveCanceled;
        _inputs.Player.Sprint.performed += OnSprint;
        _inputs.Player.LightAttack.performed += OnLightAttack;
        _inputs.Player.HeavyAttack.performed += OnHeavyAttack;
        _inputs.Player.ToggleWeaponPressed.performed += OnDrawWeapon;
    }

    private void OnDisable()
    {
        _inputs.Player.Move.performed -= OnMove;
        _inputs.Player.Move.canceled -= OnMoveCanceled;
        _inputs.Player.Sprint.performed -= OnSprint;
        _inputs.Player.LightAttack.performed -= OnLightAttack;
        _inputs.Player.HeavyAttack.performed -= OnHeavyAttack;
        _inputs.Player.ToggleWeaponPressed.performed -= OnDrawWeapon;

        _inputs.Disable();
    }

    private void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        _currentIntent.Move = ctx.ReadValue<Vector2>();
        RaiseIntent();
    }

    private void OnMoveCanceled(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        _currentIntent.Move = Vector2.zero;
        RaiseIntent();
    }

    private void OnSprint(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        _currentIntent.SprintPressed = true;
        RaiseIntent();
    }

    private void OnLightAttack(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        _currentIntent.LightAttackPressed = true;
        RaiseIntent();
    }

    private void OnHeavyAttack(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        _currentIntent.HeavyAttackPressed = true;
        RaiseIntent();
    }

    private void OnDrawWeapon(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        _currentIntent.ToggleWeaponPressed = true;
        RaiseIntent();
    }

    private void RaiseIntent()
    {
        OnInputIntent?.Invoke(_currentIntent);

        // reset one-frame buttons
        _currentIntent.SprintPressed = false;
        _currentIntent.LightAttackPressed = false;
        _currentIntent.HeavyAttackPressed = false;
        _currentIntent.ToggleWeaponPressed = false;
    }
}

