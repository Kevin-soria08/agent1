using System;
using System.Collections.Generic;
using UnityEngine;
using MyGame.Core;

namespace MyGame.Core.Services
{
    /// <summary>
    /// Provides functionality to play sound effects and music in the game.
    /// </summary>
    public class AudioService
    {
        private AudioSource _musicSource;
        private AudioSource _sfxSource;
        private readonly Dictionary<string, AudioClip> _clips = new Dictionary<string, AudioClip>();

        private EventBus _eventBus;

        /// <summary>
        /// Initializes the audio service with an event bus for event-driven sound playback.
        /// </summary>
        /// <param name="eventBus">The global event bus.</param>
        public void Initialize(EventBus eventBus)
        {
            _eventBus = eventBus;
            // Create audio sources on a dedicated game object
            var audioObject = new GameObject("AudioService");
            UnityEngine.Object.DontDestroyOnLoad(audioObject);
            _musicSource = audioObject.AddComponent<AudioSource>();
            _musicSource.loop = true;
            _sfxSource = audioObject.AddComponent<AudioSource>();

            // Optionally subscribe to events to play audio automatically
            // Example: eventBus.Subscribe<EnemyDeathEvent>(OnEnemyDeath);
        }

        /// <summary>
        /// Loads an audio clip into the internal dictionary.
        /// </summary>
        /// <param name="key">Key to reference the clip.</param>
        /// <param name="clip">Audio clip to store.</param>
        public void RegisterClip(string key, AudioClip clip)
        {
            if (!_clips.ContainsKey(key))
            {
                _clips[key] = clip;
            }
        }

        /// <summary>
        /// Plays a sound effect using the provided key.
        /// </summary>
        /// <param name="key">Key referencing the audio clip.</param>
        public void PlaySound(string key)
        {
            if (_clips.TryGetValue(key, out var clip))
            {
                _sfxSource.PlayOneShot(clip);
            }
            else
            {
                Debug.LogWarning($"AudioService: Sound key '{key}' not found.");
            }
        }

        /// <summary>
        /// Plays background music using the provided key.
        /// </summary>
        /// <param name="key">Key referencing the music clip.</param>
        public void PlayMusic(string key)
        {
            if (_clips.TryGetValue(key, out var clip))
            {
                _musicSource.clip = clip;
                _musicSource.Play();
            }
            else
            {
                Debug.LogWarning($"AudioService: Music key '{key}' not found.");
            }
        }

        // Example event handler; you can add event-driven sound playback here
        // private void OnEnemyDeath(EnemyDeathEvent evt)
        // {
        //     PlaySound("enemy_death");
        // }
    }
}
