using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerGameplayController : MonoBehaviour
{
    [SerializeField] private LocomotionConfigData locomotionConfig;
    [SerializeField] private AnimationPlayer _animPlayer;
    [SerializeField] PlayerInputReader _inputreader;
    [SerializeField] MovementSystem _movement;
    [SerializeField] AnimationEventRelay _animationEventRelay;
    //Systems
    private LocomotionStateMachine _locomotion;
    private CombatStateMachine _combat;
    private StatusController _status;
    private StateTransitionSystem _transition;
    private DecisionSystem _decision;


    // Actions
    private PlayerActionContainer _playerActionContainer;
    private DrawWeaponAction _drawWeaponAction;
    private SheathWeaponAction _sheathWeaponAction;
    private AttackAction _attackAction;

    private void OnEnable()
    {
        _inputreader.OnInputIntent += OnInputReceived;
        _animationEventRelay.OnWeaponEquipped += OnWeaponEquipped;
        _animationEventRelay.OnWeaponUnequipped += OnWeaponUnequipped;
    }

    private void Awake()
    {
        _locomotion = new LocomotionStateMachine(locomotionConfig);
        _status = new StatusController();
        _combat = new CombatStateMachine();
        _transition = new StateTransitionSystem(_locomotion, _combat);
        _movement.Init(_locomotion);
        _animPlayer.Init(_locomotion);

        //Actions
        _drawWeaponAction = new DrawWeaponAction(_animPlayer);
        _sheathWeaponAction = new SheathWeaponAction(_animPlayer);
        //_attackAction = new AttackAction(); TODO: Implement AttackAction
        _playerActionContainer = new PlayerActionContainer(_drawWeaponAction, _sheathWeaponAction, _attackAction);
        _decision = new DecisionSystem(_locomotion, _combat, _status, _transition, _playerActionContainer);
        
    }

    private void OnWeaponEquipped()
    {
        _transition.FinishDrawWeapon();
        Debug.Log("Weapon Equipped");
    }
    private void OnWeaponUnequipped()
    {
        _transition.FinishSheathWeapon();
        Debug.Log("Weapon Unequipped");
    }
    private void OnInputReceived(InputIntent intent)
    {
        _decision.Evaluate(intent);
        _movement.SetInput(intent.Move);
        _animPlayer.SetInput(intent.Move);
    }
    private void OnDisable()
    {
        _inputreader.OnInputIntent -= OnInputReceived;
        _animationEventRelay.OnWeaponEquipped -= OnWeaponEquipped;
        _animationEventRelay.OnWeaponUnequipped -= OnWeaponUnequipped;
    }
}
