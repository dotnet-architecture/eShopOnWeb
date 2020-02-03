namespace Microsoft.eShopWeb.ApplicationCore.Entities.BuyerAggregate
{
    public class PaymentMethod : BaseEntity
    {
        public string Alias { get; private set; }
        public string CardId { get; private set; } // actual card data must be stored in a PCI compliant system, like Stripe
        public string Last4 { get; private set; }
    }
}
