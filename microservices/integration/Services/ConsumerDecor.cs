namespace integration.Services
{
    using modeling.Builders;
    using modeling.Pact;

    public class ConsumerDecor : BaseMock
    {
        public ConsumerDecor(Pact pact) : base(pact)
        {
        }

    }
}