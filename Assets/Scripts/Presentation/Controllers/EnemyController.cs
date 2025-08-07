using UnityEngine;
using MyGame.Domain.Entities;
using MyGame.Domain.UseCases;
using MyGame.Domain.Interfaces;
using MyGame.Core;
namespace MyGame.Presentation.Controllers
{
    /// <summary>
    /// Controls enemy AI behaviour, including movement and attacking.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyController : MonoBehaviour
    {
        private Enemy _enemyData;
        private Rigidbody2D _rigidbody2D;
        private Transform _target;
        private float _timeSinceLastAttack;
        private CombatSystem _combatSystem;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _enemyData = new Enemy(
                health: 50f,
                speed: 2f,
                attackRate: 1f,
                damage: 5f);

            _combatSystem = new CombatSystem(GameBootstrap.EventBus);
        }

        private void Start()
        {
            // Find the player target by tag
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
            {
                _target = playerObj.transform;
            }
        }

        [System.Obsolete]
        private void FixedUpdate()
        {
            HandleMovement();
            HandleAttack();
        }

        /// <summary>
        /// Moves the enemy towards the player.
        /// </summary>
        [System.Obsolete]
        private void HandleMovement()
        {
            if (_target == null) return;
            Vector2 direction = (_target.position - transform.position).normalized;
            _rigidbody2D.velocity = direction * _enemyData.Speed;
        }

        /// <summary>
        /// Handles the enemy's attacking logic.
        /// </summary>
        private void HandleAttack()
        {
            if (_target == null) return;
            _timeSinceLastAttack += Time.fixedDeltaTime;
            float interval = 1f / _enemyData.AttackRate;
            if (_timeSinceLastAttack >= interval)
            {
                var damageable = _target.GetComponent<MonoBehaviour>() as IDamageable;
                if (damageable != null)
                {
                    _combatSystem.ProcessAttack(_enemyData, damageable, _enemyData.Damage);
                }
                _timeSinceLastAttack = 0f;
            }
        }
    }
}
