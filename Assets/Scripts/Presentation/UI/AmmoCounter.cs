using UnityEngine;
using UnityEngine.UI;

namespace MyGame.Presentation.UI
{
    /// <summary>
    /// Displays the player's current ammo count on a UI Text element.
    /// </summary>
    public class AmmoCounter : MonoBehaviour
    {
        [SerializeField]
        private Text _text;

        private int _currentAmmo;

        /// <summary>
        /// Sets the ammo count displayed in the UI.
        /// </summary>
        /// <param name="ammo">Current ammo value.</param>
        public void SetAmmo(int ammo)
        {
            _currentAmmo = ammo;
            if (_text != null)
            {
                _text.text = _currentAmmo.ToString();
            }
        }
    }
}
