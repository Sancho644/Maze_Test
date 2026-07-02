using UnityEngine.SceneManagement;

namespace Core
{
    public class SceneLoader
    {
        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}