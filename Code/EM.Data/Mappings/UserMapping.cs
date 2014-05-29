using System.Data.Entity.ModelConfiguration;
using EM.Data.Models;

namespace EM.Data.Mappings
{
    public class UserConfiguration:EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            
        }
    }
}