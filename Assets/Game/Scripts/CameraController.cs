using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform cameraLookTarget;
    [SerializeField] float lookSpeed = 2f;
    [SerializeField] float clampAngle = 80f;

    private Vector2 _lookInput;
    private float _yaw;
    private float _pitch;

    private void OnEnable()
    {
        PlayerInputs playerInputs = new PlayerInputs();
        playerInputs.Enable();
        playerInputs.Player.Look.performed += ctx => _lookInput = ctx.ReadValue<Vector2>();
        playerInputs.Player.Look.canceled -= _ => _lookInput = Vector2.zero;
    }
    void Start()
    {
        
    }
    void HandleCameraRotation()
    {
        _yaw += _lookInput.x * lookSpeed;
        _pitch -= _lookInput.y * lookSpeed;
        _pitch = Mathf.Clamp( _pitch, -clampAngle, clampAngle);
        cameraLookTarget.rotation = Quaternion.Euler(_pitch, _yaw, 0);
    }
    // Update is called once per frame
    void Update()
    {
        HandleCameraRotation();
    }
}
