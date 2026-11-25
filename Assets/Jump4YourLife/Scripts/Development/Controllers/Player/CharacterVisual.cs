using Jump4YourLife.Scripts.Core.Character;
using UnityEngine;

namespace Jump4YourLife.Scripts.Development.Controllers.Player
{
    public class CharacterVisual : MonoBehaviour
    {
        [SerializeField] private Transform visualRoot;
        private Animator _animator;

        public void ApplyCharacter(CharacterData data)
        {
            GameObject visual = Instantiate(data.CharacterPrefab, visualRoot);
            _animator = visual.GetComponent<Animator>();
        }

        public void SetJumping(bool isJumping)
        {
            _animator.SetBool("IsJumping", isJumping);
        }
    }
}