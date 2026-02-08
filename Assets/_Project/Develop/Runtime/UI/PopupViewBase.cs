using System;
using UnityEngine;

namespace _Project.Develop.Runtime.UI
{
    public abstract class PopupViewBase : MonoBehaviour, IShowableView
    {
        public event Action CloseRequested;

        [SerializeField] private CanvasGroup _mainGroup;

        private void Awake()
        {
            _mainGroup.alpha = 0;
        }

        public void OnCloseButtonClicked() => CloseRequested?.Invoke();

        public void Show()
        {
            OnPreShow();
            _mainGroup.alpha = 1;
            OnPostShow();
        }

        public void Hide()
        {
            OnPreHide();
            _mainGroup.alpha = 0;
            OnPostHide();
        }

        protected virtual void OnPreShow() { }
        protected virtual void OnPostShow() { }
        protected virtual void OnPreHide() { }
        protected virtual void OnPostHide() { }
    }
}
