using UnityEngine;

public class PlayerGameplayController : MonoBehaviour
{
    [SerializeField] private LocomotionConfigData locomotionConfig;
    [SerializeField] private AnimationPlayer _animPlayer;
    [SerializeField] PlayerInputReader _inputreader;
    [SerializeField] MovementSystem _movement;
    //Systems
    private LocomotionStateMachine _locomotion;
    private CombatStateMachine _combat;
    private StatusController _status;
    private StateTransitionSystem _transition;
    private DecisionSystem _decision;

    // Actions
    private DrawWeaponAction _drawWeaponAction;

    private void Awake()
    {
        _locomotion = new LocomotionStateMachine(locomotionConfig);
        _drawWeaponAction = new DrawWeaponAction(_animPlayer);
        _status = new StatusController();
        _combat = new CombatStateMachine();
        _transition = new StateTransitionSystem(_locomotion, _combat);
        _decision = new DecisionSystem(_locomotion, _combat, _status, _transition, _drawWeaponAction);
        _movement.Init(_locomotion);
        _animPlayer.Init(_locomotion);
    }

    private void OnEnable()
    {
        _inputreader.OnInputIntent += OnInputReceived;
    }
    private void OnDisable()
    {
        _inputreader.OnInputIntent -= OnInputReceived;
    }
    private void OnInputReceived(InputIntent intent)
    {
        _decision.Evaluate(intent);
        _movement.SetInput(intent.Move);
        _animPlayer.SetInput(intent.Move);
    }
}
