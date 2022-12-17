using Zenject;

namespace Crafting.Storage.Installer
{
    public class MaterialStorageInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<MaterialStorage>().AsSingle().NonLazy();
        }
    }
}