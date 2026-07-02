namespace Diamonds.Signals
{
    public class DiamondsSpawnedSignal
    {
        public int Count { get; }

        public DiamondsSpawnedSignal(int count)
        {
            Count = count;
        }
    }
}