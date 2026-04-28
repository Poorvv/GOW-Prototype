using UnityEngine;

public class EnemyReaction : MonoBehaviour
{
    [SerializeField] Animator animator;
    public void PlayHitReaction(Vector3 direction)
    {
        animator.CrossFade("BodyHit", 0.1f);
        Debug.Log("Ouch! Enemy hit from direction: " + direction);
    }
}
