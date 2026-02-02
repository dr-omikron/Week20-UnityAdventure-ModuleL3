using _Project.Develop.Runtime.Utilities.DataManagement;
using _Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Gameplay.Features
{
    public class PlayerProgressTracker : IDataReader<PlayerStatistic>, IDataWriter<PlayerStatistic>
    {
        private readonly ReactiveVariable<int> _wins = new ReactiveVariable<int>(0);
        private readonly ReactiveVariable<int> _loses = new ReactiveVariable<int>(0);

        public PlayerProgressTracker(PlayerStatisticProvider playerStatisticProvider)
        {
            playerStatisticProvider.RegisterReader(this);
            playerStatisticProvider.RegisterWriter(this);
        }
        public IReadOnlyVariable<int> Wins => _wins;
        public IReadOnlyVariable<int> Losses => _loses;

        public void AddWin() => _wins.Value++;
        public void AddLoss() => _loses.Value++;

        public void ReadFrom(PlayerStatistic data)
        {
            _wins.Value = data.Wins;
            _loses.Value = data.Losses;
        }

        public void WriteTo(PlayerStatistic data)
        {
            data.Wins = _wins.Value;
            data.Losses = _loses.Value;
        }
    }
}
