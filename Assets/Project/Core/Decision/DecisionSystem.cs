using UnityEngine;

public class DecisionSystem
{
    private LocomotionStateMachine _locomotion;
    private CombatStateMachine _combat;
    private StatusController _status;
    private StateTransitionSystem _transition;
    private PlayerActionContainer _playerActionContainer;
    public DecisionSystem(LocomotionStateMachine locomotion,CombatStateMachine combat,
        StatusController status,
        StateTransitionSystem transition, PlayerActionContainer playerActionContainer)
    {
        this._locomotion = locomotion;
        this._combat = combat;
        this._status = status;
        this._transition = transition;

        this._playerActionContainer = playerActionContainer;
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
        _transition.SetCombat(CombatState.Drawing);
        _playerActionContainer.DrawWeaponAction.StartDrawWeapon();
        Debug.Log("Drawing weapon");
    }
    private void TrySheathWeapon()
    {
        if (_combat.CurrentState != CombatState.Armed) return;
        if (_status.Has(StatusType.Stunned)) return;
        _transition.SetCombat(CombatState.Sheathing);
        _playerActionContainer.SheathWeaponAction.StartSheathWeapon();
        Debug.Log("Sheathing weapon");
    }
    private void TryLightAttack()
    {
        if (_status.Has(StatusType.Stunned)) return;
        if (_combat.CurrentState == CombatState.Unarmed) return;

        _playerActionContainer.AttackAction.HandleLightAttack();  
    }
    public void Evaluate(InputIntent inputIntent)
    {
        if (inputIntent.Move.sqrMagnitude > 0.01f)
        {
            if (inputIntent.SprintPressed && inputIntent.Move.y == 1f)
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
        if(inputIntent.LightAttackPressed)
        {
            TryLightAttack();
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
