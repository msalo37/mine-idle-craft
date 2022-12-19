using System;
using System.Collections.Generic;
using Materials;

namespace Crafting.Storage {

    public class MaterialStorage
    {
        private readonly Dictionary<BaseMaterial, long> _inventory;
        public Dictionary<BaseMaterial, long> Data => _inventory;
        public Action<BaseMaterial> StorageUpdated;
        
        public MaterialStorage()
        {
            _inventory = new Dictionary<BaseMaterial, long>();
        }

        public MaterialStorage(Dictionary<BaseMaterial, long> savedData)
        {
            _inventory = savedData;
        }

        public bool Have(BaseMaterial craftingItem, long amount = 1)
        {
            if (craftingItem == null) return true;
            if (_inventory.ContainsKey(craftingItem) == false) return false;
            return _inventory[craftingItem] >= amount;
        }

        public long GetAmount(BaseMaterial craftingItem)
        {
            if (craftingItem == null) return 0;
            if (_inventory.ContainsKey(craftingItem)) return _inventory[craftingItem];
            return 0;
        }

        public void Increase(BaseMaterial craftingItem, long amount = 1)
        {
            if (craftingItem == null) return;
            if (_inventory.ContainsKey(craftingItem))
                _inventory[craftingItem] += amount;
            else
                _inventory[craftingItem] = amount;
            
            StorageUpdated.Invoke(craftingItem);
        }

        public bool TryToDecrease(BaseMaterial craftingItem, long amount = 1)
        {
            if (craftingItem == null) return false;
            if (Have(craftingItem, amount) == true)
            {
                _inventory[craftingItem] -= amount;
                StorageUpdated.Invoke(craftingItem);
                return true;
            }
            else
                return false;
        }
    }
}