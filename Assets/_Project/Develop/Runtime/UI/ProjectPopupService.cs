using _Project.Develop.Runtime.Utilities.Factories;
using UnityEngine;

namespace _Project.Develop.Runtime.UI
{
    public class ProjectPopupService : PopupsService
    {
        private readonly SceneUIRoot _sceneUIRoot;

        public ProjectPopupService(ViewsFactory viewsFactory, ProjectPresentersFactory projectPresentersFactory, SceneUIRoot sceneUIRoot) 
            : base(viewsFactory, projectPresentersFactory)
        {
            _sceneUIRoot = sceneUIRoot;
        }

        protected override Transform PopupsLayer => _sceneUIRoot.PopupsLayer;
    }
}
