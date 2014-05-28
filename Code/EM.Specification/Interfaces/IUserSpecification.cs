using EM.Data.Models;
using EM.Framework.Data.Repository;

namespace EM.Specification.Interfaces
{
    public interface IUserSpecification : ISpecification<User>
    {
        IUserSpecification WithName(string name);
        IUserSpecification IncludeMemberShip();
        IUserSpecification WithToken(string token);
    }
}
