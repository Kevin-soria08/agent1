using System;

namespace MyGame.Domain.Interfaces
{
    /// <summary>
    /// Interface for objects that can take damage and be healed.
    /// </summary>
    public interface IDamageable
    {
        /// <summary>
        /// Current health of the object.
        /// </summary>
        float Health { get; }

        /// <summary>
        /// Takes an amount of damage.
        /// </summary>
        /// <param name="amount">Amount of damage to inflict.</param>
        void TakeDamage(float amount);

        /// <summary>
        /// Heals the object by a certain amount.
        /// </summary>
        /// <param name="amount">Amount of health to restore.</param>
        void Heal(float amount);

        /// <summary>
        /// Event fired when the object's health reaches zero.
        /// </summary>
        event Action OnDeath;
    }
}
