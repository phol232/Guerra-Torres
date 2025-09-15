using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public float Speed = 8f;
    protected int _damage;

    public virtual void Initialize(Transform target, int damage) { _damage = damage; }

    protected virtual void OnTriggerEnter2D(Collider2D collision) { }
}