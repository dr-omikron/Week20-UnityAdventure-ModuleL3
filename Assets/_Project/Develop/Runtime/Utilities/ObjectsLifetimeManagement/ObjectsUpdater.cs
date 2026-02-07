using System.Collections.Generic;

namespace _Project.Develop.Runtime.Utilities.ObjectsLifetimeManagement
{
    public class ObjectsUpdater
    {
        private readonly List<IUpdatable> _updatables;

        public ObjectsUpdater(List<IUpdatable> updatables)
        {
            _updatables = new List<IUpdatable>(updatables);
        }

        public void Update(float deltaTime)
        {
            foreach (var updatable in _updatables)
                updatable.Update(deltaTime);
        }
    }
}
