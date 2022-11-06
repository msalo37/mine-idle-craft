using Crafting;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TipBox
{
    public class TipCraftingItemSlot : MonoBehaviour
    {
        [SerializeField] private TMP_Text titleAmount;
        [SerializeField] private Image icon;

        public void SetRecipeItem(RecipeItem recipeItem)
        {
            icon.sprite = recipeItem.craftingItem.icon;
            titleAmount.text = recipeItem.amount.ToString();
        }
    }
}