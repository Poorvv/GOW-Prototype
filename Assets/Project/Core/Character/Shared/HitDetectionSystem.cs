using UnityEngine;

public class HitDetectionSystem
{
    private Transform _attackPoint;
    private float _radius;
    private LayerMask _targetLayer;
    private FeedbackSystem _feedbackSystem;

    public HitDetectionSystem(Transform attackPoint, float radius, LayerMask targetLayer, FeedbackSystem feedbackSystem)
    {
        _attackPoint = attackPoint;
        _radius = radius;
        _feedbackSystem = feedbackSystem;
        _targetLayer = targetLayer;
    }

    public void DetectHit(AttackInfo attackInfo)
    {
        //RaycastHit hit;
        Collider[] hits = Physics.OverlapSphere(
            _attackPoint.position,
            _radius,
            _targetLayer
        );
        /*if(Physics.Raycast(_attackPoint.position, _attackPoint.forward, out hit, _radius, _targetLayer))
        {
            Debug.Log(hit);
            var health = hit.collider.GetComponent<HealthComponent>();
            if (health != null)
            {
                attackInfo.HitPoint = hit.point;
                attackInfo.HitDirection = (hit.point - attackInfo.Attacker.transform.position).normalized;
                attackInfo.Normal = hit.normal;
                health.TakeDamage(attackInfo);
                _feedbackSystem.PlayHitFeedback(attackInfo.HitPoint, attackInfo.Normal);
            }
        }*/

        foreach (var hit in hits)
        {
            Debug.Log( hit );
            var health = hit.GetComponent<HealthComponent>();
            Vector3 closestPoint = hit.ClosestPoint(_attackPoint.position);
            Vector3 normal = (closestPoint - hit.transform.position).normalized;
            attackInfo.Normal = normal;
            if (health != null)
            {
                attackInfo.HitPoint = hit.transform.position;
                attackInfo.HitDirection = (hit.transform.position - attackInfo.Attacker.transform.position).normalized;

                health.TakeDamage(attackInfo);
                _feedbackSystem.PlayHitFeedback(attackInfo.HitPoint, attackInfo.Normal);
            }
        }
    }
}