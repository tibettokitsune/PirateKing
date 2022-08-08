using System.Collections.Generic;
using Game.Configs;
using Game.Infrastructure;
using Game.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;


namespace Game.Infrastructure
{
    public class SceneController : ISceneController
    {
        [Inject] private SceneConfig _sceneConfig;
        [Inject] private LoadingScreen _loadingScreen;
        private List<string> _currentScene = new List<string>();
        
        public void OpenTutorialScene()
        {
            if (_currentScene.Count > 0)
            {
                foreach (var s in _currentScene)
                {
                    SceneManager.UnloadSceneAsync(s);
                }
            }

            var envSceneName = RandomEnvironmentScene();
            var l = SceneManager.LoadSceneAsync(envSceneName);
            _loadingScreen.Loading(l, () =>
            {
                _currentScene.Add(envSceneName);
                var g = SceneManager.LoadSceneAsync(_sceneConfig.singlePlayerScene);
                _loadingScreen.Loading(g, () =>
                {
                    _currentScene.Add(_sceneConfig.singlePlayerScene);
                });
            });
        }

        private string RandomEnvironmentScene() =>
            _sceneConfig.environmentScenes[Random.Range(0, _sceneConfig.environmentScenes.Length)];

        private void LoadScene()
        {
            
        }
    }
}