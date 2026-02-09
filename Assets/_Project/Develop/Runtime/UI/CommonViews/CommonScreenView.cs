using UnityEngine;

namespace _Project.Develop.Runtime.UI.CommonViews
{
    public class CommonScreenView : MonoBehaviour, IIconTextListScreenView
    {
        [field: SerializeField] public IconTextListView IconTextListView { get; private set; }
    }
}
