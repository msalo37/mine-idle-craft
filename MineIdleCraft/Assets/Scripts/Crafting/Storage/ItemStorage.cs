using System;
using System.Collections.Generic;

namespace Crafting.Storage {

    public class ItemStorage
    {
        private readonly Dictionary<CraftingItem, long> _inventory;
        public Dictionary<CraftingItem, long> Data => _inventory;
        public Action<CraftingItem> StorageUpdated;
        
        public ItemStorage()
        {
            _inventory = new Dictionary<CraftingItem, long>();
        }

        public ItemStorage(Dictionary<CraftingItem, long> savedData)
        {
            _inventory = savedData;
        }

        public bool Have(CraftingItem craftingItem, long amount = 1)
        {
            if (_inventory.ContainsKey(craftingItem) == false) return false;
            return _inventory[craftingItem] >= amount;
        }

        public long GetAmount(CraftingItem craftingItem)
        {
            if (_inventory.ContainsKey(craftingItem)) return _inventory[craftingItem];
            return 0;
        }

        public void Increase(CraftingItem craftingItem, long amount = 1)
        {
            if (_inventory.ContainsKey(craftingItem))
                _inventory[craftingItem] += amount;
            else
                _inventory[craftingItem] = amount;
            
            StorageUpdated.Invoke(craftingItem);
        }

        public bool TryToDecrease(CraftingItem craftingItem, long amount = 1)
        {
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