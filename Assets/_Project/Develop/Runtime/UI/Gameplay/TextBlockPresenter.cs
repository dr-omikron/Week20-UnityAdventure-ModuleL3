
namespace _Project.Develop.Runtime.UI.Gameplay
{
    public class TextBlockPresenter : IPresenter
    {
        private readonly IStringChanged _stringChanged;
        private readonly TextBlockView _view;
        private readonly string _titleText;

        public TextBlockPresenter(TextBlockView view, string titleText, IStringChanged stringChanged)
        {
            _view = view;
            _titleText = titleText;

            _stringChanged = stringChanged;
        }

        public TextBlockView View => _view;

        public void Initialize()
        {
            _stringChanged.Changed += OnTextChanged;
            _view.SetTitle(_titleText);
        }

        public void Dispose()
        {
            _stringChanged.Changed -= OnTextChanged;
        }

        private void OnTextChanged(string changedText)
        {
            _view.SetText(changedText);
        }

    }
}
