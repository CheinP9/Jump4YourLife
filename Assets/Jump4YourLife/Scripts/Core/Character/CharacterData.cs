using UnityEngine;

namespace Jump4YourLife.Scripts.Core.Character
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/Character/CharacterData")]
    public class CharacterData : ScriptableObject
    {
        [SerializeField] private int id;
        [SerializeField] private string characterName;
        [SerializeField] private int requiredScore;
        [SerializeField] private bool isUnlockedByDefault = false;
        [SerializeField] private string unlockCondition;

        [SerializeField] private Sprite iconSprite;
        [SerializeField] private Sprite lockedIconSprite;
        [SerializeField] private Sprite fullPreviewSprite;
        [SerializeField] private GameObject characterPrefab;

        public int Id => id;
        public string CharacterName => characterName;
        public int RequiredScore => requiredScore;
        public bool IsUnlockedByDefault => isUnlockedByDefault;
        public string UnlockCondition => unlockCondition;
        public Sprite IconSprite => iconSprite;
        public Sprite LockedIconSprite => lockedIconSprite;
        public Sprite FullPreviewSprite => fullPreviewSprite;
        public GameObject CharacterPrefab => characterPrefab;
    }
}
