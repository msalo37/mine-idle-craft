using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Materials.Editor
{
    [CustomEditor(typeof(BaseMaterial), true)]
    public class BaseMaterialEditor : UnityEditor.Editor
    {
        public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
        {
            Texture2D defaultTexture2D = base.RenderStaticPreview(assetPath, subAssets, width, height);

            BaseMaterial baseMaterial = (BaseMaterial)serializedObject.targetObject;
            
            if (baseMaterial == null) return defaultTexture2D;
            if (baseMaterial.sprite == null) return defaultTexture2D;
            if (baseMaterial.sprite.texture == null) return defaultTexture2D;

            var tex = new Texture2D(width, height);
            EditorUtility.CopySerialized(baseMaterial.sprite.texture, tex);
            return tex;
        }
    }
}