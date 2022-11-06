using UnityEngine;
using Zenject;

namespace TipBox.Installer
{
    public class TipMessageBoxInstaller : MonoInstaller
    {
        [SerializeField] private TipMessageBoxController controller;

        public override void InstallBindings()
        {
            Container.Bind<TipMessageBoxController>().FromInstance(controller).AsSingle().NonLazy();
        }
    }
}

