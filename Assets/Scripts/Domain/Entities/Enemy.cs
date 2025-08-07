using System;
 using UnityEngine;
using MyGame.Domain.Interfaces;

namespace MyGame.Domain.Entities
{
    /// <summary>
    /// Represents an enemy with stats and simple AI parameters.
    /// </summary>
    public class Enemy : IDamageable, IMovable, IShootable
    {
        /// <inheritdoc />
        public float Health { get; private set; }

        /// <inheritdoc />
        public float Speed { get; set; }

        /// <inheritdoc />
        public float AttackRate { get; set; }

        /// <summary>
        /// Damage inflicted when attacking.
        /// </summary>
        public float Damage { get; set; }

        /// <inheritdoc />
        public event Action OnDeath;

        /// <summary>
        /// Initializes a new instance of the <see cref="Enemy"/> class.
        /// </summary>
        /// <param name="health">Starting health value.</param>
        /// <param name="speed">Movement speed.</param>
        /// <param name="attackRate">Rate of fire.</param>
        /// <param name="damage">Damage inflicted per attack.</param>
        public Enemy(float health, float speed, float attackRate, float damage)
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
            // Domain entity does not implement actual movement.
        }

        /// <inheritdoc />
        public void Shoot()
        {
            // Domain entity does not implement actual shooting.
        }
    }
}
