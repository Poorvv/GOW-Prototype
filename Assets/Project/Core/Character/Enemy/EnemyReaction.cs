using UnityEngine;

public class EnemyReaction : MonoBehaviour
{
    [SerializeField] EnemyAnimationPlayer enemyAnimationPlayer;
    [SerializeField] HealthComponent healthComponent;

    private void OnEnable()
    {
        healthComponent.OnDamageTaken += HandleDamageTaken;
    }

    private void OnDisable()
    {
        healthComponent.OnDamageTaken -= HandleDamageTaken;
    }

    private void HandleDamageTaken(AttackInfo attackInfo)
    {
        enemyAnimationPlayer.PlayHitAnim();
        Debug.Log("Ouch! Enemy hit from direction: " + attackInfo.HitDirection);
    }
}
