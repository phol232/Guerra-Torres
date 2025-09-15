using UnityEngine;
using System;

namespace GameCore
{
    public class BaseHealth : MonoBehaviour
    {
        public int MaxHealth = 15;
        private int _currentHealth;

        public event Action<int> OnBaseDamaged;

        private void Start()
        {
            _currentHealth = MaxHealth;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth = Mathf.Max(0, _currentHealth - damage);
            OnBaseDamaged?.Invoke(_currentHealth);
            if (_currentHealth <= 0)
            {
                GameManager.Instance.OnBaseDestroyed();
            }
        }

        public int GetCurrentHealth() => _currentHealth;
    }
}