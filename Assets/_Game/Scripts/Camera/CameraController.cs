using Zenject;

namespace Game.Camera
{
    public interface ICameraController
    {
        
    }
    public class CameraController : ICameraController
    { 
        [Inject] private UnityEngine.Camera _camera;
        
    }
}