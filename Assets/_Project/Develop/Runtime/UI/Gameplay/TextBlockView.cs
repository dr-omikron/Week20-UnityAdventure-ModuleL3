using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Develop.Runtime.UI.Gameplay
{
    public class TextBlockView : MonoBehaviour, IView
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _background;

        public void SetTitle(string title) => _title.SetText(title);
        public void SetText(string text) => _text.SetText(text);
        public void SetBackgroundColor(Color color) => _background.color = color;
    }
}
