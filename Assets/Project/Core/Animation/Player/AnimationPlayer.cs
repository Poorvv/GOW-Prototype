using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] float blendSpeed = 0.3f;

    private float _currentBlendX;
    private float _currentBlendY;

    private LocomotionStateMachine _locomotion;
    private Vector2 _currentInput;

    public void Init(LocomotionStateMachine locomotion)
    {
        this._locomotion = locomotion;
    }

    public void SetInput(Vector2 input)
    {
        _currentInput = input;
    }

    private void Update()
    {
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        _currentBlendX = Mathf.Lerp(_currentBlendX, _currentInput.x, blendSpeed);
        _currentBlendY = Mathf.Lerp(_currentBlendY, _currentInput.y, blendSpeed);

        animator.SetFloat("MoveX", _currentBlendX);
        animator.SetFloat("MoveY", _currentBlendY);

        animator.SetBool("IsSprinting", _locomotion.CurrentState == LocomotionState.Sprint);
    }
    public void PlayDrawWeapon()
    {
        animator.SetTrigger("DrawWeapon");
    }
    public void PlaySheathWeapon()
    {
        animator.SetTrigger("SheathWeapon");
    } 
    
}