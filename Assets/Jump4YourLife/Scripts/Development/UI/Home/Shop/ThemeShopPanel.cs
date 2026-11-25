using System.Collections.Generic;
using Jump4YourLife.Scripts.Core.Theme;
using Jump4YourLife.Scripts.Development.LoadSave;
using Jump4YourLife.Scripts.Development.Managers;
using UnityEngine;

namespace Jump4YourLife.Scripts.Development.UI.Home.Shop
{
    public class ThemeShopPanel : Panel
    {
        [SerializeField] private ThemeDatabase themeDatabase;
        [SerializeField] private Transform itemContainer;
        [SerializeField] private ShopItemView itemPrefab;
        private List<ShopItemView> _views = new List<ShopItemView>();
        private void Awake()
        {
            for (int i = 0; i < themeDatabase.Themes.Count; i++)
            {
                ThemeData item = themeDatabase.Themes[i];
                ShopItemView view = Instantiate(itemPrefab, itemContainer);
                view.Initialize(() => OnItemClicked(item));
                _views.Add(view);
            }
        }
        private void OnEnable()
        {
            Refresh();
        }

        private void Refresh()
        {
            PlayerSaveData save = SaveManager.LoadPlayerData();
            for (int i = 0; i < themeDatabase.Themes.Count; i++)
            {
                ThemeData item = themeDatabase.Themes[i];
                _views[i].UpdateState(item.IconSprite, save.selectedThemeId == item.Id);
            }
        }
        private void OnItemClicked(ThemeData item)
        {
            PlayerSaveData save = SaveManager.LoadPlayerData();
            save.selectedThemeId = item.Id;
            SaveManager.SavePlayerData(save);
            EventManager.RaiseThemeSelected(item.Id);
            Refresh();
        }
    }
}
