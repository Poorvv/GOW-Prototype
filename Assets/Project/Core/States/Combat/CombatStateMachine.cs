using UnityEngine;

public class CombatStateMachine
{
    public CombatState CurrentState { get; private set; } = CombatState.Unarmed;

    public void SetState(CombatState newState)
    {
        CurrentState = newState;
    }
}
