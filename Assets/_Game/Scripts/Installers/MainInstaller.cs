using _Game.Scripts.Infrastructure;
using Game.Infrastructure;
using Game.UI;
using Zenject;

namespace Game.Installers
{
    public class MainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInstance(new GameEvents()).AsSingle();
            Container.BindInterfacesTo<SceneController>().AsSingle();
            BindUI();
        }

        private void BindUI()
        {
            Container.Bind<LoadingScreen>().FromComponentInHierarchy().AsSingle();
            Container.Bind<MenuScreen>().FromComponentInHierarchy().AsSingle();
        }
    }
}