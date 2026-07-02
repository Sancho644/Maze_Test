using Core;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class RestartButton : MonoBehaviour
    {
        [SerializeField] private Button button;

        private SceneLoader _sceneLoader;
        
        private void Awake()
        {
            button.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            button.onClick.AddListener(OnClick);
        }
        
        [Inject]
        public void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void OnClick()
        {
            _sceneLoader.RestartLevel();
        }
    }
}