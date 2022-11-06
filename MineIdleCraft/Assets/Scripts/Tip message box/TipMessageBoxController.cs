using System;
using System.Collections;
using System.Collections.Generic;
using Crafting;
using TipBox;
using UnityEngine;

namespace TipBox
{
    public class TipMessageBoxController : MonoBehaviour
    {
        [SerializeField] private TipMessageBox tipMessageBox;

        private RectTransform _tipRectTransform;
        private Camera _camera;
        private bool _isHided = true;

        private void Awake()
        {
            _camera = Camera.main;
            _tipRectTransform = tipMessageBox.GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (_isHided) return;
            var offset = (Vector3)(_tipRectTransform.rect.size / 2f);
            offset.y = -offset.y;
            
            _tipRectTransform.position = Input.mousePosition + offset;
        }

        public void ShowMessage(CraftingItem craftingItem)
        {
            _isHided = false;
            tipMessageBox.SetCraftingItem(craftingItem);
            tipMessageBox.gameObject.SetActive(true);
        }

        public void HideMessage()
        {
            _isHided = true;
            tipMessageBox.gameObject.SetActive(false);
        }
    }
}