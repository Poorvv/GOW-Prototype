using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PlayerInputHandler inputHandler;
    [SerializeField] PlayerAnimController animController;
    [SerializeField] WeaponManager weaponManager;
    [SerializeField] WeaponEquipmentSystem weaponEquipmentSystem;

    [Header("Settings")]
    [SerializeField] float inputBufferTime = 0.3f;
    [SerializeField] int comboMinLimit;
    [SerializeField] int comboMaxLimit;
    private int _comboIndex = 0;
    private bool _canAttack = true;
    private bool _bufferedAttack = false;
    private void OnEnable()
    {
        inputHandler.OnLightAttack += LightAttack;
        inputHandler.OnHeavyAttack += HeavyAttack;
    }

    private void OnDisable()
    {
        inputHandler.OnLightAttack -= LightAttack;
        inputHandler.OnHeavyAttack -= HeavyAttack;
    }
    private void LightAttack()
    {
        TryAttack(false);
    }
    private void HeavyAttack()
    {
        TryAttack(true);
    }

    private void TryAttack(bool _isHeavy)
    {
        if (!_canAttack)
        {
            StartCoroutine(BufferNextAttack());
            _bufferedAttack = true;
            return;
        }
        if (!weaponEquipmentSystem.ISWeaponEquipped)
        {
            return;
        }
        AttackContext _attackContext = DetermineAttackContext();
        // Proceed with the next attack
        _comboIndex++;
        _comboIndex = Mathf.Clamp(_comboIndex, comboMinLimit, comboMaxLimit); // 3 = total combo count

        weaponEquipmentSystem.CanChangeEquippedState = false;
        if (_attackContext == AttackContext.Running) 
        {
            weaponManager.PerformSpecialAttack(_isHeavy, AttackContext.Running);
        }
        weaponManager.PerformAttack(_isHeavy, _comboIndex);
        print(_comboIndex);
        _canAttack = false; // Lock further attacks until animation ends
    }
    private IEnumerator BufferNextAttack()
    {
        _bufferedAttack = true;
        yield return new WaitForSeconds(inputBufferTime);
        _bufferedAttack = false;
    }

    // 🔥 Called by Animation Event at END of each attack animation
    public void OnAttackAnimationEnd()
    {
        _canAttack = true;
        if (_bufferedAttack && _comboIndex < 3)
        {
            // Continue combo if player pressed attack during animation
            _bufferedAttack = false;

            LightAttack();
            
        }
        else
        {
            // Reset combo instantly when animation ends and no input buffered
            weaponEquipmentSystem.CanChangeEquippedState = true;
            animController.OnAnimationComplete();
            ResetCombo();
        }
    }

    private void ResetCombo()
    {
        _comboIndex = 0;
        _canAttack = true;
        animController.ResetAttack(); // Transitions back to movement/idle anim
    }
    private AttackContext DetermineAttackContext()
    {
        if (inputHandler.SprintTriggered)
        {
            return AttackContext.Running;
        }
        else
        {
            return AttackContext.Normal;
        }
    }
}
