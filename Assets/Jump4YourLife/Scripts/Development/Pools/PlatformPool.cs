using System.Collections.Generic;
using Jump4YourLife.Scripts.Development.Controllers.Platform;
using UnityEngine;

namespace Jump4YourLife.Scripts.Development.Pools
{
    public class PlatformPool : MonoBehaviour
    {
        [SerializeField] private PlatformController platformPrefab;
        [SerializeField] private Transform dynamicParent;
        [SerializeField] private int initialSize = 10;

        private readonly Queue<PlatformController> _availablePlatforms = new Queue<PlatformController>();
        private readonly List<PlatformController> _activePlatforms = new List<PlatformController>();
        private bool _gameplayActive;

        public List<PlatformController> ActivePlatforms => _activePlatforms;

        public void Initialize()
        {
            for (int i = 0; i < initialSize; i++)
            {
                _availablePlatforms.Enqueue(CreatePlatform());
            }
        }

        private PlatformController CreatePlatform()
        {
            PlatformController p = Instantiate(platformPrefab, dynamicParent);
            p.gameObject.SetActive(false);
            return p;
        }

        public PlatformController GetPlatform()
        {
            PlatformController p = _availablePlatforms.Count > 0 ? _availablePlatforms.Dequeue() : CreatePlatform();
            p.gameObject.SetActive(true);
            p.SetMovementActive(_gameplayActive);
            _activePlatforms.Add(p);
            return p;
        }

        public void Release(PlatformController p)
        {
            if (!_activePlatforms.Remove(p)) return;
            p.gameObject.SetActive(false);
            _availablePlatforms.Enqueue(p);
        }

        public void ReleaseAll()
        {
            for (int i = _activePlatforms.Count - 1; i >= 0; i--)
            {
                _activePlatforms[i].gameObject.SetActive(false);
                _availablePlatforms.Enqueue(_activePlatforms[i]);
            }
            _activePlatforms.Clear();
        }

        public void SetGameplayActive(bool active)
        {
            _gameplayActive = active;
            foreach (PlatformController platform in _activePlatforms)
            {
                platform.SetMovementActive(active);
            }
        }
    }
}