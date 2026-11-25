using Jump4YourLife.Scripts.Core.Theme;
using Jump4YourLife.Scripts.Development.Managers;
using Jump4YourLife.Scripts.Development.Pools;
using UnityEngine;

namespace Jump4YourLife.Scripts.Development.Controllers.Platform
{
    public class PlatformController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private BoxCollider2D boxCollider;
        [SerializeField] private float speed = 1.4f;
        [SerializeField] private int hitsToBreak = 2;
        [SerializeField] private Rigidbody2D rb;
        private PlatformPool _pool;
        private Sprite _crackedSprite;
        private int _direction = 1;
        private int _wallHitCount;
        private bool _isMoving = true;

        public float HalfWidth => boxCollider.size.x * 0.5f;
        public float TopOffsetY => boxCollider.offset.y + boxCollider.size.y * 0.5f;
        public float MoveSpeedX => _isMoving ? _direction * speed : 0f;

        public void SetMovementActive(bool active) => _isMoving = active;

        public void Initialize(PlatformPool pool, int startDirection, ThemeData theme)
        {
            _pool = pool;
            _direction = startDirection;
            _wallHitCount = 0;
            _crackedSprite = theme.PlatformCrackedSprite;
            spriteRenderer.sprite = theme.PlatformIntactSprite;
            boxCollider.isTrigger = false;
        }

        public void SetPassThrough() => boxCollider.isTrigger = true;

        public bool TakeWallHit()
        {
            _wallHitCount++;
            if (_wallHitCount >= hitsToBreak)
            {
                EventManager.RaisePlatformBroken();
                _pool.Release(this);
                return true;
            }

            EventManager.RaisePlatformCracked();
            spriteRenderer.sprite = _crackedSprite;
            return false;
        }

        private void FixedUpdate()
        {
            rb.linearVelocity = new Vector2(MoveSpeedX, 0f);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Wall")) return;
            _direction = transform.position.x < other.transform.position.x ? -1 : 1;
            EventManager.RaisePlatformHitWall(this);
        }
    }
}
