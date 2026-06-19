using UnityEngine;
using System.Collections;
public class PlayerReaction : MonoBehaviour
{
    [SerializeField] HealthComponent health;
    [SerializeField] PlayerAnimationPlayer animator;

    private StatusController _status;

    public void Init(StatusController status)
    {
        _status = status;
    }
    private void OnEnable()
    {
        health.OnDamageTaken += HandleDamageTaken;
        health.OnDeath += HandleDeath;
    }

    private void OnDisable()
    {
        health.OnDamageTaken -= HandleDamageTaken;
        health.OnDeath -= HandleDeath;
    }
    void HandleDamageTaken(AttackInfo attackInfo)
    {
        //Debug.Log($"Player took {damage} damage!");
        StartCoroutine(StunRoutine());
        animator.PlayHitAnim();
    }
    void HandleDeath()
    {
        var collider = GetComponent<CapsuleCollider>();
        collider.enabled = false;
        animator.PlayDeathAnim();
        Debug.Log("Player died!");
        // Implement respawn or game over logic here
    }
    private IEnumerator StunRoutine()
    {
        _status.Add(StatusType.Stunned);
        Debug.Log("Enemy stunned!");
        yield return new WaitForSeconds(0.8f);
        _status.Remove(StatusType.Stunned);
    }
}
