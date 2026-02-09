using _Project.Develop.Runtime.Meta.Configs;
using _Project.Develop.Runtime.Utilities.ConfigsManagement;
using _Project.Develop.Runtime.Utilities.DataManagement;
using _Project.Develop.Runtime.Utilities.DataManagement.DataProviders;

namespace _Project.Develop.Runtime.Meta.Features
{
    public class PlayerProgressRemover
    {
        private readonly PlayerStatisticProvider _playerStatisticProvider;
        private readonly WalletService _walletService;
        private readonly PricesConfig _pricesConfig;
        private readonly SaveLoadDataProvidersService _saveLoadDataProvidersService;

        public PlayerProgressRemover(
            ConfigsProviderService configsProviderService,
            WalletService walletService,
            PlayerStatisticProvider playerStatisticProvider,
            SaveLoadDataProvidersService saveLoadDataProvidersService) 
        {
            _playerStatisticProvider = playerStatisticProvider;
            _walletService = walletService;
            _saveLoadDataProvidersService = saveLoadDataProvidersService;
            _pricesConfig = configsProviderService.GetConfig<PricesConfig>();
        }

        public bool TryRemove(out string resultMessage)
        {
            if(_playerStatisticProvider.IsNotZeroProgress() == false)
            {
                resultMessage = "Progress has already been reset";
                return false;
            }

            if (_walletService.Enough(_pricesConfig.ResetPrice))
            {
                _walletService.Spend(_pricesConfig.ResetPrice);
                _playerStatisticProvider.Reset();
                _saveLoadDataProvidersService.SaveAll();

                resultMessage = $"Progress successfully reset { _pricesConfig.ResetPrice } gold debited from wallet. Balance - { _walletService.Gold.Value }";
                return true;
            }

            resultMessage = $"{_walletService.Gold.Value} gold is not enough to reset progress.";
            return false;
        }
    }
}
