using System.Collections;
using UnityEngine;

public class SheathWeaponAction
{
    private PlayerAnimationPlayer _animPlayer;

    public SheathWeaponAction(PlayerAnimationPlayer animPlayer)
    {
        this._animPlayer = animPlayer;
    }
    public void StartSheathWeapon()
    {
        _animPlayer.PlaySheathWeapon();
    }
}
