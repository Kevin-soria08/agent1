using UnityEngine;

namespace MyGame.Domain.Interfaces
{
    /// <summary>
    /// Interface for objects that can move.
    /// </summary>
    public interface IMovable
    {
        /// <summary>
        /// Movement speed in units per second.
        /// </summary>
        float Speed { get; set; }

        /// <summary>
        /// Moves the object in the given direction.
        /// </summary>
        /// <param name="direction">Normalized movement direction.</param>
        void Move(Vector2 direction);
    }
}
