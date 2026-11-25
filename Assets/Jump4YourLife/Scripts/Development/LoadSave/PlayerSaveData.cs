using System;
using Jump4YourLife.Scripts.Core.Config;

namespace Jump4YourLife.Scripts.Development.LoadSave
{
    [Serializable]
    public class PlayerSaveData
    {
        public int highScore;
        public int selectedCharacterId = GameConfig.DefaultCharacterId;
        public int selectedThemeId = GameConfig.DefaultThemeId;
    }
}
