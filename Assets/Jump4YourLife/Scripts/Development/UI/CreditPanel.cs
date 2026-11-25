using UnityEngine;
using UnityEngine.UI;

namespace Jump4YourLife.Scripts.Development.UI
{
    public class CreditPanel : Panel
    {
        [SerializeField] private Button closeButton;

        private void OnEnable() => closeButton.onClick.AddListener(Close);
        private void OnDisable() => closeButton.onClick.RemoveListener(Close);
    }
}
