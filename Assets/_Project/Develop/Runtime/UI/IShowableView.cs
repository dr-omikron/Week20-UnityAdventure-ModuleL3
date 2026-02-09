using DG.Tweening;

namespace _Project.Develop.Runtime.UI
{
    public interface IShowableView : IView
    {
        Tween Show();
        Tween Hide();
    }
}
