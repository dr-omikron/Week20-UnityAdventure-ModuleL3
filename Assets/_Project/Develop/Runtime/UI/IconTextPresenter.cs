using System;
using _Project.Develop.Runtime.EnumTypes;
using _Project.Develop.Runtime.UI.CommonViews;
using _Project.Develop.Runtime.UI.Configs;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.UI
{
    public class IconTextPresenter
    {
        private readonly IReadOnlyVariable<int> _variable;
        private readonly ViewType _viewType;
        private readonly IconTextViewConfig _iconTextViewConfig;
        private readonly IconTextView _view;

        private IDisposable _subscription;

        public IconTextPresenter(IReadOnlyVariable<int> variable, ViewType viewType, IconTextViewConfig iconTextViewConfig, IconTextView view)
        {
            _variable = variable;
            _viewType = viewType;
            _iconTextViewConfig = iconTextViewConfig;
            _view = view;
        }

        public void Enable()
        {
            UpdateValue(_variable.Value);
            _view.SetIcon(_iconTextViewConfig.GetSpriteFor(_viewType));

            _subscription = _variable.Subscribe(OnVariableChanged);
        }

        public void Disable()
        {
            _subscription.Dispose();
        }

        private void OnVariableChanged(int oldValue, int newValue) => UpdateValue(newValue);
        private void UpdateValue(int value) => _view.SetText(value.ToString());
    }
}
