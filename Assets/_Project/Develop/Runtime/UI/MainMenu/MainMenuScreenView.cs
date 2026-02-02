using _Project.Develop.Runtime.UI.CommonViews;
using UnityEngine;

namespace _Project.Develop.Runtime.UI.MainMenu
{
    public class MainMenuScreenView : MonoBehaviour, IView
    {
        [field: SerializeField] public IconTextListView IconTextListView { get; private set; }
    }
}
