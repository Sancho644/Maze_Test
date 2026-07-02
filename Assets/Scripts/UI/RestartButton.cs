using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class RestartButton : MonoBehaviour
    {
        [SerializeField] private Button button;

        private void Awake()
        {
            button.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}