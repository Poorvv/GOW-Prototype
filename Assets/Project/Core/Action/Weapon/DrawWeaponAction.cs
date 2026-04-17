using UnityEngine;

public class DrawWeaponAction
{
    private AnimationPlayer _animPlayer;
    public DrawWeaponAction(AnimationPlayer animPlayer)
    {
        this._animPlayer = animPlayer;
    }
    public void StartDrawWeapon()
    {
        _animPlayer.PlayDrawWeapon();

    }
}
