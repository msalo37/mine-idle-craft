using Crafting;
using TMPro;
using UnityEngine;

namespace TipBox
{
    public class TipMessageBox : MonoBehaviour
    {
        [SerializeField] private TMP_Text title;
        [SerializeField] private TipCraftingItemSlot slotPrefab;
        [SerializeField] private Transform slotParent;

        public void SetCraftingItem(CraftingItem craftingItem)
        {
            ClearSlotParent();
            title.text = craftingItem.name + $"({craftingItem.recipe.getAmount} pcs)";
            foreach (var recipeItem in craftingItem.recipe.resources)
            {
                TipCraftingItemSlot craftingSlot = Instantiate(slotPrefab, slotParent);
                craftingSlot.SetRecipeItem(recipeItem);
            }
        }

        private void ClearSlotParent()
        {
            if (slotParent.childCount == 0) return;
            foreach (Transform child in slotParent)
                Destroy(child.gameObject);
        }
    }
}

