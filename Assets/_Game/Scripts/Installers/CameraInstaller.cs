using Game.Camera;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class CameraInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Animator>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<FollowCameraController>().FromComponentsInChildren().AsSingle();
        }
    }
}