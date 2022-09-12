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
            _unitController.Movement(_input.Horizontal(), _input.Vertical(), _input.IsJump(), _input.IsEvade());
        }

        public void Dispose()
        {
        }
    }
}