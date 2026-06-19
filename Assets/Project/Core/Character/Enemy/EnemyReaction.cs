using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class EnemyReaction : MonoBehaviour
{
    [SerializeField] EnemyAnimationPlayer enemyAnimationPlayer;
    [SerializeField] HealthComponent healthComponent;

    private StatusController _status;

    public void Init(StatusController status)
    {
        _status = status;
    }
    private void OnEnable()
    {
        healthComponent.OnDamageTaken += HandleDamageTaken;
        healthComponent.OnDeath += HandleDeath;
    }

    private void OnDisable()
    {
        healthComponent.OnDamageTaken -= HandleDamageTaken;
        healthComponent.OnDeath -= HandleDeath;
    }

    private void HandleDamageTaken(AttackInfo attackInfo)
    {
        StartCoroutine(StunRoutine());
        enemyAnimationPlayer.PlayHitAnim();
        Debug.Log("Ouch! Enemy hit from direction: " + attackInfo.HitDirection);
    }
    void HandleDeath()
    {
        enemyAnimationPlayer.PlayDeathAnim();
        Debug.Log("Enemy died!");
        var enemyAI = GetComponent<EnemyAI>();
        enemyAI.enabled = false;
        //var agent = GetComponent<NavMeshAgent>();
        //agent.isStopped = true;
        //agent.enabled = false;
        var collider = GetComponent<CapsuleCollider>();
        collider.enabled = false;
        Destroy(gameObject, 5f);
    }
    private IEnumerator StunRoutine()
    {
        _status.Add(StatusType.Stunned);
        Debug.Log("Enemy stunned!");
        yield return new WaitForSeconds(0.8f);
        _status.Remove(StatusType.Stunned);
    }
}
