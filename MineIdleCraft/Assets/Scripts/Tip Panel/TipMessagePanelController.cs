using Materials;
using UnityEngine;

namespace TipPanel
{
    public class TipMessagePanelController : MonoBehaviour
    {
        [SerializeField] private TipMessagePanelUI panel;
        [SerializeField] private RectTransform panelRectTransform;

        private bool _isHided = true;

        private void Update()
        {
            if (_isHided) return;
            var offset = (Vector3)(panelRectTransform.rect.size / 2f);
            offset.y = -offset.y;
            
            panelRectTransform.position = Input.mousePosition + offset;
        }

        public void ShowMessage(CraftableMaterial craftingMaterial)
        {
            _isHided = false;
            panel.SetCraftingItem(craftingMaterial);
            panel.gameObject.SetActive(true);
        }

        public void HideMessage()
        {
            _isHided = true;
            panel.gameObject.SetActive(false);
        }
    }
}