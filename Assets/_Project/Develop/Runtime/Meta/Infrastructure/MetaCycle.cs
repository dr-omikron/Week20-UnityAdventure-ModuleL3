using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.Configs;
using _Project.Develop.Runtime.Gameplay.Infrastructure;
using _Project.Develop.Runtime.Meta.Configs;
using _Project.Develop.Runtime.Meta.Features;
using _Project.Develop.Runtime.Utilities.ConfigsManagement;
using _Project.Develop.Runtime.Utilities.CoroutinesManagement;
using _Project.Develop.Runtime.Utilities.PlayerInput;
using _Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace _Project.Develop.Runtime.Meta.Infrastructure
{
    public class MetaCycle : IDisposable
    {
        private readonly SelectGameModeArgsService _selectGameModeArgsService;
        private readonly ConfigsProviderService _configsProviderService;
        private readonly SceneSwitcherService _sceneSwitcherService;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly PlayerProgressPrinter _playerProgressPrinter;
        private readonly PlayerProgressRemover _playerProgressRemover;


        public MetaCycle(
            SelectGameModeArgsService selectGameModeArgsService, 
            PlayerProgressPrinter playerProgressPrinter, 
            PlayerProgressRemover playerProgressRemover, 
            ConfigsProviderService configsProviderService, 
            SceneSwitcherService sceneSwitcherService, 
            ICoroutinesPerformer coroutinesPerformer)
        {
            _selectGameModeArgsService = selectGameModeArgsService;
            _configsProviderService = configsProviderService;
            _sceneSwitcherService = sceneSwitcherService;
            _coroutinesPerformer = coroutinesPerformer;
            _playerProgressPrinter = playerProgressPrinter;
            _playerProgressRemover = playerProgressRemover;

            _selectGameModeArgsService.GameModeSelected += OnSelectedGameModeArgs;
        }

        public void Start()
        {
            PricesConfig pricesConfig = _configsProviderService.GetConfig<PricesConfig>();
            
            Debug.Log($"Выбрать режим игры: {KeyboardInputKeys.LoadNumbersModeKey} - сгенерировать цифры, " +
                      $"{KeyboardInputKeys.LoadCharactersModeKey} - сгенерировать буквы, " +
                      $"{KeyboardInputKeys.ShowInfoKey} - показать прогресс.");

            Debug.Log($"{KeyboardInputKeys.ResetProgressKey} - Сбросить прогресс за {pricesConfig.ResetPrice} золота.");
        }

        private void OnSelectedGameModeArgs(GameplayInputArgs args) 
            => _coroutinesPerformer.StartPerform(_sceneSwitcherService.ProcessSwitchTo(Scenes.Gameplay, args));

        public void Dispose()
        {
            _selectGameModeArgsService.GameModeSelected -= OnSelectedGameModeArgs;
        }
    }
}