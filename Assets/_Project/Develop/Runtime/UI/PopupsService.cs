using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.UI.CommonViews;
using _Project.Develop.Runtime.UI.MainMenu;
using _Project.Develop.Runtime.Utilities.Factories;
using UnityEngine;

namespace _Project.Develop.Runtime.UI
{
    public abstract class PopupsService : IDisposable
    {
        protected readonly ViewsFactory ViewsFactory;
        private readonly ProjectPresentersFactory _projectPresentersFactory;

        private readonly Dictionary<PopupPresenterBase, PopupInfo> _presenterToInfo 
            = new Dictionary<PopupPresenterBase, PopupInfo>();

        protected PopupsService(ViewsFactory viewsFactory, ProjectPresentersFactory projectPresentersFactory)
        {
            ViewsFactory = viewsFactory;
            _projectPresentersFactory = projectPresentersFactory;
        }

        protected abstract Transform PopupsLayer { get; }

        public InfoPopupPresenter OpenInfoPopup(string infoText, Action closeCallback = null)
        {
            InfoPopupView view = ViewsFactory.CreateView<InfoPopupView>(ViewIDs.InfoPopupView, PopupsLayer);
            InfoPopupPresenter popupPresenter = _projectPresentersFactory.CreateInfoPopupPresenter(view, infoText);

            OnPopupCreated(popupPresenter, view, closeCallback);

            return popupPresenter;
        }

        public void OnClosePopup(PopupPresenterBase popup)
        {
            popup.CloseRequested -= OnClosePopup;

            popup.Hide(() =>
            {
                DisposeFor(popup);
                _presenterToInfo[popup].CloseCallback?.Invoke();
                _presenterToInfo.Remove(popup);
            });
        }

        public void Dispose()
        {
            foreach (PopupPresenterBase popup in _presenterToInfo.Keys)
            {
                popup.CloseRequested -= OnClosePopup;
                DisposeFor(popup);
            }

            _presenterToInfo.Clear();
        }

        protected void OnPopupCreated(InfoPopupPresenter popupPresenter, InfoPopupView view, Action closeCallback = null)
        {
            PopupInfo popupInfo = new PopupInfo(view, closeCallback);

            _presenterToInfo.Add(popupPresenter, popupInfo);
            popupPresenter.Initialize();
            popupPresenter.Show();

            popupPresenter.CloseRequested += OnClosePopup;
        }

        private void DisposeFor(PopupPresenterBase popup)
        {
            popup.Dispose();
            ViewsFactory.Release(_presenterToInfo[popup].View);
        }

    }
}
