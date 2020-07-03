using MediatR;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.eShopWeb.ApplicationCore.Exceptions;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Entities
{
    public class ValidateUniqueCatalogItemNameHandler : INotificationHandler<UpdatingNameEvent>
    {
        private readonly IAsyncRepository<CatalogItem> _asyncRepository;

        public ValidateUniqueCatalogItemNameHandler(IAsyncRepository<CatalogItem> asyncRepository)
        {
            _asyncRepository = asyncRepository;
        }

        public async Task Handle(UpdatingNameEvent notification, CancellationToken cancellationToken)
        {
            var allItems = (await _asyncRepository.ListAllAsync()).ToList();

            var duplicateItem = allItems.FirstOrDefault(i => i.Name == notification.NewName && i.Id != notification.Id);

            if(duplicateItem != null)
            {
                throw new DuplicateCatalogItemNameException("Duplicate name not allowed", duplicateItem.Id);
            }
        }
    }
}