using _Project.Develop.Runtime.EnumTypes;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.UI;
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
    }
}
