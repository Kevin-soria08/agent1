using System;
using UnityEngine;
using MyGame.Domain.Interfaces;

namespace MyGame.Domain.Entities
{
    /// <summary>
    /// Represents player data and behavior independent of Unity components.
    /// </summary>
    public class Player : IDamageable, IMovable, IShootable
    {
        /// <inheritdoc />
        public float Health { get; private set; }

        /// <inheritdoc />
        public float Speed { get; set; }

        /// <inheritdoc />
        public float AttackRate { get; set; }

        /// <summary>
        /// Damage dealt by the player's projectiles.
        /// </summary>
        public float Damage { get; set; }

        /// <inheritdoc />
        public event Action OnDeath;

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class with default stats.
        /// </summary>
        /// <param name="health">Starting health value.</param>
        /// <param name="speed">Movement speed.</param>
        /// <param name="attackRate">Rate of fire (shots per second).</param>
        /// <param name="damage">Damage per shot.</param>
        public Player(float health, float speed, float attackRate, float damage)
        {
            Health = health;
            Speed = speed;
            AttackRate = attackRate;
            Damage = damage;
        }

        /// <inheritdoc />
        public void TakeDamage(float amount)
        {
            Health = Mathf.Max(0f, Health - amount);
            if (Health <= 0f)
            {
                OnDeath?.Invoke();
            }
        }

        /// <inheritdoc />
        public void Heal(float amount)
        {
            Health += amount;
        }

        /// <inheritdoc />
        public void Move(Vector2 direction)
        {
            // Domain-only entity does not implement movement logic.
            // Movement is handled by a controller in the Presentation layer.
        }

        /// <inheritdoc />
        public void Shoot()
        {
            // Domain-only entity does not spawn projectiles.
            // Shooting is handled by a controller in the Presentation layer.
        }
    }
}
