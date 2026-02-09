using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.UI;
using _Project.Develop.Runtime.Utilities.AssetsManagement;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Develop.Runtime.Utilities.Factories
{
    public class ViewsFactory
    {
        private readonly ResourcesAssetsLoader _resourcesAssetsLoader;

        private readonly Dictionary<string, string> _viewIDToResourcesPath = new Dictionary<string, string>
        {
            { ViewIDs.IconTextView, "UI/IconTextView" },
            { ViewIDs.MainMenuScreenView, "UI/MainMenu/MainMenuScreenView" },
            { ViewIDs.GameplayScreenView, "UI/Gameplay/GameplayScreenView" },
            { ViewIDs.InfoPopupView, "UI/InfoPopup" },
            { ViewIDs.TextBlockView, "UI/Gameplay/TextBlockView" }
        };

        public ViewsFactory(ResourcesAssetsLoader resourcesAssetsLoader)
        {
            _resourcesAssetsLoader = resourcesAssetsLoader;
        }

        public TView CreateView<TView>(string viewID, Transform parent = null) where TView : MonoBehaviour, IView
        {
            if(_viewIDToResourcesPath.TryGetValue(viewID, out string viewPath) == false)
                throw new ArgumentException($"Not find resource path {typeof(TView)}, searched view ID {viewID}");

            GameObject prefab = _resourcesAssetsLoader.Load<GameObject>(viewPath);
            GameObject instance = Object.Instantiate(prefab, parent);
            TView view = instance.GetComponent<TView>();

            if(view == null)
                throw new InvalidOperationException($"Not found {typeof(TView)} component on view instance");

            return view;
        }

        public void Release<TView>(TView view) where TView : MonoBehaviour, IView
        {
            Object.Destroy(view.gameObject);
        }
    }
}
