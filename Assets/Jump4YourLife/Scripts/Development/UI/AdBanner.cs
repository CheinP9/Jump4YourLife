using System;
using UnityEngine;
using UnityEngine.UI;
namespace Jump4YourLife.Scripts.Development.UI
{
        public class AdBanner : Panel
        {
                [SerializeField] private Button closeButton;
                private void OnEnable()
                {
                        closeButton.onClick.AddListener(HandleCloseClicked);
                }
                private void HandleCloseClicked()
                {
                        Close();
                }
                private void OnDisable()
                { 
                        closeButton.onClick.RemoveListener(HandleCloseClicked);
                }
        }
}