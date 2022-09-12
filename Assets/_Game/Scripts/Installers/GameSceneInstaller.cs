using Game.Infrastructure;
using Game.Camera;
using Game.Units;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<PlayerInput>().AsSingle();
            
            Container.BindInterfacesTo<UnitControllerSpawner>().AsSingle();
            Container.Bind<CameraController>().FromComponentInHierarchy().AsSingle();
            Container.BindFactory<UnitData, CharacterController, UnitController, UnitController.Factory>();
            
            Container.BindInterfacesTo<SinglePlayerLevelController>().AsSingle();
        }
    }
}