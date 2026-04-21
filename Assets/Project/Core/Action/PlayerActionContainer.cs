using UnityEngine;

public class PlayerActionContainer
{
    public DrawWeaponAction DrawWeaponAction { get; private set; }
    public SheathWeaponAction SheathWeaponAction { get; private set; }
    public AttackAction AttackAction { get; private set; }
    public PlayerActionContainer(DrawWeaponAction drawWeaponAction, SheathWeaponAction sheathWeaponAction, AttackAction attackAction)
    {
        DrawWeaponAction = drawWeaponAction;
        SheathWeaponAction = sheathWeaponAction;
        AttackAction = attackAction;
    }
}
