using Vacations_DomainModel.Models;

namespace Vacations_DomainModel
{
    public class Entity<TIdentifier> : IEntity
    {
        public TIdentifier? Id { get; set; }
    }
}
