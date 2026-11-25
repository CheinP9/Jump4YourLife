using UnityEngine;

namespace Jump4YourLife.Scripts.Development.Controllers.CameraSystem
{
    public class ScreenWalls : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer leftWall;
        [SerializeField] private SpriteRenderer rightWall;
        public float LeftLimitX => leftWall.bounds.max.x;
        public float RightLimitX => rightWall.bounds.min.x;
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
