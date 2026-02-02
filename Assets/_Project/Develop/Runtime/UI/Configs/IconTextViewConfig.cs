using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.EnumTypes;
using UnityEngine;

namespace _Project.Develop.Runtime.UI.Configs
{
    [CreateAssetMenu(fileName = "IconTextConfig", menuName = "Configs/UI/IconTextConfig")]
    public class IconTextViewConfig : ScriptableObject
    {
        [SerializeField] private List<IconViewConfig> _iconViewConfigs = new List<IconViewConfig>();

        public Sprite GetSpriteFor(ViewType viewType)
            => _iconViewConfigs.First(config => config.Type == viewType).Sprite;

        [Serializable]
        private class IconViewConfig
        {
            [field: SerializeField] public ViewType Type { get; private set; }
            [field: SerializeField] public Sprite Sprite { get; private set; }
        }
    }
}
