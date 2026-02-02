using System.Collections.Generic;
using _Project.Develop.Runtime.EnumTypes;
using _Project.Develop.Runtime.Gameplay.Features;
using _Project.Develop.Runtime.Meta.Features;
using _Project.Develop.Runtime.UI.CommonViews;
using _Project.Develop.Runtime.Utilities.Factories;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.UI
{
    public class IconTextListPresenter : IPresenter
    {
        private readonly WalletService _walletService;
        private readonly PlayerProgressTracker _playerProgressTracker;
        private readonly ViewsFactory _viewsFactory;
        private readonly ProjectPresentersFactory _projectPresentersFactory;
        private readonly IconTextListView _iconTextListView;

        private readonly List<IconTextPresenter> _iconTextPresenters = new List<IconTextPresenter>();

        public IconTextListPresenter(
            WalletService walletService, 
            PlayerProgressTracker playerProgressTracker, 
            ViewsFactory viewsFactory, 
            ProjectPresentersFactory projectPresentersFactory, 
            IconTextListView iconTextListView)
        {
            _walletService = walletService;
            _playerProgressTracker = playerProgressTracker;
            _viewsFactory = viewsFactory;
            _projectPresentersFactory = projectPresentersFactory;
            _iconTextListView = iconTextListView;
        }

        public void Initialize()
        {
            CreateIconTextElement(ViewType.Gold, _walletService.Gold);
            CreateIconTextElement(ViewType.Wins, _playerProgressTracker.Wins);
            CreateIconTextElement(ViewType.Loss, _playerProgressTracker.Losses);
        }

        public void Dispose()
        {
            foreach (var iconTextPresenter in _iconTextPresenters)
            {
                _iconTextListView.Remove(iconTextPresenter.View);
                _viewsFactory.Release(iconTextPresenter.View);
                iconTextPresenter.Dispose();
            }

            _iconTextPresenters.Clear();
        }

        private void CreateIconTextElement(ViewType viewType, IReadOnlyVariable<int> variable)
        {
            IconTextView iconTextView = _viewsFactory.CreateView<IconTextView>(ViewIDs.IconTextView);
            _iconTextListView.Add(iconTextView);

            IconTextPresenter iconTextPresenter = _projectPresentersFactory.CreateIconTextPresenter(
                iconTextView,
                variable,
                viewType);

            iconTextPresenter.Initialize();
            _iconTextPresenters.Add(iconTextPresenter);
        }
    }
}
