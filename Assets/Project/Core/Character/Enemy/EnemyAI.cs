using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    [Header("References")]
    [SerializeField] Transform player;
    [SerializeField] EnemyAnimationPlayer animator;
    [SerializeField] PlayerAttackAction attackAction;
    [SerializeField] NavMeshAgent agent;

    [Header("Settings")]
    [SerializeField] float chaseRange = 10f;
    [SerializeField] float attackRange = 2f;
    [SerializeField] float attackCooldown = 1.5f;

    private EnemyState _currentState;
    private EnemyAttackAction _enemyAttackAction;
    private float _lastAttackTime;

    public void Init(EnemyAttackAction attackAction)
    {
        _enemyAttackAction = attackAction;
    }
    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        switch (_currentState)
        {
            case EnemyState.Idle:
                HandleIdle(distanceToPlayer);
                break;
            case EnemyState.Chasing:
                HandleChasing(distanceToPlayer);
                break;
            case EnemyState.Attacking:
                HandleAttacking(distanceToPlayer);
                break;
        }
    }
    private void HandleIdle(float distanceToPlayer)
    {
        agent.isStopped = true;
        animator.PlayIdle();
        if (distanceToPlayer <= chaseRange)
        {
            _currentState = EnemyState.Chasing;
        }
    }
    private void HandleChasing(float distanceToPlayer)
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);
        animator.PlayChase();
        if (distanceToPlayer <= attackRange)
        {
            _currentState = EnemyState.Attacking;
        }
        else if (distanceToPlayer > chaseRange)
        {
            
            _currentState = EnemyState.Idle;
        }
    }
    private void HandleAttacking(float distanceToPlayer)
    {

        // If player goes away → chase again
        if (distanceToPlayer > attackRange)
        {
            agent.isStopped = false;
            _currentState = EnemyState.Chasing;
            return;
        }

        agent.isStopped = true;
        //_enemyAttackAction.Attack();
        // Face player
        /*Vector3 lookDir = (player.position - transform.position);
        lookDir.y = 0;
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(lookDir),
            10f * Time.deltaTime
        );*/

        // Cooldown-based attack

        if (Time.time >= _lastAttackTime + attackCooldown)
        {
            _lastAttackTime = Time.time;
            _enemyAttackAction.Attack();
        }
    }
}
