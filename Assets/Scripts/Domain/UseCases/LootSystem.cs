using System.Collections.Generic;
using MyGame.Infrastructure.DataAdapters;
using MyGame.Core;

namespace MyGame.Domain.UseCases
{
    /// <summary>
    /// Handles loot generation based on loot tables.
    /// </summary>
    public class LootSystem
    {
        private readonly EventBus _eventBus;
        private readonly LootTableDataAdapter _lootTableDataAdapter;

        /// <summary>
        /// Initializes a new instance of the <see cref="LootSystem"/> class.
        /// </summary>
        /// <param name="eventBus">Global event bus for publishing loot events.</param>
        /// <param name="lootTableDataAdapter">Adapter to retrieve loot tables.</param>
        public LootSystem(EventBus eventBus, LootTableDataAdapter lootTableDataAdapter)
        {
            _eventBus = eventBus;
            _lootTableDataAdapter = lootTableDataAdapter;
        }

        /// <summary>
        /// Generates loot from a named loot table.
        /// </summary>
        /// <param name="tableName">Name of the loot table.</param>
        /// <returns>A list of loot item identifiers.</returns>
        public List<string> GenerateLoot(string tableName)
        {
            var table = _lootTableDataAdapter.GetLootTable(tableName);
            var results = new List<string>();
            if (table == null || table.Items == null) return results;

            // Simple random selection based on drop chance
            foreach (var entry in table.Items)
            {
                float roll = UnityEngine.Random.value;
                if (roll <= entry.DropChance)
                {
                    results.Add(entry.ItemKey);
                }
            }

            // Publish an event for generated loot
            _eventBus.Publish(new LootGeneratedEvent(tableName, results));

            return results;
        }
    }

    /// <summary>
    /// Event raised when loot has been generated.
    /// </summary>
    public class LootGeneratedEvent
    {
        /// <summary>
        /// Name of the loot table used.
        /// </summary>
        public string TableName { get; }

        /// <summary>
        /// List of item keys generated.
        /// </summary>
        public IReadOnlyList<string> Items { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LootGeneratedEvent"/> class.
        /// </summary>
        /// <param name="tableName">Name of the loot table.</param>
        /// <param name="items">Generated items.</param>
        public LootGeneratedEvent(string tableName, IReadOnlyList<string> items)
        {
            TableName = tableName;
            Items = items;
        }
    }
}
