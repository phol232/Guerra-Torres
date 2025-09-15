using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "DB/EnemyDatabase")]
public class EnemyDatabase : ScriptableObject
{
    private static EnemyDatabase _instance;
    public static EnemyDatabase Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<EnemyDatabase>("EnemyDatabase");
                if (_instance == null)
                {
                    Debug.LogError("EnemyDatabase Instance not found. Create one via Create > DB > EnemyDatabase and place it under a Resources folder named 'EnemyDatabase.asset'.");
                }
            }
            return _instance;
        }
    }
    [System.Serializable]
    public struct EnemyEntry
    {
        public string type;
        public GameObject prefab;
    }

    [SerializeField] private List<EnemyEntry> _enemies;

    public GameObject GetEnemyPrefab(string type)
    {
        foreach (var entry in _enemies)
        {
            if (entry.type == type) return entry.prefab;
        }
        Debug.LogError("Enemy prefab not found: " + type);
        return null;
    }
}