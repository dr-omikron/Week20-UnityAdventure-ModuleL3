using _Project.Develop.Runtime.EnumTypes;
using _Project.Develop.Runtime.Gameplay.Features;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Features;
using _Project.Develop.Runtime.UI.CommonViews;
using _Project.Develop.Runtime.UI.Configs;
using _Project.Develop.Runtime.Utilities.ConfigsManagement;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Utilities.Factories
{
    public class ProjectPresentersFactory
    {
        private readonly DIContainer _container;

        public ProjectPresentersFactory(DIContainer container)
        {
            _container = container;
        }

        public IconTextPresenter CreateIconTextPresenter(IconTextView view, IReadOnlyVariable<int> variable, ViewType viewType)
        {
            return new IconTextPresenter(
                variable, 
                viewType, 
                _container.Resolve<ConfigsProviderService>().GetConfig<IconTextViewConfig>(), 
                view);
        }

        public IconTextListPresenter CreateIconTextListPresenter(IconTextListView view)
        {
            return new IconTextListPresenter(
                _container.Resolve<WalletService>(),
                _container.Resolve<PlayerProgressTracker>(),
                _container.Resolve<ViewsFactory>(),
                this,
                view);
        }

        public InfoPopupPresenter CreateInfoPopupPresenter(InfoPopupView view, string infoText)
        {
            return new InfoPopupPresenter(view, infoText);
        }
    }
}
