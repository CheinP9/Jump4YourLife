using Jump4YourLife.Scripts.Development.Managers;
using UnityEngine;

namespace Jump4YourLife.Scripts.Development.Controllers
{
    public class GameOverTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                EventManager.RaiseLoseLevel();
            }
        }
    }
}