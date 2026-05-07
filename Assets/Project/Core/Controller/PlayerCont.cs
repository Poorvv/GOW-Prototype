using UnityEngine;

public class PlayerGameplayController : MonoBehaviour
{
    [SerializeField] private LocomotionConfigData locomotionConfig;
    [SerializeField] private AnimationPlayer animPlayer;
    [SerializeField] private PlayerInputReader inputreader;
    [SerializeField] private MovementSystem movement;
    [SerializeField] private FeedbackSystem feedbackSystem;
    [SerializeField] private AnimationEventRelay animationEventRelay;
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
        inputreader.OnInputIntent += OnInputReceived;
        animationEventRelay.OnWeaponEquipped += OnWeaponEquipped;
        animationEventRelay.OnWeaponUnequipped += OnWeaponUnequipped;
        animationEventRelay.OnComboWindowOpen += _attackAction.OpenComboWindow;
        animationEventRelay.OnComboWindowClose += _attackAction.CloseComboWindow;
        animationEventRelay.OnAttackEnd += _attackAction.OnAttackEnd;
        animationEventRelay.OnHit += _attackAction.OnHit;
    }

    private void Awake()
    {
        _locomotion = new LocomotionStateMachine(locomotionConfig);
        _status = new StatusController();
        _combat = new CombatStateMachine();
        _transition = new StateTransitionSystem(_locomotion, _combat);
        _hitDetectionSystem = new HitDetectionSystem(attackPoint, attackRadius, enemyLayer, feedbackSystem);
        movement.Init(_locomotion);
        animPlayer.Init(_locomotion);

        //Actions
        _drawWeaponAction = new DrawWeaponAction(animPlayer);
        _sheathWeaponAction = new SheathWeaponAction(animPlayer);
        _attackAction = new AttackAction(_transition, animPlayer, _hitDetectionSystem);
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
        movement.SetInput(intent.Move);
        animPlayer.SetInput(intent.Move);
    }
    private void OnDisable()
    {
        inputreader.OnInputIntent -= OnInputReceived;
        animationEventRelay.OnWeaponEquipped -= OnWeaponEquipped;
        animationEventRelay.OnWeaponUnequipped -= OnWeaponUnequipped;
    }
}
