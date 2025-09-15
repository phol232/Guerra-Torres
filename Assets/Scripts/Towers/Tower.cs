using UnityEngine;
using GameCore;


public abstract class Tower : MonoBehaviour
{
    [SerializeField] protected float m_range = 3f;
    [SerializeField] protected float m_attackCooldown = 1f;
    [SerializeField] protected int m_damage = 5;

    protected float _lastAttackTime = 0f;

    protected virtual void Update()
    {
        if (Time.time > _lastAttackTime + m_attackCooldown)
        {
            Enemy target = GetTarget();
            if (target)
            {
                Attack(target);
                _lastAttackTime = Time.time;
            }
        }
    }

    protected Enemy GetTarget()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, m_range, LayerMask.GetMask("Enemy"));
        float minDist = float.MaxValue;
        Enemy target = null;
        foreach (var col in enemies)
        {
            float dist = Vector2.Distance(col.transform.position, transform.position);
            if (dist < minDist)
            {
                Enemy e = col.GetComponent<Enemy>();
                if (e && !e.IsDead)
                {
                    minDist = dist;
                    target = e;
                }
            }
        }
        return target;
    }

    protected abstract void Attack(Enemy target);

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_range);
    }
}