using System;
using Game.Infrastructure;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI
{
    public class MenuScreen : UIScreen
    {
        [Inject] private ISceneController _sceneController;
        [BoxGroup("MenuButtons")]
        [SerializeField] private Button trainingButton;
        private void Start()
        {
            OpenScreen();
            
            trainingButton.onClick.AddListener(OnTrainingClick);
        }

        private void OnTrainingClick()
        {
            _sceneController.OpenTutorialScene();
        }
    }
}