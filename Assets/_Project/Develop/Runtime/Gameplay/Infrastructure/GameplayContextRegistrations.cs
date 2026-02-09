using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.Features;
using _Project.Develop.Runtime.Gameplay.Inputs;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.UI;
using _Project.Develop.Runtime.UI.Gameplay;
using _Project.Develop.Runtime.Utilities.AssetsManagement;
using _Project.Develop.Runtime.Utilities.CoroutinesManagement;
using _Project.Develop.Runtime.Utilities.Factories;
using _Project.Develop.Runtime.Utilities.ObjectsLifetimeManagement;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayContextRegistrations
    {
        public static void Process(DIContainer container, GameplayInputArgs gameplayInputArgs)
        {
            container.RegisterAsSingle(CreateGameplayPlayerInputs);
            container.RegisterAsSingle(CreateSymbolsSequenceGenerator);
            container.RegisterAsSingle(CreateObjectsUpdater);
            container.RegisterAsSingle(CreateInputStringReader);
            container.RegisterAsSingle(CreateGameCycleFactory);
            container.RegisterAsSingle(CreatePopupService);
            container.RegisterAsSingle(CreateGameplayPresentersFactory);
            container.RegisterAsSingle(CreateGameplayScreenPresenter).NonLazy();
            container.RegisterAsSingle(CreateSceneUIRoot).NonLazy();
        }

        private static GameplayPlayerInputs CreateGameplayPlayerInputs(DIContainer c) => new GameplayPlayerInputs();
        private static SymbolsSequenceGenerator CreateSymbolsSequenceGenerator(DIContainer c) => new SymbolsSequenceGenerator();
        private static ObjectsUpdater CreateObjectsUpdater(DIContainer c)
        {
            List<IUpdatable> updatables = new List<IUpdatable>();
            GameplayPlayerInputs inputs = c.Resolve<GameplayPlayerInputs>();
            updatables.Add(inputs);

            return new ObjectsUpdater(updatables);
        }

        private static InputStringReader CreateInputStringReader(DIContainer c)
        {
            ICoroutinesPerformer coroutinesPerformer = c.Resolve<ICoroutinesPerformer>();
            return new InputStringReader(coroutinesPerformer);
        }

        private static GameCycleFactory CreateGameCycleFactory(DIContainer c) => new GameCycleFactory(c);


        private static SceneUIRoot CreateSceneUIRoot(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            SceneUIRoot sceneUIRootPrefab = 
                resourcesAssetsLoader.Load<SceneUIRoot>("UI/SceneUIRoot");

            return Object.Instantiate(sceneUIRootPrefab);
        }
        
        private static GameplayPresentersFactory CreateGameplayPresentersFactory(DIContainer c)
            => new GameplayPresentersFactory(c);

        private static GameplayScreenPresenter CreateGameplayScreenPresenter(DIContainer c)
        {
            SceneUIRoot uiRoot = c.Resolve<SceneUIRoot>();

            GameplayScreenView view = c
                .Resolve<ViewsFactory>()
                .CreateView<GameplayScreenView>(ViewIDs.GameplayScreenView, uiRoot.HUDLayer);

            GameplayScreenPresenter presenter =
                c.Resolve<GameplayPresentersFactory>().CreateGameplayScreenPresenter(view);

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
