using System.Collections;
using _Project.Develop.Runtime.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.UI;
using _Project.Develop.Runtime.UI.CommonViews;
using _Project.Develop.Runtime.Utilities.Factories;
using _Project.Develop.Runtime.Utilities.ObjectsLifetimeManagement;
using _Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace _Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private ObjectsUpdater _objectsUpdater;
        private MainMenuPlayerInputs _mainMenuPlayerInputs;
        private MetaCycle _metaCycle;

        [SerializeField] private IconTextListView _iconTextListView;
        private ProjectPresentersFactory _projectPresentersFactory;
        private bool _isRun;

        public override void ProcessRegistration(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;
            MainMenuContextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            _objectsUpdater = _container.Resolve<ObjectsUpdater>();
            _mainMenuPlayerInputs = _container.Resolve<MainMenuPlayerInputs>();

            _objectsUpdater.Add(_mainMenuPlayerInputs);

            MetaCycleFactory factory = _container.Resolve<MetaCycleFactory>();
            _metaCycle = factory.Create();

            _projectPresentersFactory = _container.Resolve<ProjectPresentersFactory>();

            IconTextListPresenter iconTextListPresenter =
                _projectPresentersFactory.CreateIconTextListPresenter(_iconTextListView);

            iconTextListPresenter.Enable();

            yield return null;
        }

        public override void Run()
        {
            _isRun = true;
            _metaCycle.Start();
        }

        private void Update()
        {
            if (_isRun == false)
                return;

            _objectsUpdater.Update(Time.deltaTime);
        }

        private void OnDestroy()
        {
            _metaCycle.Deinitialize();
        }
    }
}
