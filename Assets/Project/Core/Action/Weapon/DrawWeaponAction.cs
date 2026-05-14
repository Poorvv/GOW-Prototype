using UnityEngine;

public class DrawWeaponAction
{
    private PlayerAnimationPlayer _animPlayer;
    public DrawWeaponAction(PlayerAnimationPlayer animPlayer)
    {
        this._animPlayer = animPlayer;
    }
    public void StartDrawWeapon()
    {
        _animPlayer.PlayDrawWeapon();

    }
}
