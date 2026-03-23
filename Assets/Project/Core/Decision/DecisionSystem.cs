using UnityEngine;

public class DecisionSystem
{
    private LocomotionStateMachine _locomotion;
    private StatusController _status;
    private StateTransitionSystem _transition;
    public DecisionSystem(LocomotionStateMachine locomotion,
        StatusController status,
        StateTransitionSystem transition)
    {
        this._locomotion = locomotion;
        this._status = status;
        this._transition = transition;
    }
    public void TrySprint()
    {
        if (_locomotion.CurrentState != LocomotionState.Sprint) return;
        if (_status.Has(StatusType.Stunned)) return;

        _transition.SetLocomotion(LocomotionState.Sprint);
        Debug.Log("Sprinting");
    }
    public void Evaluate(InputIntent inputIntent)
    {
        if (inputIntent.Move.sqrMagnitude > 0.01f)
        {
            if (inputIntent.SprintPressed)
                _transition.SetLocomotion(LocomotionState.Sprint);
            else
                _transition.SetLocomotion(LocomotionState.Walk);
        }
        else
        {
            _transition.SetLocomotion(LocomotionState.Idle);
        }
        /*if(inputIntent.DrawWeaponPressed)
        {
            Debug.Log("Drawing weapon");
        }
        if(inputIntent.LightAttackPressed)
        {
            Debug.Log("Light attack");
        }
        if(inputIntent.HeavyAttackPressed)
        {
            Debug.Log("Heavy attack");
        }*/
    }
}
