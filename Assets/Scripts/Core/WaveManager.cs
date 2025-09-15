using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class WaveData
    {
        public List<EnemySpawnInfo> enemies;
        public float spawnInterval;
    }

    [System.Serializable]
    public struct EnemySpawnInfo
    {
        public string enemyType; 
        public int count;
    }

    [SerializeField] private List<WaveData> _waves;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private EnemyDatabase _enemyDatabase; 

    public int CurrentWave { get; private set; } = 0;

    private int _enemiesInWave;
    private int _enemiesAlive;

    public event System.Action<int> OnWaveStarted;
    public event System.Action<int> OnWaveCompleted;

    private void Start()
    {
        StartCoroutine(WaveRoutine());
    }

    private IEnumerator WaveRoutine()
    {
        for (int i = 0; i < _waves.Count; i++)
        {
            CurrentWave = i + 1;
            OnWaveStarted?.Invoke(CurrentWave);
            yield return StartCoroutine(SpawnWave(_waves[i]));
            while (_enemiesAlive > 0) yield return null;
            OnWaveCompleted?.Invoke(CurrentWave);
            yield return new WaitForSeconds(2f);
        }

        GameManager.Instance.OnVictory();
    }

    private IEnumerator SpawnWave(WaveData wave)
    {
        foreach (var info in wave.enemies)
        {
            for (int i = 0; i < info.count; i++)
            {
                SpawnEnemy(info.enemyType);
                _enemiesInWave++;
                _enemiesAlive++;
                yield return new WaitForSeconds(wave.spawnInterval);
            }
        }
    }

    private void SpawnEnemy(string type)
    {
        var db = _enemyDatabase ? _enemyDatabase : EnemyDatabase.Instance;
        if (db == null) { Debug.LogError("EnemyDatabase not found. Assign it in WaveManager or place one in Resources/EnemyDatabase."); return; }
        GameObject prefab = db.GetEnemyPrefab(type);
        GameObject go = Instantiate(prefab, _spawnPoint.position, Quaternion.identity);
        Enemy enemy = go.GetComponent<Enemy>();
        if (enemy == null)
        {
            Debug.LogError($"Spawned prefab for '{type}' does not have an Enemy component.");
            return;
        }
        enemy.OnEnemyDied += HandleEnemyKilled;
        enemy.OnReachBase += HandleEnemyKilled;
    }

    private void HandleEnemyKilled(Enemy enemy)
    {
        _enemiesAlive = Mathf.Max(0, _enemiesAlive - 1);
    }
}