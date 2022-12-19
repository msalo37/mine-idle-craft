using System.Collections.Generic;
using Crafting.Storage;
using Materials;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
using Random = UnityEngine.Random;

namespace Idle 
{
    public class IdlePanel : MonoBehaviour
    {
        [SerializeField] private MineableMaterial[] materials;
        
        private readonly List<KeyValuePair<MineableMaterial, MiningToolRecipe>> _availableMaterials = new();
        private KeyValuePair<MineableMaterial, MiningToolRecipe> _currentMaterial;
        private bool _isMining;
        private float _timer;
        
        [Inject] private MaterialStorage _storage;

        public KeyValuePair<MineableMaterial, MiningToolRecipe> CurrentMaterial => _currentMaterial;
        public float MiningPercent => _timer / _currentMaterial.Value?.mineDuration ?? 0;
        public bool IsMining => _isMining;
        
        public UnityEvent MaterialUpdated;
        public UnityEvent StartIdle;

        private void Awake()
        {
            OnStorageUpdated(null);
            _currentMaterial = GetRandomAvailableMaterial();
            MaterialUpdated?.Invoke();
        }

        private void Update()
        {
            if (_isMining == false) return;
            if (_currentMaterial.Key == null) return;
            
            _timer += Time.deltaTime;
            if (_timer >= _currentMaterial.Value.mineDuration) 
                EndMining();
        }

        public void StartMine()
        {
            StartIdle?.Invoke();
            _isMining = true;
            _timer = 0;
        }

        private void EndMining()
        {
            _isMining = false;
            _timer = 0;

            _storage.Increase(_currentMaterial.Key, _currentMaterial.Key.mineRecipe.getAmount);
            _currentMaterial = GetRandomAvailableMaterial();
            MaterialUpdated?.Invoke();
        }

        private KeyValuePair<MineableMaterial, MiningToolRecipe> GetRandomAvailableMaterial()
        {
            if (_availableMaterials.Count == 0)
                return new KeyValuePair<MineableMaterial, MiningToolRecipe>(null, null);
            return _availableMaterials[Random.Range(0, _availableMaterials.Count)];
        }

        private void OnEnable()
        {
            _storage.StorageUpdated += OnStorageUpdated;
        }
        
        private void OnDisable()
        {
            _storage.StorageUpdated -= OnStorageUpdated;
        }
        
        // bad method, but it didn't occur to me how to do it differently
        private void OnStorageUpdated(BaseMaterial _)
        {
            _availableMaterials.Clear();
            foreach (var mat in materials)
                if (CanBeMined(mat.mineRecipe, out var tool))
                    _availableMaterials.Add(new KeyValuePair<MineableMaterial, MiningToolRecipe>(mat, tool));

            if (_currentMaterial.Key == null && _availableMaterials.Count > 0)
            {
                _currentMaterial = GetRandomAvailableMaterial();
                MaterialUpdated?.Invoke();
            }
        }

        private bool CanBeMined(MineRecipe recipe, out MiningToolRecipe toolRecipe)
        {
            foreach (var recipeItem in recipe.toolsToMine)
                if (_storage.Have(recipeItem.tool))
                {
                    toolRecipe = recipeItem;
                    return true;
                }

            toolRecipe = default;
            return false;
        }
    }
}

