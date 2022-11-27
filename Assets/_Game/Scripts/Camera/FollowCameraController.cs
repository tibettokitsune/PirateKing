using System;
using Cinemachine;
using Game.Infrastructure;
using UnityEngine;
using Zenject;

namespace Game.Camera
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class FollowCameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private float radius;
        [SerializeField] private Vector2 verticalOffset;
        [SerializeField] private Vector2 verticalDistancesLimits;
        
        [SerializeField] private Vector2 horizontalOffset;
        [SerializeField] private Vector2 horizontalDistancesLimits;

        private CinemachineTransposer _transposer;
        private void OnValidate() 
            => _camera ??= GetComponent<CinemachineVirtualCamera>();

        private void Start()
        {
            _transposer = _camera.GetCinemachineComponent<CinemachineTransposer>();
        }

        private void LateUpdate()
        {
            if (_camera.Follow)
            {
                _transposer.m_FollowOffset = FocusDirection() * radius 
                                                + Vector3.up * VerticalOffset() 
                                                +  FocusRightDirection() * HorizontalOffset();
            }
        }
        
        private float Distance()
        {
            var distance = (_camera.LookAt.position - _camera.Follow.position).magnitude;
            return distance;
        }
        
        private float HorizontalOffset()
        {
            var absDistance = (Distance() - horizontalDistancesLimits.x ) 
                              / (horizontalDistancesLimits.y - horizontalDistancesLimits.x);
            return Mathf.Lerp(horizontalOffset.x, horizontalOffset.y, absDistance);
        }
        
        private float VerticalOffset()
        {
            var absDistance = (Distance() - verticalDistancesLimits.x ) 
                              / (horizontalDistancesLimits.y - horizontalDistancesLimits.x);
            if (verticalDistancesLimits.x > verticalDistancesLimits.y)
            {
                absDistance = 1f - absDistance;
                return Mathf.Lerp(verticalOffset.y, verticalOffset.x, absDistance);
            }
            return Mathf.Lerp(verticalOffset.x, verticalOffset.y, absDistance);
        }

        private Vector3 FocusDirection()
        {
            var res = _camera.m_Follow.position - _camera.m_LookAt.position;
            res.y = 0;
            return res.normalized;
        }
        
        private Vector3 FocusRightDirection()
        {
            var res = _camera.m_Follow.position - _camera.m_LookAt.position;
            res.y = 0;
            return Quaternion.Euler(0, 90, 0) * res.normalized;
        }

        public void SetupFollowAndLookTarget(Transform follow, Transform lookAt)
        {
            _camera.Follow = follow;
            _camera.LookAt = lookAt;
        }
    }
}