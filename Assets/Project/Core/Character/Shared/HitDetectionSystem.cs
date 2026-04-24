using UnityEngine;

public class HitDetectionSystem
{
    private Transform _attackPoint;
    private float _radius;
    private LayerMask _targetLayer;

    public HitDetectionSystem(Transform attackPoint, float radius, LayerMask targetLayer)
    {
        _attackPoint = attackPoint;
        _radius = radius;
        _targetLayer = targetLayer;
    }

    public void DetectHit(AttackInfo attackInfo)
    {
        Collider[] hits = Physics.OverlapSphere(
            _attackPoint.position,
            _radius,
            _targetLayer
        );

        foreach (var hit in hits)
        {
            Debug.Log( hit );
            var health = hit.GetComponent<HealthComponent>();

            if (health != null)
            {
                attackInfo.HitPoint = hit.transform.position;
                attackInfo.HitDirection = (hit.transform.position - attackInfo.Attacker.transform.position).normalized;

                health.TakeDamage(attackInfo);
            }
        }
    }
}