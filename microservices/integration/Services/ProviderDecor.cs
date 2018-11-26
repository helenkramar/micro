namespace integration.Services
{
    using modeling.Builders;
    using modeling.Pact;

    public class ProviderDecor : BaseMock
    {
        public ProviderDecor(Pact pact) : base(pact)
        {
        }

    }
}