using System.Collections.Generic;
using Jump4YourLife.Scripts.Core.Theme;
using Jump4YourLife.Scripts.Development.Controllers.CameraSystem;
using Jump4YourLife.Scripts.Development.Pools;
using UnityEngine;

namespace Jump4YourLife.Scripts.Development.Controllers.Platform
{
    public class PlatformSpawner : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Transform despawnPoint;
        [SerializeField] private ScreenWalls walls;
        [SerializeField] private PlatformPool pool;

        [SerializeField] private float verticalGap = 3.8f;
        [SerializeField] private float firstPlatformY = 3.0f;

        private ThemeData _theme;
        private float _nextSpawnY;

        public void SetTheme(ThemeData theme)
        {
            _theme = theme;
        }
        public PlatformController BuildInitialLevel()
        {
            pool.ReleaseAll();
            PlatformController firstPlatform = SpawnAt(firstPlatformY, true);
            _nextSpawnY = firstPlatformY - verticalGap;
            SpawnMissingPlatforms();
            return firstPlatform;
        }

        private void Update()
        {
            SpawnMissingPlatforms();
            List<PlatformController> active = pool.ActivePlatforms;
            for (int i = active.Count - 1; i >= 0; i--)
            {
                PlatformController p = active[i];
                if (p.transform.position.y > despawnPoint.position.y)
                {
                    pool.Release(p);
                }
            }
        }

        private void SpawnMissingPlatforms()
        {
            while (_nextSpawnY > spawnPoint.position.y)
            {
                SpawnAt(_nextSpawnY, false);
                _nextSpawnY -= verticalGap;
            }
        }

        private PlatformController SpawnAt(float y, bool isFirst)
        {
            PlatformController p = pool.GetPlatform();
            int dir = isFirst ? 0 : (Random.value < 0.5f ? -1 : 1);
            p.Initialize(pool, dir, _theme);
            p.transform.position = new Vector3(0, y, 0f);
            return p;
        }
    }
}
