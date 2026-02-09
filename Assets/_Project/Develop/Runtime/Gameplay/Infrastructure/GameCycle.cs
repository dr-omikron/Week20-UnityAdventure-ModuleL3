using System;
using System.Collections;
using _Project.Develop.Runtime.Gameplay.Configs;
using _Project.Develop.Runtime.Gameplay.Features;
using _Project.Develop.Runtime.Gameplay.Inputs;
using _Project.Develop.Runtime.Meta.Features;
using _Project.Develop.Runtime.UI;
using _Project.Develop.Runtime.Utilities.CoroutinesManagement;
using _Project.Develop.Runtime.Utilities.DataManagement;
using _Project.Develop.Runtime.Utilities.SceneManagement;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameCycle
    {
        private readonly PlayerProgressTracker _playerProgressTracker;
        private readonly LevelConfig _levelConfig;
        private readonly WalletService _walletService;
        private readonly SymbolsSequenceGenerator _symbolsSequenceGenerator;
        private readonly InputStringReader _inputStringReader;
        private readonly SceneSwitcherService _sceneSwitcherService;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly SaveLoadDataProvidersService _saveLoadDataProvidersService;
        private readonly GameplayInputArgs _inputArgs;
        private readonly ProjectPopupService _popupService;

        public GameCycle(
            PlayerProgressTracker playerProgressTracker,
            LevelConfig levelConfig,
            WalletService walletService,
            SymbolsSequenceGenerator symbolsSequenceGenerator,
            InputStringReader inputStringReader,
            SceneSwitcherService sceneSwitcherService,
            ICoroutinesPerformer coroutinesPerformer,
            SaveLoadDataProvidersService saveLoadDataProvidersService,
            ProjectPopupService popupService,
            GameplayInputArgs inputArgs)
        {
            _playerProgressTracker = playerProgressTracker;
            _levelConfig = levelConfig;
            _walletService = walletService;
            _symbolsSequenceGenerator = symbolsSequenceGenerator;
            _inputStringReader = inputStringReader;
            _sceneSwitcherService = sceneSwitcherService;
            _coroutinesPerformer = coroutinesPerformer;
            _saveLoadDataProvidersService = saveLoadDataProvidersService;
            _popupService = popupService;
            _inputArgs = inputArgs;
        }

        public IEnumerator Start()
        {
            string generated = _symbolsSequenceGenerator.Generate(_inputArgs.Symbols, _inputArgs.SequenceLenght);

            yield return _coroutinesPerformer.StartPerform(_inputStringReader.StartProcess(_inputArgs.SequenceLenght));

            if (string.Equals(_inputStringReader.CurrentInput, generated, StringComparison.OrdinalIgnoreCase))
                ProcessWin();
            else
                ProcessDefeat();

            _saveLoadDataProvidersService.SaveAll();
        }

        private void ProcessWin()
        {
            _popupService.OpenInfoPopup("You win", OnMainMenuReturn);

            _playerProgressTracker.AddWin();
            _walletService.Add(_levelConfig.WinGoldAmount);
        }

        private void ProcessDefeat()
        {
            _popupService.OpenInfoPopup("You lose", OnRestartGame);

            _playerProgressTracker.AddLoss();

            if(_walletService.Enough(_levelConfig.DefeatGoldAmount))
                _walletService.Spend(_levelConfig.DefeatGoldAmount);
            else
                _walletService.Reset();
        }

        private void OnMainMenuReturn()
        {
            _coroutinesPerformer.StartPerform(ReturnToMainMenu());
        }

        private void OnRestartGame()
        {
            _coroutinesPerformer.StartPerform(Start());
        }

        private IEnumerator ReturnToMainMenu()
        {
            yield return _sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu);
        }
    }
}
