using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.Features;
using _Project.Develop.Runtime.Gameplay.Inputs;
using _Project.Develop.Runtime.Utilities.Factories;

namespace _Project.Develop.Runtime.UI.Gameplay
{
    public class TextBlockListPresenter : IPresenter
    {
        private const string GeneratedStringTextBlockHeader = "Repeat the generated string: ";
        private const string PlayerInputTextBlockHeader = "You entered: ";
        
        private readonly ViewsFactory _viewsFactory;
        private readonly TextBlocksListView _textBlocksListView;
        private readonly GameplayPresentersFactory _gameplayPresentersFactory;
        private readonly SymbolsSequenceGenerator _symbolsSequenceGenerator;
        private readonly InputStringReader _inputStringReader;

        private readonly List<TextBlockPresenter> _textBlockPresenters = new List<TextBlockPresenter>();

        public TextBlockListPresenter(
            ViewsFactory viewsFactory, 
            TextBlocksListView view, 
            GameplayPresentersFactory gameplayPresentersFactory, 
            SymbolsSequenceGenerator symbolsSequenceGenerator, 
            InputStringReader inputStringReader)
        {
            _viewsFactory = viewsFactory;
            _textBlocksListView = view;
            _gameplayPresentersFactory = gameplayPresentersFactory;
            _symbolsSequenceGenerator = symbolsSequenceGenerator;
            _inputStringReader = inputStringReader;
        }

        public void Initialize()
        {
            CreateGeneratedStringTextBlock();
            CreateInputStringTextBlock();
        }

        public void Dispose()
        {
            foreach (TextBlockPresenter presenter in _textBlockPresenters)
            {
                _textBlocksListView.Remove(presenter.View);
                _viewsFactory.Release(presenter.View);
                presenter.Dispose();
            }

            _textBlockPresenters.Clear();
        }

        private void CreateInputStringTextBlock()
        {
            TextBlockView textBlockView = CreateTextBlockView();

            TextBlockPresenter textBlockPresenter = _gameplayPresentersFactory.CreateTextBlockPresenter(
                textBlockView,
                PlayerInputTextBlockHeader, 
                _inputStringReader);

            textBlockPresenter.Initialize();
            _textBlockPresenters.Add(textBlockPresenter);
        }

        private void CreateGeneratedStringTextBlock()
        {
            TextBlockView textBlockView = CreateTextBlockView();

            TextBlockPresenter textBlockPresenter = _gameplayPresentersFactory.CreateTextBlockPresenter(
                textBlockView,
                GeneratedStringTextBlockHeader, 
                _symbolsSequenceGenerator);

            textBlockPresenter.Initialize();
            _textBlockPresenters.Add(textBlockPresenter);
        }

        private TextBlockView CreateTextBlockView()
        {
            TextBlockView textBlockView = _viewsFactory.CreateView<TextBlockView>(ViewIDs.TextBlockView);
            _textBlocksListView.Add(textBlockView);
            return textBlockView;
        }
    }
}
