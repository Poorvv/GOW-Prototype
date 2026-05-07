using UnityEngine;

public class EnemyReaction : MonoBehaviour
{
    [SerializeField] Animator animator;
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
        animator.CrossFade("BodyHit", 0.1f);
        Debug.Log("Ouch! Enemy hit from direction: " + attackInfo.HitDirection);
    }
}
