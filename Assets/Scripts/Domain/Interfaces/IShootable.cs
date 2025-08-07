namespace MyGame.Domain.Interfaces
{
    /// <summary>
    /// Interface for objects that can shoot projectiles.
    /// </summary>
    public interface IShootable
    {
        /// <summary>
        /// Rate of fire expressed as shots per second.
        /// </summary>
        float AttackRate { get; set; }

        /// <summary>
        /// Performs a shooting action.
        /// </summary>
        void Shoot();
    }
}
