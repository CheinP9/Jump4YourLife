using System.Collections.Generic;
using Jump4YourLife.Scripts.Core.Character;
using Jump4YourLife.Scripts.Development.LoadSave;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Jump4YourLife.Scripts.Development.UI.Home.Shop
{
    public class CharacterShopPanel : Panel
    {
        [SerializeField] private CharacterDatabase characterDatabase;
        [SerializeField] private Transform itemContainer;
        [SerializeField] private ShopItemView itemPrefab;

        [SerializeField] private Image previewImage;
        [SerializeField] private TextMeshProUGUI previewNameText;
        [SerializeField] private TextMeshProUGUI previewConditionText;
        [SerializeField] private TextMeshProUGUI previewStatusText;

        private List<ShopItemView> _views = new List<ShopItemView>();
        private int _previewCharacterId;

        private void Awake()
        {
            for (int i = 0; i < characterDatabase.Characters.Count; i++)
            {
                CharacterData item = characterDatabase.Characters[i];
                ShopItemView view = Instantiate(itemPrefab, itemContainer);
                view.Initialize(() => OnItemClicked(item));
                _views.Add(view);
            }
        }

        private void OnEnable()
        {
            _previewCharacterId = SaveManager.LoadPlayerData().selectedCharacterId;
            Refresh();
        }

        private void Refresh()
        {
            PlayerSaveData save = SaveManager.LoadPlayerData();
            for (int i = 0; i < characterDatabase.Characters.Count; i++)
            {
                CharacterData item = characterDatabase.Characters[i];
                bool unlocked = IsUnlocked(item, save);
                Sprite icon = unlocked ? item.IconSprite : item.LockedIconSprite;
                _views[i].UpdateState(icon, save.selectedCharacterId == item.Id);
            }
            UpdatePreview(characterDatabase.GetCharacterById(_previewCharacterId), save);
        }

        private void UpdatePreview(CharacterData character, PlayerSaveData save)
        {
            bool unlocked = IsUnlocked(character, save);
            previewNameText.text = character.CharacterName;
            previewConditionText.text = character.UnlockCondition;
            if (unlocked)
            {
                previewImage.sprite = character.FullPreviewSprite;
                previewStatusText.text = "COMPLETED";
            }
            else
            {
                previewImage.sprite = character.LockedIconSprite;
                previewStatusText.text = $"{save.highScore}/{character.RequiredScore}";
            }
        }

        private void OnItemClicked(CharacterData item)
        {
            _previewCharacterId = item.Id;
            PlayerSaveData save = SaveManager.LoadPlayerData();
            if (IsUnlocked(item, save))
            {
                save.selectedCharacterId = item.Id;
                SaveManager.SavePlayerData(save);
            }
            Refresh();
        }

        private static bool IsUnlocked(CharacterData item, PlayerSaveData save)
        {
            return item.IsUnlockedByDefault || save.highScore >= item.RequiredScore;
        }
    }
}
