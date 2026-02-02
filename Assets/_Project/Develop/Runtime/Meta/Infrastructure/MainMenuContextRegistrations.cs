using _Project.Develop.Runtime.Gameplay.Features;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Features;
using _Project.Develop.Runtime.UI;
using _Project.Develop.Runtime.UI.MainMenu;
using _Project.Develop.Runtime.Utilities.AssetsManagement;
using _Project.Develop.Runtime.Utilities.ConfigsManagement;
using _Project.Develop.Runtime.Utilities.DataManagement;
using _Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using _Project.Develop.Runtime.Utilities.Factories;
using UnityEngine;

namespace _Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle(CreateMainMenuPlayerInputs);
            container.RegisterAsSingle(CreateSelectGameModeService);
            container.RegisterAsSingle(CreatePlayerProgressPrinter);
            container.RegisterAsSingle(CreatePlayerProgressRemover);
            container.RegisterAsSingle(CreateMetaCycleFactory);
            container.RegisterAsSingle(CreateSceneUIRoot).NonLazy();
            container.RegisterAsSingle(CreateMainMenuPresentersFactory);
            container.RegisterAsSingle(CreateMainMenuScreenPresenter).NonLazy();
        }

        private static MainMenuPlayerInputs CreateMainMenuPlayerInputs(DIContainer c) => new MainMenuPlayerInputs();
        private static SelectGameModeArgsService CreateSelectGameModeService(DIContainer c)
        {
            MainMenuPlayerInputs mainMenuPlayerInputs = c.Resolve<MainMenuPlayerInputs>();
            ConfigsProviderService configsProviderService = c.Resolve<ConfigsProviderService>();

            return new SelectGameModeArgsService(mainMenuPlayerInputs, configsProviderService);
        }

        private static PlayerProgressPrinter CreatePlayerProgressPrinter(DIContainer c)
        {
            PlayerProgressTracker playerProgressTracker = c.Resolve<PlayerProgressTracker>();
            WalletService walletService = c.Resolve<WalletService>();
            MainMenuPlayerInputs mainMenuPlayerInputs = c.Resolve<MainMenuPlayerInputs>();

            return new PlayerProgressPrinter(playerProgressTracker, walletService, mainMenuPlayerInputs);
        }

        private static PlayerProgressRemover CreatePlayerProgressRemover(DIContainer c)
        {
            MainMenuPlayerInputs mainMenuPlayerInputs = c.Resolve<MainMenuPlayerInputs>();
            ConfigsProviderService configsProviderService = c.Resolve<ConfigsProviderService>();
            WalletService walletService = c.Resolve<WalletService>();
            PlayerStatisticProvider playerStatisticProvider = c.Resolve<PlayerStatisticProvider>();
            SaveLoadDataProvidersService saveLoadDataProvidersService = c.Resolve<SaveLoadDataProvidersService>();
            
            return new PlayerProgressRemover(
                mainMenuPlayerInputs,
                configsProviderService,
                walletService,
                playerStatisticProvider,
                saveLoadDataProvidersService);
        }

        private static MetaCycleFactory CreateMetaCycleFactory(DIContainer c) => new MetaCycleFactory(c);

        private static SceneUIRoot CreateSceneUIRoot(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            SceneUIRoot sceneUIRootPrefab = 
                resourcesAssetsLoader.Load<SceneUIRoot>("UI/SceneUIRoot");

            return Object.Instantiate(sceneUIRootPrefab);
        }

        private static MainMenuPresentersFactory CreateMainMenuPresentersFactory(DIContainer c)
            => new MainMenuPresentersFactory(c);

        private static MainMenuScreenPresenter CreateMainMenuScreenPresenter(DIContainer c)
        {
            SceneUIRoot uiRoot = c.Resolve<SceneUIRoot>();

            MainMenuScreenView view = c
                .Resolve<ViewsFactory>()
                .CreateView<MainMenuScreenView>(ViewIDs.MainMenuScreenView, uiRoot.HUDLayer);

            MainMenuScreenPresenter presenter =
                c.Resolve<MainMenuPresentersFactory>().CreateMainMenuScreenPresenter(view);

            return presenter;
        }
    }
}
