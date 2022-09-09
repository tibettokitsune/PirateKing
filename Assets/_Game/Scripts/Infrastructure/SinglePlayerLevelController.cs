using Game.Camera;
using Game.Units;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure
{
    public class SinglePlayerLevelController : ILevelController
    {
        // [Inject] private CameraController _cameraController;
        [Inject] private IUnitControllerSpawner _unitControllerSpawner;
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
            Debug.Log("Spawn enemy");
        }

        private void SpawnPlayer()
        {
            var unitData = new UnitData();
            var unit = _unitControllerSpawner.SpawnUnit(unitData);
        }
    }

    public interface ILevelController : IInitializable
    {

        void SpawnUnits();
    }
}