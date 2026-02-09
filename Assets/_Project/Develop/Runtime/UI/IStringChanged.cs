using System;

namespace _Project.Develop.Runtime.UI
{
    public interface IStringChanged
    {
        public event Action<string> Changed;
    }
}
