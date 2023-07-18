using System;
using Materials;
using TipPanel;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Zenject;

namespace Crafting.Slot
{
    public class CraftingMaterialSlot : MaterialSlot, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Inject] private Crafter _crafter;
        [Inject] private TipMessagePanelController _tipMessagePanel;

        public UnityEvent SlotClicked;

        private CraftableMaterial _craftableMaterial;

        protected override void OnValidate()
        {
            base.OnValidate();
            if (material == null) return;
            if (material is not CraftableMaterial)
                Debug.Log("This material cannot be crafted!");
        }

        protected override void Awake()
        {
            base.Awake();
            _craftableMaterial = (CraftableMaterial)material;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_crafter == null) throw new NullReferenceException("Crafter is null!");
            SlotClicked?.Invoke();
            _crafter.TryToCraft(_craftableMaterial);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _tipMessagePanel.ShowMessage(_craftableMaterial);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _tipMessagePanel.HideMessage();
        }
    }
}