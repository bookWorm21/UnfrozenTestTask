using UnityEngine;
using UnityEngine.UI;

namespace UnfrozenTestTask.Source.UI.Windows
{
    public class WinWindow : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private LevelManager _levelManager;

        private void Start()
        {
            _restartButton.onClick.AddListener(() => _levelManager.Restart());
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}