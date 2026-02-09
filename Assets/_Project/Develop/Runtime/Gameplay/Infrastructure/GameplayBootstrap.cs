using System;
using System.Collections;
using _Project.Develop.Runtime.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.CoroutinesManagement;
using _Project.Develop.Runtime.Utilities.Factories;
using _Project.Develop.Runtime.Utilities.SceneManagement;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private GameplayInputArgs _inputArgs;
        private ICoroutinesPerformer _coroutinesPerformer;
        private GameCycle _gameCycle;

        public override void ProcessRegistration(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            if (sceneArgs is not GameplayInputArgs gameplayInputArgs)
                throw new ArgumentException($"{nameof(sceneArgs)} is not match with {typeof(GameplayInputArgs)}");

            _inputArgs = gameplayInputArgs;

            GameplayContextRegistrations.Process(_container, _inputArgs);
        }

        public override IEnumerator Initialize()
        {
            _coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();

            GameCycleFactory gameCycleFactory = _container.Resolve<GameCycleFactory>();
            _gameCycle = gameCycleFactory.Create(_inputArgs);

            yield return null;
        }

        public override void Run()
        {
            _coroutinesPerformer.StartPerform(_gameCycle.Start());
        }
    }
}
