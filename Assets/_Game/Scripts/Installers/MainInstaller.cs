using Game.UI;
using Zenject;

namespace Game.Installers
{
    public class MainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindUI();
        }

        private void BindUI()
        {
            Container.Bind<LoadingScreen>().FromComponentInHierarchy().AsSingle();
            Container.Bind<MenuScreen>().FromComponentInHierarchy().AsSingle();
        }
    }
}