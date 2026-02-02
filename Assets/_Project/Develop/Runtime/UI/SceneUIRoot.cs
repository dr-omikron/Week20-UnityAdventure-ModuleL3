using UnityEngine;

namespace _Project.Develop.Runtime.UI
{
    public class SceneUIRoot : MonoBehaviour
    {
        [field: SerializeField] public Transform HUDLayer { get ; private set; }
        [field: SerializeField] public Transform PopupsLayer { get ; private set; }
        [field: SerializeField] public Transform VFXUnderPopupLayer { get ; private set; }
        [field: SerializeField] public Transform VFXOverPopupLayer { get ; private set; }
    }
}
