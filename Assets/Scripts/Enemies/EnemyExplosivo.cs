using UnityEngine;

namespace GameCore
{
    public class EnemyExplosivo : Enemy
    {
        [SerializeField] private float _explosionRadius = 1.5f;
        [SerializeField] private int _explosionDamage = 20;

        protected override void Start()
        {
            MaxHealth = 25;
            MoveSpeed = 3f;
            base.Start();
        }

        protected override void Die()
        {
            var hits = Physics2D.OverlapCircleAll(transform.position, _explosionRadius, LayerMask.GetMask("Hero"));
            foreach (var h in hits)
            {
                HeroController hero = h.GetComponent<HeroController>();
                if (hero)
                {
                    hero.TakeDamage(_explosionDamage);
                }
            }
            base.Die();
        }
    }
}