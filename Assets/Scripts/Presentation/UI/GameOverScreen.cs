using UnityEngine;

namespace MyGame.Presentation.UI
{
    /// <summary>
    /// Controls the visibility of the game over screen.
    /// </summary>
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField]
        private GameObject _panel;

        private void Awake()
        {
            if (_panel != null)
            {
                _panel.SetActive(false);
            }
        }

        /// <summary>
        /// Shows the game over screen.
        /// </summary>
        public void Show()
        {
            if (_panel != null)
            {
                _panel.SetActive(true);
            }
        }

        /// <summary>
        /// Hides the game over screen.
        /// </summary>
        public void Hide()
        {
            if (_panel != null)
            {
                _panel.SetActive(false);
            }
        }
    }
}
