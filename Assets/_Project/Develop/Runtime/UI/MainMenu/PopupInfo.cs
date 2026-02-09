using System;

namespace _Project.Develop.Runtime.UI.MainMenu
{
    public class PopupInfo
    {
        public PopupInfo(PopupViewBase view, Action closeCallback)
        {
            View = view;
            CloseCallback = closeCallback;
        }

        public PopupViewBase View { get; }
        public Action CloseCallback { get; }
    }
}
