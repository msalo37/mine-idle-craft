using System;
using Crafting.Storage;
using Materials;
using TipPanel;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Zenject;
using UnityEngine.UI;

namespace Crafting.Slot
{
    public class CraftingItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private CraftableMaterial craftableMaterial;
        [Space] [SerializeField] private Image icon;
        [SerializeField] private TMP_Text titleAmount;

        [Inject] private MaterialStorage _storage;
        [Inject] private Crafter _crafter;
        [Inject] private TipMessagePanelController _tipMessagePanel;

        public UnityEvent OnSlotClicked;

        private void OnValidate()
        {
            if (craftableMaterial == null) return;
            if (craftableMaterial.sprite == null) return;
            icon.sprite = craftableMaterial.sprite;
        }

        private void Awake()
        {
            if (_storage != null) UpdateTextAmount();
        }

        private void OnEnable()
        {
            if (_storage == null) throw new NullReferenceException("Item Storage is null!");
            _storage.StorageUpdated += OnStorageUpdated;
        }

        private void OnDisable()
        {
            _storage.StorageUpdated -= OnStorageUpdated;
        }

        private void OnStorageUpdated(BaseMaterial item)
        {
            if (item != craftableMaterial) return;
            UpdateTextAmount();
        }

        private void UpdateTextAmount()
        {
            titleAmount.text = _storage.GetAmount(craftableMaterial).ToString();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_crafter == null) throw new NullReferenceException("Crafter is null!");
            OnSlotClicked?.Invoke();
            _crafter.TryToCraft(craftableMaterial);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _tipMessagePanel.ShowMessage(craftableMaterial);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _tipMessagePanel.HideMessage();
        }
    }
}