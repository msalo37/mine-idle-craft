using Materials;
using TMPro;
using UnityEngine;

namespace TipPanel
{
    public class TipMessagePanelUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text title;
        [SerializeField] private TipCraftingSlotUI slotPrefab;
        [SerializeField] private Transform slotParent;

        public void SetCraftingItem(CraftableMaterial craftableMaterial)
        {
            ClearSlots();
            title.text = craftableMaterial.name + " -> " + craftableMaterial.craftingRecipe.getAmount;
            foreach (var recipeItem in craftableMaterial.craftingRecipe.resources)
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

