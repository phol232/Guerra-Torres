using UnityEngine;


public class HeroWaveProjectile : Projectile
{
    private float _radius;
    public void Initialize(int damage, float radius)
    {
        _damage = damage;
        _radius = radius;
        Explode();
    }

    private void Explode()
    {
        var hits = Physics2D.OverlapCircleAll(transform.position, _radius, LayerMask.GetMask("Enemy"));
        foreach (var hit in hits)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.TakeDamage(_damage);
                enemy.Push((enemy.transform.position - transform.position).normalized * 3f);
            }
        }
        Destroy(gameObject);
    }
}