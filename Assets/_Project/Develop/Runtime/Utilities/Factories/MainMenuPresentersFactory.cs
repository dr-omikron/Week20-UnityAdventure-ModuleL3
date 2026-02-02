using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.UI.MainMenu;

namespace _Project.Develop.Runtime.Utilities.Factories
{
    public class MainMenuPresentersFactory
    {
        private readonly DIContainer _container;

        public MainMenuPresentersFactory(DIContainer container)
        {
            _container = container;
        }

        public MainMenuScreenPresenter CreateMainMenuScreenPresenter(MainMenuScreenView view)
        {
            return new MainMenuScreenPresenter(view,
                _container.Resolve<ProjectPresentersFactory>());
        }
    }
}
