using UnityEngine;
using Zenject;

namespace TipPanel.Installer
{
    public class TipMessagePanelInstaller : MonoInstaller
    {
        [SerializeField] private TipMessagePanelController controller;

        public override void InstallBindings()
        {
            Container.Bind<TipMessagePanelController>().FromInstance(controller).AsSingle().NonLazy();
        }
    }
}

