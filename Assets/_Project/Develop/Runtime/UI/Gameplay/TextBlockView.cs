using TMPro;
using UnityEngine;

namespace _Project.Develop.Runtime.UI.Gameplay
{
    public class TextBlockView : MonoBehaviour, IView
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _text;

        public void SetTitle(string title) => _title.SetText(title);
        public void SetText(string text) => _text.SetText(text);
    }
}
