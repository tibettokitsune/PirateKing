using _Game.Scripts.Infrastructure;
using Game.Infrastructure;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI
{
    public class MenuScreen : UIScreen
    {
        [Inject] private ISceneController _sceneController;
        [Inject] private GameEvents _gameEvents;
        [BoxGroup("MenuButtons")]
        [SerializeField] private Button trainingButton;
        private void Start()
        {
            OpenScreen();
            
            trainingButton.onClick.AddListener(OnTrainingClick);

            _gameEvents.OnTutorialSceneLoaded.Subscribe(_ =>
            {
                CloseScreen();
            }).AddTo(this);
        }

        private void OnTrainingClick()
        {
            _sceneController.OpenTutorialScene();
        }
    }
}