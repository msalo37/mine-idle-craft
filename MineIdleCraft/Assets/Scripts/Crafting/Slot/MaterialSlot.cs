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
        [SerializeField] private BaseMaterial material;
        [Space] [SerializeField] private Image icon;
        [SerializeField] private TMP_Text titleAmount;

        [Inject] private MaterialStorage _storage;

        private void OnValidate()
        {
            if (material == null) return;
            if (material.sprite == null) return;
            icon.sprite = material.sprite;
            gameObject.name = $"Material slot ({material.name})";
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
            if (item != material) return;
            UpdateTextAmount();
        }

        private void UpdateTextAmount()
        {
            titleAmount.text = _storage.GetAmount(material).ToString();
        }
    }
}