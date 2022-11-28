using Game.Camera;
using Game.Units;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure
{
    public class SinglePlayerLevelController : ILevelController, IFixedTickable, ITickable
    {
        private ReactiveCommand OnLevelFixesUpdate { get; } = new ReactiveCommand();
        private ReactiveCommand OnLevelUpdate { get; } = new ReactiveCommand();
        private ReactiveCommand<UnitController> OnEnemyCreated { get; } = new ReactiveCommand<UnitController>();
        [Inject] private CameraController _cameraController;
        [Inject] private IUnitControllerSpawner _unitControllerSpawner;
        [Inject] private IInput _input;
        private UnitController _player;
        private UnitController _enemy;

        private DiContainer _container;
        public SinglePlayerLevelController(DiContainer container)
        {
            _container = container;
        }
        public void Initialize()
        {
            SpawnUnits();
        }

        public void SpawnUnits()
        {
            SpawnPlayer();
            
            SpawnEnemy();
            
            
        }

        private void SpawnEnemy()
        {
            var unitData = new UnitData();
            _enemy = _unitControllerSpawner.SpawnUnit(unitData);
            var enemyAIController = new EnemyBotController(_enemy);
            _enemy.UpdateFightTarget(_player);
            OnLevelFixesUpdate.Subscribe(_ => enemyAIController.Update()).AddTo(enemyAIController.Disposable);
            OnLevelFixesUpdate.Subscribe(_ => _enemy.FixedTick()).AddTo(enemyAIController.Disposable);
            OnEnemyCreated.Execute(_enemy);
        }

        private void SpawnPlayer()
        {
            var unitData = new UnitData();
            _player = _unitControllerSpawner.SpawnUnit(unitData);
            
            var playerLogicController = new PlayerController(_player, _input);
            OnEnemyCreated.Take(1).Subscribe(enemy =>
            {
                _player.UpdateFightTarget(enemy);
                _cameraController.SetupPlayerCamera(_player.GetTransformTarget(), _enemy.GetTransformTarget());
            }).AddTo(playerLogicController.Disposable);
            OnLevelFixesUpdate.Subscribe(_ => playerLogicController.Update()).AddTo(playerLogicController.Disposable);
            OnLevelFixesUpdate.Subscribe(_ => _player.FixedTick()).AddTo(playerLogicController.Disposable);
            OnLevelUpdate.Subscribe(_ => _player.Tick()).AddTo(playerLogicController.Disposable);
        }
        
        public void FixedTick()=> OnLevelFixesUpdate?.Execute();
        public void Tick() => OnLevelUpdate?.Execute();
    }

    public interface ILevelController : IInitializable
    {
        void SpawnUnits();
    }
}