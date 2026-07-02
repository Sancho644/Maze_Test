using Core;

namespace Diamonds.Signals
{
    public class DiamondsCountChangedSignal
    {
        public CollectibleProgress Progress { get; }

        public DiamondsCountChangedSignal(CollectibleProgress progress)
        {
            Progress = progress;
        }
    }
}