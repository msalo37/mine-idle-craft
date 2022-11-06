using System;
using Crafting.Storage;
using TipBox;
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
        [SerializeField] private CraftingItem craftingItem;
        [Space] [SerializeField] private Image icon;
        [SerializeField] private TMP_Text titleAmount;

        [Inject] private ItemStorage _storage;
        [Inject] private Crafter _crafter;
        [Inject] private TipMessageBoxController _tipMessageBox;

        public UnityEvent OnSlotClicked;

        private void OnValidate()
        {
            if (craftingItem == null) return;
            if (craftingItem.icon == null) return;
            icon.sprite = craftingItem.icon;
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

        private void OnStorageUpdated(CraftingItem item)
        {
            if (item != craftingItem) return;
            UpdateTextAmount();
        }

        private void UpdateTextAmount()
        {
            titleAmount.text = _storage.GetAmount(craftingItem).ToString();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_crafter == null) throw new NullReferenceException("Crafter is null!");
            OnSlotClicked?.Invoke();
            _crafter.TryToCraft(craftingItem);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _tipMessageBox.ShowMessage(craftingItem);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _tipMessageBox.HideMessage();
        }
    }
}