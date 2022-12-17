using Crafting.Storage;
using Materials;
using Zenject;

namespace Crafting
{
    public class Crafter
    {
        [Inject] private MaterialStorage _storage;

        public bool TryToCraft(CraftableMaterial craftableMaterial)
        {
            if (CanBeCrafted(craftableMaterial) == false) return false;
            foreach (var recipeItem in craftableMaterial.craftingRecipe.resources)
            {
                if (recipeItem.consume) 
                    _storage.TryToDecrease(recipeItem.craftingItem, recipeItem.amount);
            }
            _storage.Increase(craftableMaterial, craftableMaterial.craftingRecipe.getAmount);
            return true;
        }

        public bool CanBeCrafted(CraftableMaterial craftableMaterial)
        {
            foreach (var recipeItem in craftableMaterial.craftingRecipe.resources)
            {
                if (_storage.Have(recipeItem.craftingItem, recipeItem.amount) == false)
                    return false;
            }
            return true;
        }
    }
}