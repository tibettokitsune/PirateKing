using System;
using UnityEngine;
using Game.Infrastructure;
using UniRx;

namespace Game.Units
{
    public class EnemyBotController : IDisposable
    {
        private UnitController _unitController;
        public CompositeDisposable Disposable { get; } = new CompositeDisposable();
        public EnemyBotController(UnitController unitController)
        {
            _unitController = unitController;
        }

        public void Update()
        {
            _unitController.UpdateMovementData(Vector3.zero, 
                0f, 0f, 0f, 0f);
        }

        public void Dispose()
        {
        }
    }
}