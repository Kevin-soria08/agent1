using System.Collections.Generic;
using UnityEngine;
using MyGame.Core;
using MyGame.Domain.UseCases;

namespace MyGame.Presentation.FX
{
    /// <summary>
    /// Manages particle effects and animations in response to events.
    /// </summary>
    public class FXManager : MonoBehaviour
    {
        [SerializeField]
        private List<FXEntry> _effects = new List<FXEntry>();

        private readonly Dictionary<string, ParticleSystem> _fxLookup = new Dictionary<string, ParticleSystem>();

        private void Awake()
        {
            foreach (var entry in _effects)
            {
                if (!string.IsNullOrEmpty(entry.Key) && entry.Particle != null)
                {
                    _fxLookup[entry.Key] = entry.Particle;
                }
            }

            // Example subscription: play an effect when damage is dealt
            GameBootstrap.EventBus.Subscribe<DamageDealtEvent>(OnDamageDealt);
        }

        private void OnDestroy()
        {
            GameBootstrap.EventBus?.Unsubscribe<DamageDealtEvent>(OnDamageDealt);
        }

        /// <summary>
        /// Plays a particle effect at the specified position.
        /// </summary>
        /// <param name="key">Key referencing the effect.</param>
        /// <param name="position">Position to play the effect.</param>
        public void PlayFX(string key, Vector3 position)
        {
            if (_fxLookup.TryGetValue(key, out var particle))
            {
                particle.transform.position = position;
                particle.Play();
            }
            else
            {
                Debug.LogWarning($"FXManager: FX key '{key}' not found.");
            }
        }

        private void OnDamageDealt(DamageDealtEvent evt)
        {
            // You can extend the DamageDealtEvent to include position data and play effects accordingly
        }

        [System.Serializable]
        private struct FXEntry
        {
            public string Key;
            public ParticleSystem Particle;
        }
    }
}
