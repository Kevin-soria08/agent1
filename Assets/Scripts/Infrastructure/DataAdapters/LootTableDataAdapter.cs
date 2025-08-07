using UnityEngine;
using System.Collections.Generic;

namespace MyGame.Infrastructure.DataAdapters
{
    /// <summary>
    /// Adapter for loading loot tables from ScriptableObjects.
    /// </summary>
    public class LootTableDataAdapter
    {
        private readonly Dictionary<string, LootTable> _cache = new Dictionary<string, LootTable>();

        /// <summary>
        /// Gets a loot table by name.
        /// </summary>
        /// <param name="tableName">Name of the loot table.</param>
        /// <returns>The <see cref="LootTable"/> associated with the name.</returns>
        public LootTable GetLootTable(string tableName)
        {
            if (_cache.TryGetValue(tableName, out var table))
            {
                return table;
            }

            var loaded = Resources.Load<LootTable>($"LootTables/{tableName}");
            if (loaded != null)
            {
                _cache[tableName] = loaded;
            }

            return loaded;
        }
    }

    /// <summary>
    /// Represents a loot table consisting of multiple entries.
    /// </summary>
    [CreateAssetMenu(menuName = "MyGame/Data/LootTable", fileName = "LootTable")]
    public class LootTable : ScriptableObject
    {
        /// <summary>
        /// List of loot entries defining item drop chances.
        /// </summary>
        public List<LootEntry> Items;
    }

    /// <summary>
    /// Represents a single entry in a loot table.
    /// </summary>
    [System.Serializable]
    public class LootEntry
    {
        /// <summary>
        /// Identifier of the item that may drop.
        /// </summary>
        public string ItemKey;

        /// <summary>
        /// Chance of the item dropping (0 to 1).
        /// </summary>
        [Range(0f, 1f)]
        public float DropChance;
    }
}
