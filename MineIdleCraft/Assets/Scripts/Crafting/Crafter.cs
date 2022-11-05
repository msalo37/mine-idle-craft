using Crafting.Storage;
using Zenject;

namespace Crafting
{
    public class Crafter
    {
        [Inject] private ItemStorage _storage;

        public bool TryToCraft(CraftingItem craftingItem)
        {
            if (CanBeCrafted(craftingItem) == false) return false;
            foreach (var recipeItem in craftingItem.recipe.resources)
            {
                if (recipeItem.consume) 
                    _storage.TryToDecrease(recipeItem.craftingItem, recipeItem.amount);
            }
            _storage.Increase(craftingItem, craftingItem.recipe.getAmount);
            return true;
        }

        public bool CanBeCrafted(CraftingItem craftingItem)
        {
            foreach (var recipeItem in craftingItem.recipe.resources)
            {
                if (_storage.Have(recipeItem.craftingItem, recipeItem.amount) == false)
                    return false;
            }
            return true;
        }
    }
}