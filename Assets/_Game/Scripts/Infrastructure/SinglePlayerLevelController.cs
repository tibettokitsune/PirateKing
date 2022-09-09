using Game.Camera;
using Game.Units;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure
{
    public class SinglePlayerLevelController : ILevelController
    {
        [Inject] private CameraController _cameraController;
        [Inject] private IUnitControllerSpawner _unitControllerSpawner;

        private UnitController _player;
        private UnitController _enemy;
        public void Initialize()
        {
            SpawnUnits();
        }

        public void SpawnUnits()
        {
            SpawnPlayer();
            
            SpawnEnemy();
            
            _cameraController.SetupPlayerCamera(_player.GetTransformTarget(), _enemy.GetTransformTarget());
        }

        private void SpawnEnemy()
        {
            var unitData = new UnitData();
            _enemy = _unitControllerSpawner.SpawnUnit(unitData);
        }

        private void SpawnPlayer()
        {
            var unitData = new UnitData();
            _player = _unitControllerSpawner.SpawnUnit(unitData);
        }
    }

    public interface ILevelController : IInitializable
    {

        void SpawnUnits();
    }
}