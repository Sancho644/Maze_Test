namespace Diamonds.Signals
{
    public class DiamondCollectedSignal
    {
        public Diamond Diamond { get; }

        public DiamondCollectedSignal(Diamond diamond)
        {
            Diamond = diamond;
        }
    }
}