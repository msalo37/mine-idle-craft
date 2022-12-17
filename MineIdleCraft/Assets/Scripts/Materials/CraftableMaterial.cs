using System;
using UnityEngine;

namespace Materials
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Materials/Craftable Material", fileName = "New crafting material")]
    public class CraftableMaterial : BaseMaterial
    {
        public CraftingRecipe craftingRecipe;
    }
    
    [Serializable]
    public class CraftingRecipe
    {
        public RecipeItem[] resources;
        public long getAmount;
    }
    
    [Serializable]
    public class RecipeItem
    {
        public BaseMaterial craftingItem;
        public long amount;
        public bool consume = true;
    }
}