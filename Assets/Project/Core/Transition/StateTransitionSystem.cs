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
    public void SetCombat(CombatState state)
    {
        _combat.SetState(state);
    }
    public bool IsAttacking()
    {
        return _combat.CurrentState == CombatState.Attacking;
    }
}
