using Zenject;

namespace Crafting.Storage.Installer
{
    public class ItemStorageInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ItemStorage>().AsSingle().NonLazy();
        }
    }
}