using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInputs _playerInputs;

    public Vector2 MoveInput { get; private set; }
    public Vector2 DodgeDirection { get; private set; }
    public bool SprintTriggered { get; private set; }
    public bool IsMovingForward { get; private set; }
    public bool DodgePressed { get; private set; }

    //public delegate void SprintEvent();
    //public event SprintEvent OnSprint;

    private void OnEnable()
    {
        _playerInputs = new PlayerInputs();
        _playerInputs.Enable();

        _playerInputs.Player.Move.performed += ctx => HandleMoveInput(ctx.ReadValue<Vector2>());
        _playerInputs.Player.Move.canceled += ctx => HandleMoveCancel();
        _playerInputs.Player.Sprint.performed += ctx => HandleSprintInput();
        _playerInputs.Player.Doge.performed += ctx => HandleDogeInput();
    }

    private void OnDisable()
    {
        _playerInputs.Disable();

        _playerInputs.Player.Move.performed -= ctx => HandleMoveInput(ctx.ReadValue<Vector2>());
        _playerInputs.Player.Move.canceled -= ctx => HandleMoveCancel();
        _playerInputs.Player.Sprint.performed -= ctx => HandleSprintInput();
        _playerInputs.Player.Doge.performed -= ctx => HandleDogeInput();
    }

    void HandleMoveInput(Vector2 input)
    {
        MoveInput = input;
        IsMovingForward = input.y > 0.71f;
        print(input.y);
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
            //OnSprint?.Invoke();
        }
    }
    void HandleDogeInput()
    {
        DodgePressed = true;
        DodgeDirection = MoveInput == Vector2.zero ? Vector2.down : MoveInput.normalized;
    }
    public void ResetDodge()
    {
        DodgePressed = false;
    }
}
