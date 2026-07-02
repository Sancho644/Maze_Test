namespace Core
{
    public readonly struct CollectibleProgress
    {
        public int Collected { get; }
        public int Total { get; }

        public CollectibleProgress(int collected, int total)
        {
            Collected = collected;
            Total = total;
        }
    }
}