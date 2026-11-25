using Jump4YourLife.Scripts.Core.Theme;
using UnityEngine;
using UnityEngine.UI;

namespace Jump4YourLife.Scripts.Development.Controllers.Theme
{
    public class BackgroundController : MonoBehaviour
    {
        [SerializeField] private ThemeDatabase themeDatabase;
        [SerializeField] private Image backgroundImage;

        public void SetTheme(int themeId)
        {
            ThemeData theme = themeDatabase.GetThemeById(themeId);
            backgroundImage.sprite = theme.BackgroundSprite;
        }
    }
}
