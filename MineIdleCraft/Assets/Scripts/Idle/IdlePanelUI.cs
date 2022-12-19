using System;
using System.Collections.Generic;
using Materials;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Idle
{
    public class IdlePanelUI : MonoBehaviour
    {
        [SerializeField] private IdlePanel idlePanel;
        [SerializeField] private Image materialIcon;
        [SerializeField] private Image progressBar;
        [SerializeField] private TMP_Text materialTitle;

        private void Awake()
        {
            HideMaterial();
        }

        private void Update()
        {
            if (idlePanel.IsMining == false) return;
            progressBar.fillAmount = idlePanel.MiningPercent;
        }

        public void OnMaterialUpdated()
        {
            progressBar.fillAmount = 0;
            if (idlePanel.CurrentMaterial.Key == null)
            {
                HideMaterial();
                return;
            }
            BaseMaterial mat = idlePanel.CurrentMaterial.Key;
            materialIcon.color = Color.white;
            materialIcon.sprite = mat.sprite;
            materialTitle.text = mat.name;
        }

        private void HideMaterial()
        {
            materialTitle.text = "Nothing";
            materialIcon.color = new Color(0, 0, 0, 0);
        }
    }
}
