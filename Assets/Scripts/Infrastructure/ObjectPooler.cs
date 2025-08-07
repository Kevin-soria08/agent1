using System.Collections.Generic;
using UnityEngine;

namespace MyGame.Infrastructure
{
    /// <summary>
    /// A generic object pooler for reusing GameObjects.
    /// </summary>
    public class ObjectPooler
    {
        private readonly Dictionary<GameObject, Queue<GameObject>> _pools = new Dictionary<GameObject, Queue<GameObject>>();

        /// <summary>
        /// Creates a pool for a given prefab with an initial size.
        /// </summary>
        /// <param name="prefab">Prefab to pool.</param>
        /// <param name="initialSize">Initial number of instances to pre-create.</param>
        public void CreatePool(GameObject prefab, int initialSize)
        {
            if (_pools.ContainsKey(prefab)) return;
            var queue = new Queue<GameObject>();
            for (int i = 0; i < initialSize; i++)
            {
                GameObject obj = UnityEngine.Object.Instantiate(prefab);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }

            _pools[prefab] = queue;
        }

        /// <summary>
        /// Spawns an instance of the given prefab from the pool.
        /// </summary>
        /// <param name="prefab">Prefab to spawn.</param>
        /// <param name="position">Position for the new instance.</param>
        /// <param name="rotation">Rotation for the new instance.</param>
        /// <returns>The spawned GameObject.</returns>
        public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            if (!_pools.ContainsKey(prefab))
            {
                CreatePool(prefab, 1);
            }

            var pool = _pools[prefab];
            GameObject obj;
            if (pool.Count > 0)
            {
                obj = pool.Dequeue();
            }
            else
            {
                obj = UnityEngine.Object.Instantiate(prefab);
            }

            obj.transform.SetPositionAndRotation(position, rotation);
            obj.SetActive(true);
            return obj;
        }

        /// <summary>
        /// Returns an instance to its pool.
        /// </summary>
        /// <param name="prefab">The original prefab.</param>
        /// <param name="instance">The instance to return.</param>
        public void Despawn(GameObject prefab, GameObject instance)
        {
            instance.SetActive(false);
            if (!_pools.ContainsKey(prefab))
            {
                _pools[prefab] = new Queue<GameObject>();
            }

            _pools[prefab].Enqueue(instance);
        }
    }
}
