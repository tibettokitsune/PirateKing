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
        [Inject] private FollowCameraController _followCameraController;
        [Inject] private Animator _animator;


        public void SetupPlayerCamera(Transform player, Transform enemy)
        {
            _followCameraController.SetupFollowAndLookTarget(player, enemy);
        }
    }
}