using Game.Configs;
using Zenject;

namespace _Game.Scripts.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public RootConfig rootConfig;
        public override void InstallBindings()
        {
            BindConfigs();
        }

        private void BindConfigs()
        {
            Container.BindInstance(rootConfig.sceneConfig);
        }
    }
}