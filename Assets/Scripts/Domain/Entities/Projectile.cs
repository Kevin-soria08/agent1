using UnityEngine;

namespace MyGame.Domain.Entities
{
    /// <summary>
    /// Represents a projectile's data.
    /// </summary>
    public class Projectile
    {
        /// <summary>
        /// Speed of the projectile in units per second.
        /// </summary>
        public float Speed { get; set; }

        /// <summary>
        /// Damage inflicted by the projectile.
        /// </summary>
        public float Damage { get; set; }

        /// <summary>
        /// Maximum travel distance of the projectile.
        /// </summary>
        public float Range { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Projectile"/> class.
        /// </summary>
        /// <param name="speed">Initial speed.</param>
        /// <param name="damage">Damage inflicted.</param>
        /// <param name="range">Maximum travel distance.</param>
        public Projectile(float speed, float damage, float range)
        {
            Speed = speed;
            Damage = damage;
            Range = range;
        }
    }
}
