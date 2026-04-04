using System.Collections;
using UnityEngine;

public class SheathWeaponAction
{
    private AnimationPlayer _animPlayer;

    public SheathWeaponAction(AnimationPlayer animPlayer)
    {
        this._animPlayer = animPlayer;
    }
    public void StartSheathWeapon()
    {
        _animPlayer.PlaySheathWeapon();
    }
}
