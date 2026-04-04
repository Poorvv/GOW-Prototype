using System;
using UnityEngine;

public class AnimationEventRelay : MonoBehaviour
{
    public Action OnWeaponEquipped;
    public Action OnWeaponUnequipped;
    public void WeaponEquipped() 
    {
        OnWeaponEquipped?.Invoke();
    }
    public void WeaponUnequipped() 
    {
        OnWeaponUnequipped?.Invoke();
    }
    
}
