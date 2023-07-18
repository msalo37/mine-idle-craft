using Materials;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TipPanel
{
    public class TipCraftingSlotUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text titleAmount;
        [SerializeField] private Image icon;

        public void SetRecipeItem(RecipeItem recipeItem)
        {
            icon.sprite = recipeItem.material.sprite;
            titleAmount.text = recipeItem.amount.ToString();
        }
    }
}