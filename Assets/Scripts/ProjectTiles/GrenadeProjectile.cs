using UnityEngine;
using GameCore;

public class GrenadeProjectile : Projectile
{
    private Vector3 _targetPosition;
    private float _explosionRadius = 1.2f;

    public void Initialize(Vector3 targetPos, int damage)
    {
        _targetPosition = targetPos;
        _damage = damage;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, Speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _targetPosition) < 0.1f)
        {
            Explode();
            Destroy(gameObject);
        }
    }

    private void Explode()
    {
        var hits = Physics2D.OverlapCircleAll(transform.position, _explosionRadius, LayerMask.GetMask("Enemy"));
        foreach (var hit in hits)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy) enemy.TakeDamage(_damage);
        }
    }
}