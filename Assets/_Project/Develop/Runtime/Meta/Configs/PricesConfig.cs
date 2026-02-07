using UnityEngine;

namespace _Project.Develop.Runtime.Meta.Configs
{
    [CreateAssetMenu(fileName = "PricesConfig", menuName = "Configs/Gameplay/PricesConfig")]
    public class PricesConfig : ScriptableObject
    {
        [field: SerializeField] public int ResetPrice { get; private set; }
    }
}
