using UnityEngine;
using GameCore;

public class TowerExplosiva : Tower
{
    [SerializeField] private GameObject _grenadePrefab;
    [SerializeField] private Transform _shootPoint;

    private void Awake()
    {
        m_attackCooldown = 2.5f;
        m_damage = 25;
        m_range = 2.8f;
    }

    protected override void Attack(Enemy target)
    {
        var grenade = Instantiate(_grenadePrefab, _shootPoint.position, Quaternion.identity)
             .GetComponent<GrenadeProjectile>();
        grenade.Initialize(target.transform.position, m_damage);
    }
}