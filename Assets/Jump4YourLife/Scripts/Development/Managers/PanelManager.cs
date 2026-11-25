using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Jump4YourLife.Scripts.Core.Enums;
using Jump4YourLife.Scripts.Development.UI;
using Jump4YourLife.Scripts.Development.Utils;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Jump4YourLife.Scripts.Development.Managers
{
    public class PanelManager : Singleton<PanelManager>
    {
        [SerializeField] private Transform sheetContainer;
        [SerializeField] private Transform modalContainer;
        [SerializeField] private Transform activityContainer;

        public Dictionary<string, Panel> panels = new Dictionary<string, Panel>();
        private readonly Dictionary<string, AsyncOperationHandle<GameObject>> _instanceHandles = new Dictionary<string, AsyncOperationHandle<GameObject>>();
        private readonly Dictionary<string, UniTask<Panel>> _loadingTasks = new Dictionary<string, UniTask<Panel>>();

        public override void Awake()
        {
            base.Awake();
            if (Instance != this) return;

            Panel[] list = GetComponentsInChildren<Panel>(true);
            foreach (Panel panel in list)
            {
                panels[panel.name] = panel;
            }
        }
        public void OpenPanel(string panelName)
        {
            OpenPanelAsync(panelName).Forget();
        }
        public void ClosePanel(string panelName)
        {
            if (!IsExisted(panelName)) return;
            panels[panelName].Close();
        }
        public async UniTask OpenPanelAsync(string panelName)
        {
            Panel panel = await GetPanelAsync(panelName);
            panel.Open();
        }
        public async UniTask<Panel> GetPanelAsync(string panelName)
        {
            if (IsExisted(panelName))
            {
                return panels[panelName];
            }
            if (_loadingTasks.TryGetValue(panelName, out UniTask<Panel> loadingTask))
            {
                return await loadingTask;
            }
            UniTask<Panel> task = LoadPanelInternal(panelName);
            _loadingTasks[panelName] = task;
            try
            {
                return await task;
            }
            finally
            {
                _loadingTasks.Remove(panelName);
            }
        }
        private Transform GetContainer(PanelLayer layer)
        {
            switch (layer)
            {
                case PanelLayer.Sheet: return sheetContainer;
                case PanelLayer.Modal: return modalContainer;
                case PanelLayer.Activity: return activityContainer;
                default: return null;
            }
        }
        public void RemovePanel(string panelName)
        {
            panels.TryGetValue(panelName, out Panel panel);

            if (_instanceHandles.TryGetValue(panelName, out AsyncOperationHandle<GameObject> handle))
            {
                Addressables.ReleaseInstance(handle);
                _instanceHandles.Remove(panelName);
            }
            else if (panel != null)
            {
                Destroy(panel.gameObject);
            }

            if (IsExisted(panelName))
            {
                panels.Remove(panelName);
            }
        }
        public void CloseAllPanels()
        {
            List<Panel> panelList = new List<Panel>(panels.Values);
            foreach (Panel panel in panelList)
            {
                panel.Close();
            }
        }

        private bool IsExisted(string panelName) => panels.ContainsKey(panelName);
        public void Unregister(string panelName)
        {
            _loadingTasks.Remove(panelName);
            RemovePanel(panelName);
        }

        private async UniTask<Panel> LoadPanelInternal(string panelName)
        {
            AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(panelName, transform);

            try
            {
                GameObject panelObject = await handle.ToUniTask();
                Panel newPanel = panelObject.GetComponent<Panel>();
                newPanel.name = panelName;
                newPanel.transform.SetParent(GetContainer(newPanel.Layer), false);
                newPanel.Close();
                panels[panelName] = newPanel;
                _instanceHandles[panelName] = handle;
                return newPanel;
            }
            catch
            {
                if (handle.IsValid())
                {
                    Addressables.Release(handle);
                }
                throw;
            }
        }
    }
}
