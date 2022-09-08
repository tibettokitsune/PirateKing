using Game.Camera;
using Zenject;

namespace Game.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CameraController>().FromComponentInHierarchy().AsSingle();
            
        }
    }
}