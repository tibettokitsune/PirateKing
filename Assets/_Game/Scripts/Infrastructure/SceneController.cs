using Game.Infrastructure;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;


namespace Game.Infrastructure
{
    public class SceneController : ISceneController
    {
        private string _currentScene;
        
        public void OpenTutorialScene()
        {
            if (!string.IsNullOrEmpty(_currentScene))
            {
                
            }
        }

        private void LoadScene()
        {
            
        }
    }
}