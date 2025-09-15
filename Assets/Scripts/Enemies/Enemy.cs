using UnityEngine;
using System;
using GameCore;


public abstract class Enemy : MonoBehaviour
{
    public int MaxHealth = 40;
    protected int _currentHealth;
    public float MoveSpeed = 1.5f;
    protected int m_damage = 1;
    protected SimplePath _path;
    protected int _wayPointIndex = 0;
    public bool IsDead { get; private set; }

    public event Action<Enemy> OnEnemyDied;
    public event Action<Enemy> OnReachBase;

    protected virtual void Start()
    {
        _currentHealth = MaxHealth;
        _path = UnityEngine.Object.FindFirstObjectByType<SimplePath>();
    }

    protected virtual void Update()
    {
        MoveAlongPath();
    }

    protected void MoveAlongPath()
    {
        if (!_path || IsDead) return;
        Vector3 target = _path.GetWaypoint(_wayPointIndex);
        transform.position = Vector3.MoveTowards(transform.position, target, MoveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            _wayPointIndex++;
            if (_wayPointIndex >= _path.WaypointCount)
            {
                ReachBase();
            }
        }
    }

    public virtual void TakeDamage(int damage)
    {
        if (IsDead) return;
        _currentHealth -= damage;
        if (_currentHealth <= 0) Die();
    }

    protected virtual void Die()
    {
        IsDead = true;
        ResourceManager.Instance.AddGold(5);
        OnEnemyDied?.Invoke(this);
        Destroy(gameObject);
    }

    protected virtual void ReachBase()
    {
        BaseHealth baseHealth = UnityEngine.Object.FindFirstObjectByType<BaseHealth>();
        baseHealth.TakeDamage(m_damage);
        OnReachBase?.Invoke(this);
        Destroy(gameObject);
    }

    public virtual void Push(Vector2 force) 
    {
        // Add push logic here if needed
        // For example: apply force to rigidbody or modify position
    }
}