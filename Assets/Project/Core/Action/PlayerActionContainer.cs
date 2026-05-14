using UnityEngine;

public class PlayerActionContainer
{
    public DrawWeaponAction DrawWeaponAction { get; private set; }
    public SheathWeaponAction SheathWeaponAction { get; private set; }
    public PlayerAttackAction AttackAction { get; private set; }
    public PlayerActionContainer(DrawWeaponAction drawWeaponAction, SheathWeaponAction sheathWeaponAction, PlayerAttackAction attackAction)
    {
        DrawWeaponAction = drawWeaponAction;
        SheathWeaponAction = sheathWeaponAction;
        AttackAction = attackAction;
    }
}
