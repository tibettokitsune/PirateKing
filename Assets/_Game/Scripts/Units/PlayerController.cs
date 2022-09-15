using System;
using Game.Infrastructure;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Units
{
    
    public class PlayerController : IDisposable
    {
        private IInput _input;
        private UnitController _unitController;
        public CompositeDisposable Disposable { get; } = new CompositeDisposable();
        public PlayerController(UnitController unitController, IInput input)
        {
            _input = input;
            _unitController = unitController;
        }

        public void Update()
        {
            var movementVector = new Vector2(_input.Horizontal(), _input.Vertical());
            _unitController.UpdateMovementData(movementVector, 
                _input.IsJump(), _input.IsEvade(), _input.IsMovementBoost(), _input.IsCrouch());
        }

        public void Dispose()
        {
        }
    }
}