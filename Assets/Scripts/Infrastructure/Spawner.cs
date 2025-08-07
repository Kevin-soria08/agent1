using System.Collections.Generic;
using UnityEngine;

namespace MyGame.Infrastructure
{
    /// <summary>
    /// Spawns enemies or items at random positions using an object pooler.
    /// </summary>
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> _spawnPrefabs = new List<GameObject>();

        [SerializeField]
        private float _spawnInterval = 2f;

        [SerializeField]
        private float _spawnRadius = 5f;

        private float _timeSinceLastSpawn;
        private ObjectPooler _pooler;

        private void Awake()
        {
            _pooler = new ObjectPooler();
            foreach (var prefab in _spawnPrefabs)
            {
                _pooler.CreatePool(prefab, 10);
            }
        }

        private void Update()
        {
            _timeSinceLastSpawn += Time.deltaTime;
            if (_timeSinceLastSpawn >= _spawnInterval)
            {
                SpawnRandom();
                _timeSinceLastSpawn = 0f;
            }
        }

        /// <summary>
        /// Spawns a random prefab from the list at a random position.
        /// </summary>
        public void SpawnRandom()
        {
            if (_spawnPrefabs.Count == 0) return;
            int index = UnityEngine.Random.Range(0, _spawnPrefabs.Count);
            GameObject prefab = _spawnPrefabs[index];
            Vector2 position = (Vector2)transform.position + UnityEngine.Random.insideUnitCircle * _spawnRadius;
            GameObject instance = _pooler.Spawn(prefab, position, Quaternion.identity);
            instance.SetActive(true);
        }
    }
}
