using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] Rigidbody rb;
    private LocomotionStateMachine _locomotion;
    private Vector2 _currentInput;

    public void Init(LocomotionStateMachine locomotion)
    {
        _locomotion = locomotion;
    }
    public void SetInput(Vector2 input)
    {
        _currentInput = input;
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        float speed = _locomotion.GetSpeed();
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDir = forward *_currentInput.y + right * _currentInput.x;

        if (moveDir.magnitude > 0.1f)
        {
            Quaternion targetRot = Quaternion.LookRotation(forward);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRot,
                _locomotion.GetRotationSpeed() * Time.deltaTime
            );
        }
        rb.linearVelocity = new Vector3(moveDir.x, rb.linearVelocity.y, moveDir.z) * speed;
    }

}
