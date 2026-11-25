using Jump4YourLife.Scripts.Development.Controllers.Platform;
using Jump4YourLife.Scripts.Development.Controllers.Player;
using Jump4YourLife.Scripts.Development.LoadSave;
using Jump4YourLife.Scripts.Development.Pools;

namespace Jump4YourLife.Scripts.Development.StateMachine
{
    public class GameContext
    {
        public PlatformSpawner PlatformSpawner { get; }
        public PlatformPool PlatformPool { get; }
        public PlayerController Player { get; }
        public PlayerSaveData PlayerSaveData { get; }
        public int CurrentScore { get; set; }
        public int Combo { get; set; }

        public GameContext(PlatformSpawner platformSpawner, PlatformPool platformPool, PlayerController player, PlayerSaveData playerSaveData)
        {
            PlatformSpawner = platformSpawner;
            PlatformPool = platformPool;
            Player = player;
            PlayerSaveData = playerSaveData;
        }

        public void ResetScore()
        {
            CurrentScore = 0;
            Combo = 0;
        }
    }
}
