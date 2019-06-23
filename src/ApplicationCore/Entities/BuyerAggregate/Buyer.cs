using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using System.Collections.Generic;
using Microsoft.eShopWeb.ApplicationCore.Attributes;

namespace Microsoft.eShopWeb.ApplicationCore.Entities.BuyerAggregate
{
    [AggregateRoot]
    public class Buyer : BaseEntity
    {
        public string IdentityGuid { get; private set; }

        private List<PaymentMethod> _paymentMethods = new List<PaymentMethod>();

        public IEnumerable<PaymentMethod> PaymentMethods => _paymentMethods.AsReadOnly();

        private Buyer()
        {
            // required by EF
        }

        public Buyer(string identity) : this()
        {
            Guard.Against.NullOrEmpty(identity, nameof(identity));
            IdentityGuid = identity;
        }
    }
}
