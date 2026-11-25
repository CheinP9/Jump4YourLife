using UnityEngine;

namespace Jump4YourLife.Scripts.Development.Utils
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private bool dontDestroyOnLoad;
        public static T Instance { get; private set; }
        public virtual void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this as T;
            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        protected virtual void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }
    }
}