using Game.Camera;
using Zenject;

namespace _Game.Scripts.Infrastructure
{
    public class SinglePlayerLevelController : ILevelController
    {
        [Inject] private CameraController _cameraController;
        public void Initialize()
        {
            SpawnUnits();
        }

        public void SpawnUnits()
        {
            
        }
    }

    public interface ILevelController : IInitializable
    {

        void SpawnUnits();
    }
}