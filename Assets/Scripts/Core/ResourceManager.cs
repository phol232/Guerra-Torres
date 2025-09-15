using UnityEngine;
using System;

namespace GameCore
{
    public class ResourceManager : MonoBehaviour
    {
        public static ResourceManager Instance { get; private set; }

        public int Gold { get; private set; }

        public event Action<int> OnGoldChanged;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject); return;
            }
            Instance = this;
        }

        public void SetInitialGold(int amount)
        {
            Gold = amount;
            OnGoldChanged?.Invoke(Gold);
        }

        public bool TrySpendGold(int amount)
        {
            if (Gold < amount) return false;
            Gold -= amount;
            OnGoldChanged?.Invoke(Gold);
            return true;
        }

        public void AddGold(int amount)
        {
            Gold += amount;
            OnGoldChanged?.Invoke(Gold);
        }
    }
}