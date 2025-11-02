using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private PlayerInputHandler inputHandler;
    public Animator playerAnimator;
    [SerializeField] float blendSpeed = 0.3f;

    private float _currentBlendX;
    private float _currentBlendY;

    public bool isBusy = false;
    private bool _isWeaponDrawn;

    private void OnEnable()
    {
        inputHandler.OnWeaponDrawn += HandleDrawWeapon; 
    }

    private void OnDisable()
    {
        inputHandler.OnWeaponDrawn -= HandleDrawWeapon;
    }
    public void UpdateMoveAnim(float moveX, float moveY)
    {
        _currentBlendX = Mathf.Lerp(_currentBlendX, moveY, blendSpeed);
        _currentBlendY = Mathf.Lerp(_currentBlendY, moveX, blendSpeed);

        playerAnimator.SetFloat("MoveX", _currentBlendX);
        playerAnimator.SetFloat("MoveY", _currentBlendY);
    }
    public void UpdateSprintAnim(bool isSprinting)
    {
        playerAnimator.SetBool("IsSprinting", isSprinting);
    }
    public void UpdateDodgeAnim(float dodgeX, float dodgeY)
    {
        playerAnimator.SetBool("IsDodging", true);

    }

    public void HandleDrawWeapon()
    {
        if (isBusy) return;

        isBusy = true;

        if (!_isWeaponDrawn)
        {
            playerAnimator.SetTrigger("DrawWeapon");
            playerAnimator.SetBool("isArmed", true);
            _isWeaponDrawn = true;
        }
        else
        {
            playerAnimator.SetTrigger("SheathWeapon");
            playerAnimator.SetBool("isArmed", false);
            _isWeaponDrawn = false;
        }
    }
    public void OnAnimationComplete()
    {
        isBusy = false;
    }
    public void LightAttack(string _animName)
    {
        isBusy = true;
        playerAnimator.Play(_animName);
    }
    public void ResetAttack() //TODO
    {
        playerAnimator.SetTrigger("Move");
    }
}
