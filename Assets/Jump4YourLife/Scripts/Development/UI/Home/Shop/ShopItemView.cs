using System;
using UnityEngine;
using UnityEngine.UI;

namespace Jump4YourLife.Scripts.Development.UI.Home
{
    public class ShopItemView : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private GameObject selectedBorder;
        [SerializeField] private GameObject selectedTick;
        [SerializeField] private Button actionButton;
        private Action _onClick;

        private void OnEnable() => actionButton.onClick.AddListener(HandleActionClicked);
        private void OnDisable() => actionButton.onClick.RemoveListener(HandleActionClicked);

        public void Initialize(Action onClick)
        {
            _onClick = onClick;
        }

        public void UpdateState(Sprite icon, bool isSelected)
        {
            iconImage.sprite = icon;
            selectedBorder.SetActive(isSelected);
            selectedTick.SetActive(isSelected);
        }

        private void HandleActionClicked() => _onClick();
    }
}
