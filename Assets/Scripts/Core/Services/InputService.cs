using System;
using UnityEngine;
using MyGame.Core;

namespace MyGame.Core.Services
{
    /// <summary>
    /// Provides methods to read and interpret player input for a twin-stick shooter.
    /// </summary>
    public class InputService
    {
        private EventBus _eventBus;

        /// <summary>
        /// Initializes the input service and registers it with the event bus.
        /// </summary>
        /// <param name="eventBus">The global event bus.</param>
        public void Initialize(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        /// <summary>
        /// Gets the movement vector from the left stick.
        /// </summary>
        public Vector2 MovementVector
        {
            get
            {
                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");
                return new Vector2(horizontal, vertical);
            }
        }

        /// <summary>
        /// Gets the aiming vector from the right stick or mouse.
        /// </summary>
        public Vector2 AimVector
        {
            get
            {
                // For twin-stick controllers, map right stick axes; fall back to mouse position if needed.
                float aimX = Input.GetAxis("AimHorizontal");
                float aimY = Input.GetAxis("AimVertical");
                if (Mathf.Approximately(aimX, 0f) && Mathf.Approximately(aimY, 0f))
                {
                    // Mouse aiming: compute vector from player to mouse cursor in world space.
                    // The caller should compute this relative to the player's position.
                    return Vector2.zero;
                }

                return new Vector2(aimX, aimY);
            }
        }

        /// <summary>
        /// Indicates whether the player is pressing the primary fire button.
        /// </summary>
        public bool IsFiring => Input.GetButton("Fire1");

        /// <summary>
        /// Indicates whether the player just pressed the primary fire button.
        /// </summary>
        public bool FirePressed => Input.GetButtonDown("Fire1");

        /// <summary>
        /// Indicates whether the player activated an ability.
        /// </summary>
        public bool AbilityPressed => Input.GetButtonDown("Ability");
    }
}
