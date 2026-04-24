using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class PlayerGameplayController : MonoBehaviour
{
    [SerializeField] private LocomotionConfigData locomotionConfig;
    [SerializeField] private AnimationPlayer _animPlayer;
    [SerializeField] PlayerInputReader _inputreader;
    [SerializeField] MovementSystem _movement;
    [SerializeField] AnimationEventRelay _animationEventRelay;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius = 1.5f;
    [SerializeField] private LayerMask enemyLayer;
    //Systems
    private LocomotionStateMachine _locomotion;
    private CombatStateMachine _combat;
    private StatusController _status;
    private StateTransitionSystem _transition;
    private DecisionSystem _decision;
    private HitDetectionSystem _hitDetectionSystem;

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
        _animationEventRelay.OnComboWindowOpen += _attackAction.OpenComboWindow;
        _animationEventRelay.OnComboWindowClose += _attackAction.CloseComboWindow;
        _animationEventRelay.OnAttackEnd += _attackAction.OnAttackEnd;
        _animationEventRelay.OnHit += _attackAction.OnHit;
    }

    private void Awake()
    {
        _locomotion = new LocomotionStateMachine(locomotionConfig);
        _status = new StatusController();
        _combat = new CombatStateMachine();
        _transition = new StateTransitionSystem(_locomotion, _combat);
        _hitDetectionSystem = new HitDetectionSystem(attackPoint, attackRadius, enemyLayer);
        _movement.Init(_locomotion);
        _animPlayer.Init(_locomotion);

        //Actions
        _drawWeaponAction = new DrawWeaponAction(_animPlayer);
        _sheathWeaponAction = new SheathWeaponAction(_animPlayer);
        _attackAction = new AttackAction(_transition, _animPlayer, _hitDetectionSystem);
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
