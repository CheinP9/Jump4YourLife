using Jump4YourLife.Scripts.Core.Config;
using UnityEngine;

namespace Jump4YourLife.Scripts.Development.LoadSave
{
    public static class SaveManager
    {
        public static PlayerSaveData LoadPlayerData()
        {
            if (PlayerPrefs.HasKey(GameConfig.PlayerSaveKey))
            {
                string json = PlayerPrefs.GetString(GameConfig.PlayerSaveKey);
                try
                {
                    PlayerSaveData data = JsonUtility.FromJson<PlayerSaveData>(json);
                        return data;
                }
                catch
                {
                    return new PlayerSaveData();
                }
            }
            return new PlayerSaveData();
        }

        public static void SavePlayerData(PlayerSaveData data)
        {
            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(GameConfig.PlayerSaveKey, json);
            PlayerPrefs.Save();
        }
    }
}
