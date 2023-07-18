using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Materials
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Materials/Craftable Material", fileName = "New crafting material")]
    public class CraftableMaterial : BaseMaterial
    {
        public CraftingRecipe craftingRecipe;

        private void OnValidate()
        {
            if (craftingRecipe.getAmount == 0) 
                Debug.LogWarning($"{name}'s crafting recipe produce 0 materials");
            
            /*foreach (RecipeItem recipeItem in craftingRecipe.resources)
                if (recipeItem.amount == 0)
                    Debug.LogWarning($"{name} need 0 {recipeItem.material.name} to craft");*/
        }
    }
    
    [Serializable]
    public class CraftingRecipe
    {
        public RecipeItem[] resources;
        public long getAmount = 1;
    }
    
    [Serializable]
    public class RecipeItem
    {
        [FormerlySerializedAs("craftingItem")] public BaseMaterial material;
        public long amount = 1;
        public bool consume = true;
    }
}