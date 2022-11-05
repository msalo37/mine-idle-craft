using Zenject;

namespace Crafting.Installer
{
    public class CrafterInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Crafter>().AsSingle().NonLazy();
        }
    }
}