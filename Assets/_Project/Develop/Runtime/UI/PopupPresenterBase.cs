using System;

namespace _Project.Develop.Runtime.UI
{
    public abstract class PopupPresenterBase : IPresenter
    {
        public event Action<PopupPresenterBase> CloseRequested;

        protected abstract PopupViewBase PopupView { get; }

        public virtual void Initialize()
        {
            
        }

        public virtual void Dispose()
        {
            PopupView.CloseRequested -= OnCloseRequested;
        }

        public void Show()
        {
            OnPreShow();
            PopupView.Show();
            OnPostShow();
        }

        public void Hide(Action callback = null)
        {
            OnPreHide();
            PopupView.Hide();
            OnPostHide();

            callback?.Invoke();
        }

        protected virtual void OnPreShow()
        {
            PopupView.CloseRequested += OnCloseRequested;
        }

        protected virtual void OnPostShow() { }

        protected virtual void OnPreHide()
        {
            PopupView.CloseRequested -= OnCloseRequested;
        }

        protected virtual void OnPostHide() { }

        protected void OnCloseRequested() => CloseRequested?.Invoke(this);
    }
}
