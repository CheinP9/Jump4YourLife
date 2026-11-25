using Jump4YourLife.Scripts.Development.Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Jump4YourLife.Scripts.Development.Controllers
{
    public class InputController : MonoBehaviour
    {
        private void Update()
        {
            Pointer pointer = Pointer.current;
            if (pointer != null && pointer.press.wasPressedThisFrame)
            {
                if (EventSystem.current.IsPointerOverGameObject(pointer.deviceId)) return;
                EventManager.RaiseTap();
            }
        }
    }
}