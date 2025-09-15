using UnityEngine;

namespace GameCore
{
    public class EnemyBruto : Enemy
    {
        protected override void Start()
        {
            MaxHealth = 120;
            MoveSpeed = 1.2f;
            m_damage = 2;
            base.Start();
        }
    }
}