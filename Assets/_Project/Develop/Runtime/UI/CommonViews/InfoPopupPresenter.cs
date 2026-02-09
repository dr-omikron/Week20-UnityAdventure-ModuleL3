namespace _Project.Develop.Runtime.UI.CommonViews
{
    public class InfoPopupPresenter : PopupPresenterBase
    {
        private readonly InfoPopupView _view;

        public InfoPopupPresenter(InfoPopupView view, string infoText)
        {
            _view = view;
            _view.SetText(infoText);
        }

        protected override PopupViewBase PopupView => _view;
    }
}
