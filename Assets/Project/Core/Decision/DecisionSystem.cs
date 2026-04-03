using UnityEngine;

public class DecisionSystem
{
    private LocomotionStateMachine _locomotion;
    private CombatStateMachine _combat;
    private StatusController _status;
    private StateTransitionSystem _transition;
    private DrawWeaponAction _drawWeaponAction;
    public DecisionSystem(LocomotionStateMachine locomotion,CombatStateMachine combat,
        StatusController status,
        StateTransitionSystem transition, DrawWeaponAction drawWeaponAction)
    {
        this._locomotion = locomotion;
        this._combat = combat;
        this._status = status;
        this._transition = transition;
        this._drawWeaponAction = drawWeaponAction;
    }
    public void TrySprint()
    {
        if (_locomotion.CurrentState != LocomotionState.Sprint) return;
        if (_status.Has(StatusType.Stunned)) return;

        _transition.SetLocomotion(LocomotionState.Sprint);
        Debug.Log("Sprinting");
    }
    private void TryDrawWeapon()
    {
        if (_combat.CurrentState != CombatState.Unarmed) return;
        if (_status.Has(StatusType.Stunned)) return;
        _transition.EnterDrawWeapon();
        _drawWeaponAction.StartDrawWeapon();
        Debug.Log("Drawing weapon");
    }
    private void TrySheathWeapon()
    {
        if (_combat.CurrentState != CombatState.Armed) return;
        if (_status.Has(StatusType.Stunned)) return;
        _transition.EnterSheathWeapon();
        Debug.Log("Sheathing weapon");
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
        if(inputIntent.ToggleWeaponPressed)
        {
            HandleWeaponToggle();
        }
    }
    private void HandleWeaponToggle()
    {

        switch (_combat.CurrentState)
        {
            case CombatState.Unarmed:
                TryDrawWeapon();
                break;
            case CombatState.Armed:
                TrySheathWeapon();
                break;
            default:
                break;
        }
    }
}
