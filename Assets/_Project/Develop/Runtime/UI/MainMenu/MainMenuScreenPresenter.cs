using System.Collections.Generic;
using System.Text;
using _Project.Develop.Runtime.Meta.Configs;
using _Project.Develop.Runtime.UI.MainMenu;
using _Project.Develop.Runtime.Utilities.ConfigsManagement;
using _Project.Develop.Runtime.Utilities.Factories;
using _Project.Develop.Runtime.Utilities.PlayerInput;

namespace _Project.Develop.Runtime.UI.CommonViews
{
    public class MainMenuScreenPresenter : IPresenter
    {
        private readonly MainMenuScreenView _screenView;
        private readonly ProjectPresentersFactory _projectPresentersFactory;
        private readonly ConfigsProviderService _configsProviderService;
        private readonly ProjectPopupService _popupService;

        private readonly List<IPresenter> _childPresenters = new List<IPresenter>();

        public MainMenuScreenPresenter(
            MainMenuScreenView screenView, 
            ProjectPresentersFactory projectPresentersFactory, 
            ConfigsProviderService configsProviderService, 
            ProjectPopupService popupService)
        {
            _screenView = screenView;
            _projectPresentersFactory = projectPresentersFactory;
            _configsProviderService = configsProviderService;
            _popupService = popupService;
        }

        public void Initialize()
        {
            CreateIconTextListPresenter();

            string tutorialMessage = CreateTutorialMessage();
            _screenView.SetTutorialText(tutorialMessage);

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

        private string CreateTutorialMessage()
        {
            StringBuilder sb = new StringBuilder();
            PricesConfig pricesConfig = _configsProviderService.GetConfig<PricesConfig>();

            sb.AppendLine($"Generate numbers: Press Button { KeyboardInputKeys.LoadNumbersModeKey }");
            sb.AppendLine($"Generate character: Press Button { KeyboardInputKeys.LoadCharactersModeKey }");
            sb.AppendLine($"Reset game progress: {pricesConfig.ResetPrice} gold");

            return sb.ToString();
        }
    }
}
