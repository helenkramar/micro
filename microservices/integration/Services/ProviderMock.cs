namespace integration.Services
{
    using modeling.Builders;
    using modeling.Pact;
    using Pact;

    public class ProviderMock : BaseMock
    {
        public ProviderMock(ConsumerPact pact) : base((Pact)pact)
        {
        }

    }
}