using EM.Framework.Data.Repository;
using EM.Model;

namespace EM.Specification.Interfaces
{
    public interface IRoleSpecification : ISpecification<Role>
    {
        IRoleSpecification AllRoles();
        IRoleSpecification WithName(string role); 
    }
}