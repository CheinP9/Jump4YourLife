using UnityEngine;

namespace Jump4YourLife.Scripts.Development.Controllers.CameraSystem
{
    public class ScreenWalls : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private SpriteRenderer leftWall;
        [SerializeField] private SpriteRenderer rightWall;
        [SerializeField] private float wallVisibleWidth = 0.25f;

        public float LeftLimitX => leftWall.bounds.max.x;
        public float RightLimitX => rightWall.bounds.min.x;

        private void Awake()
        {
            float halfScreenWidth = mainCamera.orthographicSize * mainCamera.aspect;
            float wallX = halfScreenWidth + leftWall.bounds.extents.x - wallVisibleWidth;
            leftWall.transform.position = new Vector3(-wallX, leftWall.transform.position.y, leftWall.transform.position.z);
            rightWall.transform.position = new Vector3(wallX, rightWall.transform.position.y, rightWall.transform.position.z);
        }
        public void SetWallSprite(Sprite wallSprite)
        {
            leftWall.sprite = wallSprite;
            rightWall.sprite = wallSprite;
        }
        private void LateUpdate()
        {
            float tileHeight = leftWall.sprite.bounds.size.y;
            float camY = transform.position.y;
            while (leftWall.transform.position.y - camY >= tileHeight)
            {
                leftWall.transform.position += new Vector3(0f, -tileHeight, 0f);
                rightWall.transform.position += new Vector3(0f, -tileHeight, 0f);
            }
            while (camY - leftWall.transform.position.y >= tileHeight)
            {
                leftWall.transform.position += new Vector3(0f, tileHeight, 0f);
                rightWall.transform.position += new Vector3(0f, tileHeight, 0f);
            }
        }
    }
}
