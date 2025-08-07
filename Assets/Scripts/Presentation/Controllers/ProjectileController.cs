using UnityEngine;
using MyGame.Domain.Interfaces;

namespace MyGame.Presentation.Controllers
{
    /// <summary>
    /// Controls the behaviour of a projectile in the scene.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class ProjectileController : MonoBehaviour
    {
        private float _speed;
        private float _damage;
        private Vector3 _spawnPosition;

        /// <summary>
        /// Initializes the projectile with speed and damage values.
        /// </summary>
        /// <param name="speed">Speed at which the projectile travels.</param>
        /// <param name="damage">Damage inflicted on hit.</param>
        public void Initialize(float speed, float damage)
        {
            _speed = speed;
            _damage = damage;
            _spawnPosition = transform.position;
        }

        private void Update()
        {
            // Destroy projectile after travelling beyond a certain range
            if (Vector3.Distance(_spawnPosition, transform.position) > 20f)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Apply damage to any IDamageable hit target
            var damageable = other.GetComponent<MonoBehaviour>() as IDamageable;
            if (damageable != null)
            {
                damageable.TakeDamage(_damage);
                Destroy(gameObject);
            }
        }
    }
}
