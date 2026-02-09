using System.Collections.Generic;
using _Project.Develop.Runtime.Utilities.Factories;

namespace _Project.Develop.Runtime.UI.CommonViews
{
    public class ScreenPresenter : IPresenter
    {
        private readonly IIconTextListScreenView _screenView;
        private readonly ProjectPresentersFactory _projectPresentersFactory;

        private readonly List<IPresenter> _childPresenters = new List<IPresenter>();

        public ScreenPresenter(IIconTextListScreenView screenView, ProjectPresentersFactory projectPresentersFactory)
        {
            _screenView = screenView;
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
                _projectPresentersFactory.CreateIconTextListPresenter(_screenView.IconTextListView);

            _childPresenters.Add(iconTextListPresenter);
        }
    }
}
