using UnityEngine;
using System.Collections.Generic;

namespace MyGame.Infrastructure.DataAdapters
{
    /// <summary>
    /// Adapter for loading character stats data from ScriptableObjects.
    /// </summary>
    public class StatsDataAdapter
    {
        private readonly Dictionary<string, StatsData> _cache = new Dictionary<string, StatsData>();

        /// <summary>
        /// Gets stats data by key.
        /// </summary>
        /// <param name="key">Key of the stats data.</param>
        /// <returns>The <see cref="StatsData"/> associated with the key.</returns>
        public StatsData GetStats(string key)
        {
            if (_cache.TryGetValue(key, out var data))
            {
                return data;
            }

            var loaded = Resources.Load<StatsData>($"Stats/{key}");
            if (loaded != null)
            {
                _cache[key] = loaded;
            }

            return loaded;
        }
    }

    /// <summary>
    /// Represents base stats for a character or entity.
    /// </summary>
    [CreateAssetMenu(menuName = "MyGame/Data/Stats", fileName = "StatsData")]
    public class StatsData : ScriptableObject
    {
        /// <summary>
        /// Maximum health value.
        /// </summary>
        public float MaxHealth;

        /// <summary>
        /// Base movement speed.
        /// </summary>
        public float MovementSpeed;

        /// <summary>
        /// Base attack power.
        /// </summary>
        public float AttackDamage;
    }
}
