using UnityEngine;

public class StateTransitionSystem
{
    private readonly LocomotionStateMachine _locomotion;
    private readonly CombatStateMachine _combat;
    //private readonly InteractionStateMachine _interaction;
    public StateTransitionSystem(LocomotionStateMachine locomotion,
        CombatStateMachine combat//,
        /*InteractionStateMachine interaction*/)
    {
        this._locomotion = locomotion;
        this._combat = combat;
        //this._interaction = interaction;
    }
    public void SetLocomotion(LocomotionState state)
    {
        _locomotion.SetState(state);
    }
    public void EnterDrawWeapon()
    {
        _combat.SetState(CombatState.Drawing);
    }
    public void FinishDrawWeapon() 
    {
        _combat.SetState(CombatState.Armed);
    }
    public void EnterSheathWeapon()
    {
        _combat.SetState(CombatState.Sheathing);
    }
    public void FinishSheathWeapon() 
    {
        _combat.SetState(CombatState.Unarmed);
    }
    public void EnterAttack()
    {
        _combat.SetState(CombatState.Attacking);
    }
    public void ExitAttack()
    {
        _combat.SetState(CombatState.Armed);
    }
    public bool IsAttacking()
    {
        return _combat.CurrentState == CombatState.Attacking;
    }
}
