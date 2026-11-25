using Jump4YourLife.Scripts.Core.Character;
using Jump4YourLife.Scripts.Core.Config;
using Jump4YourLife.Scripts.Development.Controllers.Platform;
using Jump4YourLife.Scripts.Development.Managers;
using UnityEngine;

namespace Jump4YourLife.Scripts.Development.Controllers.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private BoxCollider2D boxCollider;
        [SerializeField] private CharacterVisual characterVisual;
        [SerializeField] private float jumpForce = 3.2f;

        private PlatformController _currentPlatform;

        private void OnEnable()
        {
            EventManager.OnTap += Jump;
            EventManager.OnPlatformHitWall += HandlePlatformHitWall;
        }

        private void OnDisable()
        {
            EventManager.OnTap -= Jump;
            EventManager.OnPlatformHitWall -= HandlePlatformHitWall;
        }

        public void Initialize(CharacterData characterData)
        {
            characterVisual.ApplyCharacter(characterData);
        }

        public void SetGameplayActive(bool active) => rb.simulated = active;

        public void SpawnOn(PlatformController startPlatform)
        {
            float playerBottomOffset = boxCollider.size.y * 0.5f - boxCollider.offset.y;
            Vector3 spawnPos = startPlatform.transform.position + Vector3.up * (playerBottomOffset + startPlatform.TopOffsetY);
            transform.position = spawnPos;
            rb.linearVelocity = Vector2.zero;
            AttachToPlatform(startPlatform);
        }

        private void FixedUpdate()
        {
            if (!rb.simulated || _currentPlatform == null) return;
            rb.linearVelocity = new Vector2(_currentPlatform.MoveSpeedX, rb.linearVelocity.y);
        }

        private void Jump()
        {
            if (!rb.simulated || _currentPlatform == null) return;
            _currentPlatform.SetPassThrough();
            _currentPlatform = null;
            rb.linearVelocity = new Vector2(0f, jumpForce);
            characterVisual.SetJumping(true);
            EventManager.RaisePlayerJumped();
        }

        private void HandlePlatformHitWall(PlatformController platform)
        {
            if (platform != _currentPlatform) return;
            if (platform.TakeWallHit())
            {
                _currentPlatform = null;
                rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
                characterVisual.SetJumping(true);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_currentPlatform != null) return;
            if (!collision.gameObject.TryGetComponent(out PlatformController platform)) return;
            AttachToPlatform(platform);
            float distToCenter = Mathf.Abs(transform.position.x - platform.transform.position.x);
            bool isPerfect = distToCenter <= GameConfig.PerfectDistance;
            EventManager.RaisePlatformLanded(platform, isPerfect);
        }

        private void AttachToPlatform(PlatformController platform)
        {
            _currentPlatform = platform;
            characterVisual.SetJumping(false);
        }
    }
}
