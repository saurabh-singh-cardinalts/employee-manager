using EM.Data.Models;
using EM.Framework.Data.Entity;
using EM.Specification.Interfaces;

namespace EM.Specification
{
    public class UserSpecification : QueryableSpecification<User>, IUserSpecification
    {
        public IUserSpecification WithName(string name)
        {
            return this;
        }

        public IUserSpecification IncludeMemberShip()
        {
            return this;
        }

        public IUserSpecification WithToken(string token)
        {
            return this;
        }
    }
}