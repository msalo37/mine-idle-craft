using UnityEditor;
using UnityEngine;

namespace Crafting
{
    [CustomEditor(typeof(CraftingItem))]
    public class CraftingItemEditor : Editor
    {
        public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
        {
            CraftingItem craftingItem = (CraftingItem)serializedObject.targetObject;

            if (craftingItem == null) return base.RenderStaticPreview(assetPath, subAssets, width, height);
            if (craftingItem.icon == null) return base.RenderStaticPreview(assetPath, subAssets, width, height);
            if (craftingItem.icon.texture == null) return base.RenderStaticPreview(assetPath, subAssets, width, height);

            var tex = new Texture2D(width, height);
            EditorUtility.CopySerialized(craftingItem.icon.texture, tex);
            return tex;
        }
    }
}

