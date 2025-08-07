using System;
using UnityEngine;
using MyGame.Core.Services;

namespace MyGame.Core
{
    /// <summary>
    /// Entry point for the game. Initializes core services and persists across scenes.
    /// </summary>
    public class GameBootstrap : MonoBehaviour
    {
        /// <summary>
        /// Singleton instance of the event bus for publishing and subscribing to game events.
        /// </summary>
        public static EventBus EventBus { get; private set; }

        /// <summary>
        /// Provides audio playback functionality for sound effects and music.
        /// </summary>
        public static AudioService AudioService { get; private set; }

        /// <summary>
        /// Provides input reading for player controls.
        /// </summary>
        public static InputService InputService { get; private set; }

        private void Awake()
        {
            // Ensure only one instance exists and persists across scene loads
            if (EventBus != null)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);

            // Initialize core services
            EventBus = new EventBus();
            AudioService = new AudioService();
            InputService = new InputService();

            // Register services with the event bus if required
            AudioService.Initialize(EventBus);
            InputService.Initialize(EventBus);
        }

        private void Start()
        {
            // Example: publish an event indicating that the game has started
            EventBus.Publish(new GameStartedEvent());
        }
    }

    /// <summary>
    /// Event raised when the game starts.
    /// </summary>
    public class GameStartedEvent
    {
    }
}
