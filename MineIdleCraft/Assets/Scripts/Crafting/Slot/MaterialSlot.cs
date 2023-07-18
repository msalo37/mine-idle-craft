using System;
using Crafting.Storage;
using Materials;
using TMPro;
using UnityEngine;
using Zenject;
using UnityEngine.UI;

namespace Crafting.Slot
{
    public class MaterialSlot : MonoBehaviour
    {
        [SerializeField] protected BaseMaterial material;
        [Space] [SerializeField] private Image icon;
        [SerializeField] private TMP_Text titleAmount;

        [Inject] protected MaterialStorage _storage;

        protected virtual void OnValidate()
        {
            if (material == null) return;
            if (material.sprite == null) return;
            icon.sprite = material.sprite;
            gameObject.name = $"Material slot ({material.name})";
        }

        protected virtual void Awake()
        {
            if (_storage != null) UpdateTextAmount();
        }

        protected virtual void OnEnable()
        {
            if (_storage == null) throw new NullReferenceException("Item Storage is null!");
            _storage.StorageUpdated += OnStorageUpdated;
        }

        protected virtual void OnDisable()
        {
            _storage.StorageUpdated -= OnStorageUpdated;
        }

        private void OnStorageUpdated(BaseMaterial item)
        {
            if (item != material) return;
            UpdateTextAmount();
        }

        private void UpdateTextAmount()
        {
            titleAmount.text = _storage.GetAmount(material).ToString();
        }
    }
}