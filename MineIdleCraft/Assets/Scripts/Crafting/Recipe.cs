using System;

namespace Crafting
{
    [Serializable]
    public class Recipe
    {
        public RecipeItem[] resources;
        public long getAmount;
    }
    
    [Serializable]
    public class RecipeItem
    {
        public CraftingItem craftingItem;
        public long amount;
        public bool consume = true;
    }
    
}