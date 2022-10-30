using UnityEngine;

namespace Crafting
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Crafting Item", fileName = "New crafting item")]
    public class CraftingItem : ScriptableObject
    {
        public Sprite icon;
        public Recipe recipe;
    }
}