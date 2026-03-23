using UnityEngine;

public class LocomotionStateMachine
{
    public LocomotionState CurrentState{get; private set; } = LocomotionState.Idle;
    private LocomotionConfigData config;

    public LocomotionStateMachine(LocomotionConfigData config)
    {
        this.config = config;
    }
    public void SetState(LocomotionState newState)
    {
        CurrentState = newState;
    }
    public float GetSpeed()
    {
        return CurrentState switch
        {
            LocomotionState.Idle => 0f,
            LocomotionState.Walk => config.WalkSpeed,
            LocomotionState.Sprint => config.SprintSpeed,
            _ => 0f
        };
    }
    public float GetRotationSpeed()
    {
        return config.RotationSpeed;
    }
}
