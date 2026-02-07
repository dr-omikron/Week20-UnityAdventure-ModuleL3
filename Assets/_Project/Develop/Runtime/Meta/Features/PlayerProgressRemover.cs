using System;
using _Project.Develop.Runtime.Meta.Configs;
using _Project.Develop.Runtime.Meta.Infrastructure;
using _Project.Develop.Runtime.Utilities.ConfigsManagement;
using _Project.Develop.Runtime.Utilities.DataManagement;
using _Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using UnityEngine;

namespace _Project.Develop.Runtime.Meta.Features
{
    public class PlayerProgressRemover : IDisposable
    {
        private readonly PlayerStatisticProvider _playerStatisticProvider;
        private readonly WalletService _walletService;
        private readonly MainMenuPlayerInputs _mainMenuPlayerInputs;
        private readonly PricesConfig _pricesConfig;
        private readonly SaveLoadDataProvidersService _saveLoadDataProvidersService;

        public PlayerProgressRemover(
            MainMenuPlayerInputs mainMenuPlayerInputs,
            ConfigsProviderService configsProviderService,
            WalletService walletService,
            PlayerStatisticProvider playerStatisticProvider,
            SaveLoadDataProvidersService saveLoadDataProvidersService) 
        {
            _mainMenuPlayerInputs = mainMenuPlayerInputs;
            _playerStatisticProvider = playerStatisticProvider;
            _walletService = walletService;
            _saveLoadDataProvidersService = saveLoadDataProvidersService;
            _pricesConfig = configsProviderService.GetConfig<PricesConfig>();

            _mainMenuPlayerInputs.ResetProgressKeyDown += Remove;
        }

        private void Remove()
        {
            if(_playerStatisticProvider.IsNotZeroProgress() == false)
            {
                Debug.Log("Прогресс уже сброшен");
                return;
            }

            if (_walletService.Enough(_pricesConfig.ResetPrice))
            {
                _walletService.Spend(_pricesConfig.ResetPrice);
                _playerStatisticProvider.Reset();
                _saveLoadDataProvidersService.SaveAll();

                Debug.Log($"Прогресс успешно сброшен, с кошелька списано { _pricesConfig.ResetPrice } золота. Баланс - { _walletService.Gold.Value }");
            }
            else
            {
                Debug.Log($"{_walletService.Gold.Value} золота не достаточно для сброса прогресса.");
            }
        }

        public void Dispose() => _mainMenuPlayerInputs.ResetProgressKeyDown -= Remove;
    }
}
