using UnityEngine;
using System.Collections.Generic;

namespace MyGame.Infrastructure.DataAdapters
{
    /// <summary>
    /// Adapter for loading weapon data from ScriptableObjects.
    /// </summary>
    public class WeaponDataAdapter
    {
        private readonly Dictionary<string, WeaponData> _cache = new Dictionary<string, WeaponData>();

        /// <summary>
        /// Gets a weapon data asset by key.
        /// </summary>
        /// <param name="key">Key of the weapon.</param>
        /// <returns>The <see cref="WeaponData"/> associated with the key.</returns>
        public WeaponData GetWeaponData(string key)
        {
            if (_cache.TryGetValue(key, out var data))
            {
                return data;
            }

            // Attempt to load from Resources folder
            var loaded = Resources.Load<WeaponData>($"Weapons/{key}");
            if (loaded != null)
            {
                _cache[key] = loaded;
            }

            return loaded;
        }
    }

    /// <summary>
    /// Represents weapon configuration data.
    /// </summary>
    [CreateAssetMenu(menuName = "MyGame/Data/Weapon", fileName = "WeaponData")]
    public class WeaponData : ScriptableObject
    {
        /// <summary>
        /// Name of the weapon.
        /// </summary>
        public string WeaponName;

        /// <summary>
        /// Damage dealt by the weapon.
        /// </summary>
        public float Damage;

        /// <summary>
        /// Rate of fire (shots per second).
        /// </summary>
        public float RateOfFire;

        /// <summary>
        /// Projectile speed.
        /// </summary>
        public float ProjectileSpeed;
    }
}
