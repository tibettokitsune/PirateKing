using System;
using System.Collections.Generic;
using _Game.Scripts.Infrastructure;
using Game.Configs;
using Game.Infrastructure;
using Game.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Random = UnityEngine.Random;


namespace Game.Infrastructure
{
    public class SceneController : ISceneController
    {
        [Inject] private SceneConfig _sceneConfig;
        [Inject] private LoadingScreen _loadingScreen;
        [Inject] private GameEvents _gameEvents;
        private List<string> _loadedScenes = new List<string>();
        private AsyncOperation _loadingOperation;
        public void OpenTutorialScene()
        {
            UnloadScenes();
            _loadingScreen.OpenScreen();
            LoadScene(RandomEnvironmentScene(), () =>
            {
                LoadScene(_sceneConfig.singlePlayerScene, () =>
                {
                    _loadingScreen.CloseScreen();
                    _gameEvents.OnTutorialSceneLoaded.Execute();
                });
            });
        }

        private void UnloadScenes()
        {
            if (_loadedScenes.Count > 0)
            {
                foreach (var s in _loadedScenes)
                {
                    SceneManager.UnloadSceneAsync(s);
                }
            }
        }

        private string RandomEnvironmentScene() =>
            _sceneConfig.environmentScenes[Random.Range(0, _sceneConfig.environmentScenes.Length)];

        private void LoadScene(string sceneName, Action onSceneComplete)
        {
            var l = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            l.completed += o =>
            {
                onSceneComplete?.Invoke();
                _loadedScenes.Add(sceneName);
            };
            _loadingScreen.Loading(l);

        }
    }
}