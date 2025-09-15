using UnityEngine;

namespace GameCore
{
    public class EnemyMinion : Enemy
    {
        protected override void Start()
        {
            MaxHealth = 40;
            MoveSpeed = 2f;
            base.Start();
        }
    }
}