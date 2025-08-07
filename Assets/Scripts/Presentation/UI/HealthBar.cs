using UnityEngine;
using UnityEngine.UI;
using MyGame.Domain.Interfaces;

namespace MyGame.Presentation.UI
{
    /// <summary>
    /// Updates a UI slider to reflect the health of an IDamageable target.
    /// </summary>
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private Slider _slider;

        [SerializeField]
        private MonoBehaviour _targetBehaviour;

        private IDamageable _target;

        private void Awake()
        {
            if (_targetBehaviour != null)
            {
                _target = _targetBehaviour as IDamageable;
            }
        }

        private void Update()
        {
            if (_target != null && _slider != null)
            {
                // Assuming max health is constant; in a full implementation this would be dynamic.
                _slider.value = _target.Health;
            }
        }
    }
}
