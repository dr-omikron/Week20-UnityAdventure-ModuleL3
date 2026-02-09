using _Project.Develop.Runtime.Gameplay.Features;
using _Project.Develop.Runtime.Gameplay.Inputs;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.UI;
using _Project.Develop.Runtime.UI.Gameplay;

namespace _Project.Develop.Runtime.Utilities.Factories
{
    public class GameplayPresentersFactory
    {
        private readonly DIContainer _container;

        public GameplayPresentersFactory(DIContainer container)
        {
            _container = container;
        }

        public TextBlockPresenter CreateTextBlockPresenter(TextBlockView textBlockView, string titleText, IStringChanged stringChanged)
        {
            return new TextBlockPresenter(textBlockView, titleText, stringChanged);
        }

        public TextBlockListPresenter CreateTextBlocksListPresenter(TextBlocksListView textBlocksListView)
        {
            return new TextBlockListPresenter(_container.Resolve<ViewsFactory>(),
                textBlocksListView,
                this,
                _container.Resolve<SymbolsSequenceGenerator>(),
                _container.Resolve<InputStringReader>());
        }

        public GameplayScreenPresenter CreateGameplayScreenPresenter(GameplayScreenView view)
        {
            return new GameplayScreenPresenter(
                view, 
                _container.Resolve<ProjectPresentersFactory>(), 
                this);
        }
    }
}
