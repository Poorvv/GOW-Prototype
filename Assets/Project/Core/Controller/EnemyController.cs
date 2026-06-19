using UnityEditor.Timeline.Actions;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] EnemyAnimationPlayer animationPlayer;
    [SerializeField] AnimationEventRelay animationRelay;
    [SerializeField] FeedbackSystem feedbackSystem;
    [SerializeField] EnemyReaction enemyReaction;
    [SerializeField] Transform attackPoint;
    [SerializeField] LayerMask targetLayer;

    private EnemyAttackAction _attackAction;
    private StatusController _statusController;
    private void OnEnable()
    {
        animationRelay.OnHit +=
            () => _attackAction.OnAttackHit(gameObject);

        animationRelay.OnAttackEnd +=
            _attackAction.OnAttackEnd;
    }
    private void OnDisable()
    {
        animationRelay.OnHit -=
            () => _attackAction.OnAttackHit(gameObject);
        animationRelay.OnAttackEnd -=
            _attackAction.OnAttackEnd;
    }

    private void Awake()
    {
        _statusController = new StatusController();
        enemyReaction.Init(_statusController);
        CombatStateMachine combat = new CombatStateMachine();

        HitDetectionSystem hitDetection =
            new HitDetectionSystem(
                attackPoint,
                1.5f,
                targetLayer,
                feedbackSystem
            );

        _attackAction = new EnemyAttackAction(
                combat,
                hitDetection,
                animationPlayer,
                feedbackSystem,
                _statusController
            );

        GetComponent<EnemyAI>()
            .Init(_attackAction);

    }
}