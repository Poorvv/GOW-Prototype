using System;
using UnityEngine;

public class AnimationEventRelay : MonoBehaviour
{
    public Action OnWeaponEquipped;
    public Action OnWeaponUnequipped;
    public Action OnComboWindowOpen;
    public Action OnComboWindowClose;
    public Action OnAttackEnd;
    public Action OnHit;
    public void WeaponEquipped() 
    {
        OnWeaponEquipped?.Invoke();
    }
    public void WeaponUnequipped() 
    {
        OnWeaponUnequipped?.Invoke();
    }
    public void OpenComboWindow() => OnComboWindowOpen?.Invoke();
    public void CloseComboWindow() => OnComboWindowClose?.Invoke();
    public void AttackEnd() => OnAttackEnd?.Invoke();
    public void AttackHit()
    {
        OnHit?.Invoke();
    }

}
