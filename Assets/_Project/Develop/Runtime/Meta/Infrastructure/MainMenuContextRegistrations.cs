using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.Features;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Features;
using _Project.Develop.Runtime.UI;
using _Project.Develop.Runtime.UI.CommonViews;
using _Project.Develop.Runtime.UI.MainMenu;
using _Project.Develop.Runtime.Utilities.AssetsManagement;
using _Project.Develop.Runtime.Utilities.ConfigsManagement;
using _Project.Develop.Runtime.Utilities.DataManagement;
using _Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using _Project.Develop.Runtime.Utilities.Factories;
using _Project.Develop.Runtime.Utilities.ObjectsLifetimeManagement;
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
            container.RegisterAsSingle(CreateObjectsUpdater);
            container.RegisterAsSingle(CreateMetaCycleFactory);
            container.RegisterAsSingle(CreatePopupService);
            container.RegisterAsSingle(CreateScreenPresenter).NonLazy();
            container.RegisterAsSingle(CreateSceneUIRoot).NonLazy();
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

        private static ObjectsUpdater CreateObjectsUpdater(DIContainer c)
        {
            List<IUpdatable> updatables = new List<IUpdatable>();
            MainMenuPlayerInputs inputs = c.Resolve<MainMenuPlayerInputs>();
            updatables.Add(inputs);

            return new ObjectsUpdater(updatables);
        }

        private static ScreenPresenter CreateScreenPresenter(DIContainer c)
        {
            SceneUIRoot uiRoot = c.Resolve<SceneUIRoot>();

            CommonScreenView view = c
                .Resolve<ViewsFactory>()
                .CreateView<CommonScreenView>(ViewIDs.MainMenuScreenView, uiRoot.HUDLayer);

            ScreenPresenter presenter =
                c.Resolve<ProjectPresentersFactory>().CreateScreenPresenter(view);

            return presenter;
        }

        private static ProjectPopupService CreatePopupService(DIContainer c)
        {
            return new ProjectPopupService(
                c.Resolve<ViewsFactory>(),
                c.Resolve<ProjectPresentersFactory>(),
                c.Resolve<SceneUIRoot>());
        }
    }
}
