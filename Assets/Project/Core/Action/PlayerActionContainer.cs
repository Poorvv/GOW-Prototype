using UnityEngine;

public class PlayerActionContainer
{
    public DrawWeaponAction DrawWeaponAction { get; private set; }
    public SheathWeaponAction SheathWeaponAction { get; private set; }
    public LightAttackAction LightAttackAction { get; private set; }
    public PlayerActionContainer(DrawWeaponAction drawWeaponAction, SheathWeaponAction sheathWeaponAction, LightAttackAction lightAttackAction)
    {
        DrawWeaponAction = drawWeaponAction;
        SheathWeaponAction = sheathWeaponAction;
        LightAttackAction = lightAttackAction;
    }
}
