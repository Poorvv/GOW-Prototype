using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PlayerInputHandler inputHandler;
    [SerializeField] PlayerAnimController animController;
    [SerializeField] WeaponEquipmentSystem weaponEquipmentSystem;

    [Header("Settings")]
    [SerializeField] float inputBufferTime = 0.3f;

    private int _comboIndex = 0;
    private bool _canAttack = true;
    private bool _bufferedAttack = false;

    private void OnEnable()
    {
        inputHandler.OnLightAttack += LightAttack;
    }

    private void OnDisable()
    {
        inputHandler.OnLightAttack -= LightAttack;
    }

    private void LightAttack()
    {
        // If player pressed attack before animation end, buffer it
        if (!_canAttack)
        {
            StartCoroutine(BufferNextAttack());
            return;
        }

        // Proceed with the next attack
        _comboIndex++;
        _comboIndex = Mathf.Clamp(_comboIndex, 1, 3); // 3 = total combo count
        weaponEquipmentSystem.canChangeEquippedState = false;
        animController.LightAttack("Attack" + _comboIndex);
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
            weaponEquipmentSystem.canChangeEquippedState = true;
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
}
