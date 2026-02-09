using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Develop.Runtime.UI
{
    public abstract class PopupViewBase : MonoBehaviour, IShowableView
    {
        public event Action CloseRequested;

        [SerializeField] private CanvasGroup _mainGroup;
        [SerializeField] private Image _antiClicker;
        [SerializeField] private Transform _body;

        private Tween _currentAnimation;

        private void Awake()
        {
            _mainGroup.alpha = 0;
        }

        public void OnCloseButtonClicked() => CloseRequested?.Invoke();

        public Tween Show()
        {
            KillCurrentAnimation();

            OnPreShow();

            _mainGroup.alpha = 1;

            Sequence sequence = DOTween.Sequence();
            
            sequence.Append(_antiClicker
                .DOFade(0.75f, 0.2f)
                .From(0))
                .Join(_body
                .DOScale(1, 0.5f)
                .From(0)
                .SetEase(Ease.OutBack));

            sequence.OnComplete(OnPostShow);
            return _currentAnimation = sequence.Play();
        }

        public Tween Hide()
        {
            KillCurrentAnimation();
            OnPreHide();

            Sequence sequence = DOTween.Sequence();
            sequence.OnComplete(OnPostHide);

            return _currentAnimation = sequence.Play();
        }

        protected virtual void OnPreShow() { }
        protected virtual void OnPostShow() { }
        protected virtual void OnPreHide() { }
        protected virtual void OnPostHide() { }

        private void OnDestroy() => KillCurrentAnimation();

        private void KillCurrentAnimation()
        {
            if (_currentAnimation != null)
                _currentAnimation.Kill();
        }
    }
}
