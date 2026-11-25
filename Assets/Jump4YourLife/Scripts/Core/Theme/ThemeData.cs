using UnityEngine;

namespace Jump4YourLife.Scripts.Core.Theme
{
    [CreateAssetMenu(fileName = "ThemeData", menuName = "ScriptableObjects/Theme/ThemeData")]
    public class ThemeData : ScriptableObject
    {
        [SerializeField] private int id = 1;
        [SerializeField] private string themeName = "Snow Mountain";

        [SerializeField] private Sprite backgroundSprite;
        [SerializeField] private Sprite iconSprite;

        [SerializeField] private Sprite platformIntactSprite;
        [SerializeField] private Sprite platformCrackedSprite;
        [SerializeField] private Sprite wallSprite;

        public int Id => id;
        public string ThemeName => themeName;
        public Sprite BackgroundSprite => backgroundSprite;
        public Sprite IconSprite =>  iconSprite;
        public Sprite PlatformIntactSprite => platformIntactSprite;
        public Sprite PlatformCrackedSprite => platformCrackedSprite;
        public Sprite WallSprite => wallSprite;
    }
}
