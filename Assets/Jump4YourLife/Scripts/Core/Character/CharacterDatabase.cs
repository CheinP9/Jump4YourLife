using System.Collections.Generic;
using UnityEngine;

namespace Jump4YourLife.Scripts.Core.Character
{
    [CreateAssetMenu(fileName = "CharacterDatabase", menuName = "ScriptableObjects/Character/CharacterDatabase")]
    public class CharacterDatabase : ScriptableObject
    {
        [SerializeField] private List<CharacterData> characters = new List<CharacterData>();
        public List<CharacterData> Characters => characters;
        public CharacterData GetCharacterById(int id)
        {
            CharacterData found = characters.Find(c => c.Id == id);
            return found;
        }
    }
}