using Jump4YourLife.Scripts.Core.Enums;
using Jump4YourLife.Scripts.Development.Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Jump4YourLife.Scripts.Development.UI
{
    public class UIButtonSound : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private AudioLibrarySounds sound = AudioLibrarySounds.Click;

        public void OnPointerClick(PointerEventData eventData)
        {
            AudioManager.PlaySound(sound);
        }
    }
}
