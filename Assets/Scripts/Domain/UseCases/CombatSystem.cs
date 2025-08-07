using MyGame.Core;
using MyGame.Domain.Interfaces;

namespace MyGame.Domain.UseCases
{
    /// <summary>
    /// Handles combat interactions such as applying damage when an attacker hits a target.
    /// </summary>
    public class CombatSystem
    {
        private readonly EventBus _eventBus;

        /// <summary>
        /// Initializes a new instance of the <see cref="CombatSystem"/> class.
        /// </summary>
        /// <param name="eventBus">Global event bus to publish combat events.</param>
        public CombatSystem(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        /// <summary>
        /// Processes an attack from an attacker onto a target.
        /// </summary>
        /// <param name="attacker">Shooter performing the attack.</param>
        /// <param name="target">Target receiving damage.</param>
        /// <param name="damage">Amount of damage inflicted.</param>
        public void ProcessAttack(IShootable attacker, IDamageable target, float damage)
        {
            target.TakeDamage(damage);
            // Optionally publish an event when damage is dealt
            _eventBus.Publish(new DamageDealtEvent(target, damage));
        }
    }

    /// <summary>
    /// Event describing damage dealt to a target.
    /// </summary>
    public class DamageDealtEvent
    {
        /// <summary>
        /// The object that received damage.
        /// </summary>
        public IDamageable Target { get; }

        /// <summary>
        /// Amount of damage dealt.
        /// </summary>
        public float Damage { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DamageDealtEvent"/> class.
        /// </summary>
        /// <param name="target">The damaged object.</param>
        /// <param name="damage">Amount of damage.</param>
        public DamageDealtEvent(IDamageable target, float damage)
        {
            Target = target;
            Damage = damage;
        }
    }
}
