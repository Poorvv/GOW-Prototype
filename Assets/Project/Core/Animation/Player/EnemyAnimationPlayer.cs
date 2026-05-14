using Unity.VisualScripting;
using UnityEngine;

public class EnemyAnimationPlayer : MonoBehaviour
{
    [SerializeField] Animator animator;
    public void PlayHitAnim()
    {
        animator.CrossFade("BodyHit", 0.1f);
    }
}
