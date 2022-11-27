using System;
using System.Collections.Generic;
using Game.Infrastructure;
using UnityEngine;
using Zenject;

namespace Game.Camera
{
    public interface ICameraController
    {
        void SetupPlayerCamera(Transform player, Transform enemy);
    }
    public class CameraController :MonoBehaviour, ICameraController
    { 
        [Inject] private UnityEngine.Camera _camera;
        [Inject] private List<FollowCameraController> _followCameraControllers;
        [Inject] private Animator _animator;
        [Inject] private IInput _input;
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");

        private void Update()
        {
            _animator.SetFloat(Horizontal, _input.Horizontal());
        }

        public void SetupPlayerCamera(Transform player, Transform enemy)
        {
            foreach (var followCamera in _followCameraControllers)
            {
                followCamera.SetupFollowAndLookTarget(player, enemy);
            }
        }
    }
}