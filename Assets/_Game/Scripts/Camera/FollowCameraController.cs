using System;
using Cinemachine;
using UnityEngine;

namespace Game.Camera
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class FollowCameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private float radius;
        [SerializeField] private float verticalOffset;
        private CinemachineTransposer _transposer;
        private void OnValidate() 
            => _camera ??= GetComponent<CinemachineVirtualCamera>();

        private void Start()
        {
            _transposer = _camera.GetCinemachineComponent<CinemachineTransposer>();
        }

        private void LateUpdate()
        {
            if(_camera.Follow)
                _transposer.m_FollowOffset = FocusDirection() * radius + Vector3.up * verticalOffset;
        }

        private Vector3 FocusDirection()
        {
            var res = _camera.m_Follow.position - _camera.m_LookAt.position;
            res.y = 0;
            return res.normalized;
        }

        public void SetupFollowAndLookTarget(Transform follow, Transform lookAt)
        {
            _camera.Follow = follow;
            _camera.LookAt = lookAt;
        }
    }
}