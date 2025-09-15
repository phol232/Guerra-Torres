using UnityEngine;
using GameCore;


public class TowerFlechera : Tower
{
    [SerializeField] private GameObject _arrowPrefab;
    [SerializeField] private Transform _shootPoint;

    private void Awake()
    {
        m_attackCooldown = 0.75f;
        m_damage = 5;
        m_range = 3.5f;
    }

    protected override void Attack(Enemy target)
    {
        var arrow = Instantiate(_arrowPrefab, _shootPoint.position, Quaternion.identity)
            .GetComponent<ArrowProjectile>();
        arrow.Initialize(target.transform, m_damage);
    }
}