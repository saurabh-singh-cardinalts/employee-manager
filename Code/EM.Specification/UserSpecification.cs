using System;
using EM.Data.Models;
using EM.Framework.Data.Entity;
using EM.Specification.Interfaces;

namespace EM.Specification
{
    public class UserSpecification : QueryableSpecification<User>, IUserSpecification
    {
        public IUserSpecification WithName(string name)
        {
            Predicate = t => t.UserName == name;
            return this;
        }

        public IUserSpecification IncludeMemberShip()
        {
            FetchStrategy.Include(t => t.EMMembership);
            return this;
        }

        public IUserSpecification WithToken(string token)
        {
            Predicate = user => (user.EMMembership.PasswordVerificationToken == token) && DateTime.Compare(user.EMMembership.PasswordVerificationTokenExpirationDate ?? DateTime.UtcNow, DateTime.UtcNow) > 0;
            return this;
        }
    }
}