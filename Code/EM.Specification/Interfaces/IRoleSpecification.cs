using EM.Data.Models;
using EM.Framework.Data.Repository;

namespace EM.Specification.Interfaces
{
    public interface IRoleSpecification : ISpecification<Role>
    {
        IRoleSpecification AllRoles();
        IRoleSpecification WithName(string role); 
    }
}