using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnfrozenTestTask.Source
{
    public class LevelManager : MonoBehaviour
    {
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}