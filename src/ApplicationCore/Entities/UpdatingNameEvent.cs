using MediatR;

namespace Microsoft.eShopWeb.ApplicationCore.Entities
{
    public class UpdatingNameEvent : INotification
    {
        public UpdatingNameEvent(int id, string newName)
        {
            Id = id;
            NewName = newName;
        }

        public int Id { get; }
        public string NewName { get; }
    }
}