using UnityEngine;
using GameCore;


public class ArrowProjectile : Projectile
{
    private Transform _target;

    public override void Initialize(Transform target, int damage)
    {
        _target = target;
        _damage = damage;
    }

    private void Update()
    {
        if (!_target || !_target.gameObject.activeSelf)
        {
            Destroy(gameObject); return;
        }

        transform.position = Vector3.MoveTowards(transform.position, _target.position, Speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _target.position) < 0.1f)
        {
            Enemy enemy = _target.GetComponent<Enemy>();
            if (enemy) enemy.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}