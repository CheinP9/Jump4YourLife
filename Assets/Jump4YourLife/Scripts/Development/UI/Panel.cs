using Jump4YourLife.Scripts.Core.Enums;
using UnityEngine;

namespace Jump4YourLife.Scripts.Development.UI
{
    public abstract class Panel : MonoBehaviour
    {
        [SerializeField] private PanelLayer layer = PanelLayer.Sheet;

        public PanelLayer Layer => layer;

        public virtual void Open()
        {
            gameObject.SetActive(true);
            transform.SetAsLastSibling();
        }

        public virtual void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
