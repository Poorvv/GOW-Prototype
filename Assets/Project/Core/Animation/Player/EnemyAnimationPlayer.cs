using Unity.VisualScripting;
using UnityEngine;

public class EnemyAnimationPlayer : MonoBehaviour
{
    [SerializeField] Animator animator;
    public void PlayHitAnim()
    {
        animator.CrossFade("BodyHit", 0.1f);
    }
    public void PlayAttack()
    {
        animator.CrossFade("Attack", 0.1f);
    }
    public void PlayIdle()
    {
        animator.SetTrigger("Idle");
    }
    public void PlayChase()
    {
        animator.SetTrigger("Chase");
    }
    public void PlayDeathAnim()
    {
        animator.CrossFade("EnemyDeath", 0.1f);
    }
}
