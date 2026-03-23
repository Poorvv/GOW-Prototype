using UnityEngine;

public class PlayerGameplayController : MonoBehaviour
{
    [SerializeField] private LocomotionConfigData locomotionConfig;
    [SerializeField] PlayerInputReader _inputreader;
    [SerializeField] MovementSystem _movement;
    //Systems
    private LocomotionStateMachine _locomotion;
    private StatusController _status;
    private StateTransitionSystem _transition;
    private DecisionSystem _decision;

    private void Awake()
    {
        _locomotion = new LocomotionStateMachine(locomotionConfig);
        _status = new StatusController();
        _transition = new StateTransitionSystem(_locomotion);
        _decision = new DecisionSystem(_locomotion, _status, _transition);
        _movement.Init(_locomotion);
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
    }
}
