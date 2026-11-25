using System;
using Jump4YourLife.Scripts.Development.Controllers.Platform;

namespace Jump4YourLife.Scripts.Development.Managers
{
    public static class EventManager
    {
        public static event Action OnTap;
        public static event Action<PlatformController> OnPlatformHitWall;
        public static event Action OnPlatformCracked;
        public static event Action OnPlatformBroken;
        public static event Action<PlatformController, bool> OnPlatformLanded;
        public static event Action<int> OnScoreChanged;
        public static event Action OnRequestPlay;
        public static event Action OnRequestPause;
        public static event Action OnRequestRestart;
        public static event Action OnRequestHome;
        public static event Action OnLoseLevel;
        public static event Action OnGameReset;
        public static event Action OnPlayerJumped;
        public static event Action<int> OnThemeSelected;

        public static void RaiseTap() => OnTap?.Invoke();
        public static void RaisePlatformHitWall(PlatformController platform) => OnPlatformHitWall?.Invoke(platform);
        public static void RaisePlatformCracked() => OnPlatformCracked?.Invoke();
        public static void RaisePlatformBroken() => OnPlatformBroken?.Invoke();
        public static void RaisePlatformLanded(PlatformController platform, bool isPerfect) => OnPlatformLanded?.Invoke(platform, isPerfect);
        public static void RaiseScoreChanged(int score) => OnScoreChanged?.Invoke(score);
        public static void RaiseRequestPlay() => OnRequestPlay?.Invoke();
        public static void RaiseRequestPause() => OnRequestPause?.Invoke();
        public static void RaiseRequestRestart() => OnRequestRestart?.Invoke();
        public static void RaiseRequestHome() => OnRequestHome?.Invoke();
        public static void RaiseLoseLevel() => OnLoseLevel?.Invoke();
        public static void RaiseGameReset() => OnGameReset?.Invoke();
        public static void RaisePlayerJumped() => OnPlayerJumped?.Invoke();
        public static void RaiseThemeSelected(int themeId) => OnThemeSelected?.Invoke(themeId);
    }
}
