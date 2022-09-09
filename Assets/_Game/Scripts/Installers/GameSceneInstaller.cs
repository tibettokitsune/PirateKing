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
            Container.BindInterfacesTo<UnitControllerSpawner>().AsSingle();
            Container.BindInterfacesTo<CameraController>().AsSingle();
            Container.BindFactory<UnitData, CharacterController, UnitController, UnitController.Factory>();
            
            Container.BindInterfacesTo<SinglePlayerLevelController>().AsSingle();
        }
    }
}