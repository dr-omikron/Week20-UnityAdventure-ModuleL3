using System.Collections.Generic;
using _Project.Develop.Runtime.UI.CommonViews;
using _Project.Develop.Runtime.Utilities.Factories;

namespace _Project.Develop.Runtime.UI.Gameplay
{
    public class GameplayScreenPresenter : IPresenter
    {
        private readonly GameplayScreenView _screenView;
        private readonly ProjectPresentersFactory _projectPresentersFactory;
        private readonly GameplayPresentersFactory _gameplayPresentersFactory;

        private readonly List<IPresenter> _childPresenters = new List<IPresenter>();

        public GameplayScreenPresenter(
            GameplayScreenView screenView, 
            ProjectPresentersFactory projectPresentersFactory, 
            GameplayPresentersFactory gameplayPresentersFactory)
        {
            _screenView = screenView;
            _projectPresentersFactory = projectPresentersFactory;
            _gameplayPresentersFactory = gameplayPresentersFactory;
        }

        public void Initialize()
        {
            CreateIconTextListPresenter();
            CreateTextBlockListPresenter();

            foreach (IPresenter childPresenter in _childPresenters)
                childPresenter.Initialize();
        }

        public void Dispose()
        {
            foreach (IPresenter childPresenter in _childPresenters)
                childPresenter.Dispose();

            _childPresenters.Clear();
        }

        private void CreateIconTextListPresenter()
        {
            IconTextListPresenter iconTextListPresenter =
                _projectPresentersFactory.CreateIconTextListPresenter(_screenView.IconTextListView);

            _childPresenters.Add(iconTextListPresenter);
        }

        private void CreateTextBlockListPresenter()
        {
            TextBlockListPresenter textBlockListPresenter 
                = _gameplayPresentersFactory.CreateTextBlocksListPresenter(_screenView.TextBlockListView);

            _childPresenters.Add(textBlockListPresenter);
        }
    }
}
