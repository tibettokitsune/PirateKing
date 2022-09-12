using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class UnitViewInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Animator>().FromComponentOn(gameObject).AsSingle();
        }
    }
}