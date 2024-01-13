using Assets._PC.Scripts.Core.Components;
using Assets._PC.Scripts.Core.Managers;
using UnityEngine.SceneManagement;

namespace Assets._PC.Scripts.Core.Loaders
{
    public class PCGameLoader : PCMonoBehaviour
    {
        private const string MAIN_GAME_SCENE = "MainGameScene";

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void Start()
        {
            var mainManager = new PCManager();
            SceneManager.LoadScene(MAIN_GAME_SCENE);
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == MAIN_GAME_SCENE)
            {
                Destroy(gameObject);
            }
        }
    }
}