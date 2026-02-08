using _Project.Develop.Runtime.UI.CommonViews;
using UnityEngine;

namespace _Project.Develop.Runtime.UI.Gameplay
{
    public class GameplayScreenView :  MonoBehaviour, IView
    {
        [field: SerializeField] public IconTextListView IconTextListView { get; private set; }
    }
}
