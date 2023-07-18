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

        public void OnMaterialUpdated(MineableMaterial mineableMaterial)
        {
            progressBar.fillAmount = 0;
            if (mineableMaterial == null)
            {
                HideMaterial();
                return;
            }
            materialIcon.color = Color.white;
            materialIcon.sprite = mineableMaterial.mineSprite == null ? mineableMaterial.sprite : mineableMaterial.mineSprite;
            materialTitle.text = mineableMaterial.name;
        }

        private void HideMaterial()
        {
            materialTitle.text = "Nothing";
            materialIcon.color = new Color(0, 0, 0, 0);
        }
    }
}
