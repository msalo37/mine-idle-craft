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
        private float _timer;
        
        [Inject] private MaterialStorage _storage;

        public KeyValuePair<MineableMaterial, MiningToolRecipe> CurrentMaterial { private set; get; }
        public float MiningPercent => _timer / CurrentMaterial.Value?.mineDuration ?? 0;
        public bool IsMining { private set; get; }

        public UnityEvent<MineableMaterial> MaterialUpdate;
        public UnityEvent StartIdle;

        private void Awake()
        {
            OnStorageUpdated(null);
            CurrentMaterial = GetRandomAvailableMaterial();
            MaterialUpdate?.Invoke(CurrentMaterial.Key);
        }

        private void Update()
        {
            if (IsMining == false) return;
            if (CurrentMaterial.Key == null) return;
            
            _timer += Time.deltaTime;
            if (_timer >= CurrentMaterial.Value.mineDuration) 
                EndMining();
        }

        public void StartMine()
        {
            if (IsMining == true) return;
            StartIdle?.Invoke();
            IsMining = true;
            _timer = 0;
        }

        private void EndMining()
        {
            IsMining = false;
            _timer = 0;

            _storage.Increase(CurrentMaterial.Key, CurrentMaterial.Key.mineRecipe.getAmount);
            CurrentMaterial = GetRandomAvailableMaterial();
            MaterialUpdate?.Invoke(CurrentMaterial.Key);
        }

        private KeyValuePair<MineableMaterial, MiningToolRecipe> GetRandomAvailableMaterial()
        {
            if (_availableMaterials.Count == 0) return default;
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

            if (CurrentMaterial.Key == null && _availableMaterials.Count > 0)
            {
                CurrentMaterial = GetRandomAvailableMaterial();
                MaterialUpdate?.Invoke(CurrentMaterial.Key);
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

