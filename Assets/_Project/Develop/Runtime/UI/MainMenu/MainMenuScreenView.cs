using System;
using _Project.Develop.Runtime.UI.CommonViews;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Develop.Runtime.UI.MainMenu
{
    public class MainMenuScreenView : MonoBehaviour, IView
    {
        public event Action ResetProgressButtonClicked; 

        [field: SerializeField] public IconTextListView IconTextListView { get; private set; }
        [SerializeField] private Button _resetProgressButton;
        [SerializeField] private TMP_Text _tutorialText;

        public void SetTutorialText(string text) => _tutorialText.text = text;

        private void OnEnable()
        {
            _resetProgressButton.onClick.AddListener(OnResetProgressButtonClicked);
        }

        private void OnDisable()
        {
            _resetProgressButton.onClick.RemoveListener(OnResetProgressButtonClicked);
        }

        private void OnResetProgressButtonClicked() => ResetProgressButtonClicked?.Invoke();
    }
}
