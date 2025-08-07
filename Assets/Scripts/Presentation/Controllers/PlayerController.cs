using UnityEngine;
using MyGame.Core;
using MyGame.Core.Services;
using MyGame.Domain.Entities;
 using MyGame.Domain.UseCases;

namespace MyGame.Presentation.Controllers
{
    /// <summary>
    /// Handles player control, movement, and shooting.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private MyGame.Infrastructure.DataAdapters.WeaponData _weaponData;

        private Rigidbody2D _rigidbody2D;
        private Player _playerData;
        private float _timeSinceLastShot;
        private CombatSystem _combatSystem;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();

            // Initialize player data from stats (could be loaded via data adapters)
            _playerData = new Player(
                health: 100f,
                speed: 5f,
                attackRate: 3f,
                damage: 10f);

            _combatSystem = new CombatSystem(GameBootstrap.EventBus);
        }

        private void Update()
        {
            HandleMovement();
            HandleShooting();
        }

        /// <summary>
        /// Moves the player based on input service values.
        /// </summary>
        private void HandleMovement()
        {
            Vector2 movement = GameBootstrap.InputService.MovementVector;
            Vector2 velocity = movement.normalized * _playerData.Speed;
            _rigidbody2D.linearVelocity = velocity;

            // Rotate to face aiming direction if available
            Vector2 aim = GameBootstrap.InputService.AimVector;
            if (aim.sqrMagnitude > 0.01f)
            {
                float angle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
                _rigidbody2D.rotation = angle - 90f;
            }
        }

        /// <summary>
        /// Handles shooting logic, spawning projectiles at a rate limited by attack rate.
        /// </summary>
        private void HandleShooting()
        {
            if (!GameBootstrap.InputService.IsFiring) return;

            _timeSinceLastShot += Time.deltaTime;
            float shotInterval = 1f / _playerData.AttackRate;
            if (_timeSinceLastShot >= shotInterval)
            {
                ShootProjectile();
                _timeSinceLastShot = 0f;
            }
        }

        /// <summary>
        /// Instantiates or retrieves a projectile from the pool and launches it.
        /// </summary>
        private void ShootProjectile()
        {
            if (_weaponData == null)
            {
                Debug.LogWarning("PlayerController: Weapon data is not assigned.");
                return;
            }
            // Acquire direction from aim or forward vector
            Vector2 aim = GameBootstrap.InputService.AimVector;
            if (aim.sqrMagnitude < 0.01f)
            {
                aim = Vector2.up;
            }
            aim.Normalize();

            // Instantiate projectile (in a real project we would use ObjectPooler)
            GameObject projectile = new GameObject("Projectile");
            projectile.transform.position = transform.position;
            Rigidbody2D rb = projectile.AddComponent<Rigidbody2D>();
            ProjectileController projCtrl = projectile.AddComponent<ProjectileController>();
            projCtrl.Initialize(_weaponData.ProjectileSpeed, _playerData.Damage);
            rb.linearVelocity = aim * _weaponData.ProjectileSpeed;
        }

        /// <summary>
        /// Resets the player's stats to default values.
        /// </summary>
        public void ResetStats()
        {
            _playerData = new Player(
                health: 100f,
                speed: 5f,
                attackRate: 3f,
                damage: 10f);
        }
    }
}
