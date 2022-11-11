using Crafting;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace TipPanel
{
    public class TipMessagePanelUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text title;
        [SerializeField] private TipCraftingSlotUI slotPrefab;
        [SerializeField] private Transform slotParent;

        public void SetCraftingItem(CraftingItem craftingItem)
        {
            ClearSlots();
            title.text = craftingItem.name + $"({craftingItem.recipe.getAmount} pcs)";
            foreach (var recipeItem in craftingItem.recipe.resources)
            {
                TipCraftingSlotUI craftingSlotUI = Instantiate(slotPrefab, slotParent);
                craftingSlotUI.SetRecipeItem(recipeItem);
            }
        }

        private void ClearSlots()
        {
            if (slotParent.childCount == 0) return;
            foreach (Transform child in slotParent)
                Destroy(child.gameObject);
        }
    }
}

