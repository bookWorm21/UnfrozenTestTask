using UnityEngine;

namespace UnfrozenTestTask.Source.UI.Windows
{
    public class SelectEnemyTargetWindow : MonoBehaviour
    {
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