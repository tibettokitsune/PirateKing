using System.Collections.Generic;
using System.Linq;
using Game.Configs;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Units
{
    public interface IUnitControllerSpawner
    {
        UnitController SpawnUnit(UnitData data);
    }
    public class UnitControllerSpawner : IUnitControllerSpawner, IInitializable
    {
        [Inject] private UnitsConfig _config;
        private readonly UnitController.Factory _factory;
        private List<GameObject> _spawnPoints = new List<GameObject>();
        public UnitControllerSpawner(UnitController.Factory factory)
        {
            _factory = factory;
        }

        public UnitController SpawnUnit(UnitData data)
        {
            var unit = _factory.Create(data, _config.controllerPrefab);
            var currentSpawnPoint = _spawnPoints[Random.Range( 0, _spawnPoints.Count - 1)];
            _spawnPoints.Remove(currentSpawnPoint);
            unit.ChangeStartPosition(currentSpawnPoint.transform.position);
            unit.CreateView(_config.unitViews[(int) UnitViewVariant.TestDummy]);
            return unit;
        }

        public void Initialize()
        {
            _spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint").ToList();
        }
    }
}