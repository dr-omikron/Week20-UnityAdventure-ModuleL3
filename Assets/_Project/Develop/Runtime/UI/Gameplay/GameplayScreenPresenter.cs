using System.Collections.Generic;
using _Project.Develop.Runtime.Utilities.Factories;

namespace _Project.Develop.Runtime.UI.Gameplay
{
    public class GameplayScreenPresenter : IPresenter
    {
        private readonly GameplayScreenView _gameplayScreenView;
        private readonly ProjectPresentersFactory _projectPresentersFactory;

        private readonly List<IPresenter> _childPresenters = new List<IPresenter>();

        public GameplayScreenPresenter(GameplayScreenView gameplayScreenView, ProjectPresentersFactory projectPresentersFactory)
        {
            _gameplayScreenView = gameplayScreenView;
            _projectPresentersFactory = projectPresentersFactory;
        }

        public void Initialize()
        {
            CreateIconTextListPresenter();

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
                _projectPresentersFactory.CreateIconTextListPresenter(_gameplayScreenView.IconTextListView);

            _childPresenters.Add(iconTextListPresenter);
        }
    }
}
