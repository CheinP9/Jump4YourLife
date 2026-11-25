using Jump4YourLife.Scripts.Development.Controllers.Platform;
using Jump4YourLife.Scripts.Development.Managers;
using UnityEngine;

namespace Jump4YourLife.Scripts.Development.Controllers.CameraSystem
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float offsetY = 3.1f;    
        [SerializeField] private float smoothSpeed = 8f;

        private Vector3 _initialPosition;
        private float _targetY;
        private void Awake()
        {
            _initialPosition = transform.position;
            _targetY = _initialPosition.y;
        }
        private void OnEnable()
        {
            EventManager.OnPlatformLanded += HandlePlatformLanded;
            EventManager.OnGameReset += ResetCamera;
        }
        private void OnDisable()
        {
            EventManager.OnPlatformLanded -= HandlePlatformLanded;
            EventManager.OnGameReset -= ResetCamera;
        }

        private void HandlePlatformLanded(PlatformController platform, bool isPerfect)
        {
            float newTargetY = platform.transform.position.y - offsetY;
            if (newTargetY < _targetY)
            {
                _targetY = newTargetY;
            }
        }

        private void ResetCamera()
        {
            transform.position = _initialPosition;
            _targetY = _initialPosition.y;
        }

        private void Update()
        {
            Vector3 pos = transform.position;
            pos.y = Mathf.Lerp(pos.y, _targetY, smoothSpeed * Time.deltaTime);
            transform.position = pos;
        }
    }
}