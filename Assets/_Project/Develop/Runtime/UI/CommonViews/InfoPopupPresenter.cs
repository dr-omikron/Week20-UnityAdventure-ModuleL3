namespace _Project.Develop.Runtime.UI.CommonViews
{
    public class InfoPopupPresenter : PopupPresenterBase
    {
        private readonly InfoPopupView _view;
        private readonly string _infoText;

        public InfoPopupPresenter(InfoPopupView view, string infoText)
        {
            _view = view;
            _infoText = infoText;
        }

        protected override PopupViewBase PopupView => _view;
    }
}
