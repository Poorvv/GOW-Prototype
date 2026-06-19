using UnityEngine;

public class EnemyAttackAction
{
    private readonly CombatStateMachine _combat;
    private readonly HitDetectionSystem _hitDetection;
    private readonly EnemyAnimationPlayer _animPlayer;
    private readonly FeedbackSystem _feedback;
    private readonly StatusController _status;

    public EnemyAttackAction(CombatStateMachine combat, HitDetectionSystem hitDetection,EnemyAnimationPlayer animPlayer,
        FeedbackSystem feedback, StatusController status)
    {
        _combat = combat;
        _hitDetection = hitDetection;
        _animPlayer = animPlayer;
        _feedback = feedback;
        _status = status;
    }
    public void Attack()
    {
        if (_status.Has(StatusType.Stunned))
            return;
        if (_combat.CurrentState == CombatState.Attacking)
            return;

        _combat.SetState(CombatState.Attacking);
        _animPlayer.PlayAttack();
    }
    public void OnAttackHit(GameObject attacker)
    {
        AttackInfo info = new AttackInfo
        {
            Attacker = attacker,
            Damage = 20, // Example damage value
        };

        _hitDetection.DetectHit(info);
    }
    public void OnAttackEnd()
    {
        _animPlayer.PlayIdle();
        _combat.SetState(CombatState.Armed);
    }
}
