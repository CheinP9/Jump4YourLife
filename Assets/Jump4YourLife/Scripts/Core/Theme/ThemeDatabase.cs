using System.Collections.Generic;
using UnityEngine;

namespace Jump4YourLife.Scripts.Core.Theme
{
    [CreateAssetMenu(fileName = "ThemeDatabase", menuName = "ScriptableObjects/Theme/ThemeDatabase")]
    public class ThemeDatabase : ScriptableObject
    {
        [SerializeField] private List<ThemeData> themes = new List<ThemeData>();
        public List<ThemeData> Themes => themes;
        public ThemeData GetThemeById(int id)
        {
            ThemeData found = themes.Find(t => t.Id == id);
            return found;
        }
    }
}