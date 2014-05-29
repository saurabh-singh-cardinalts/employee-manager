using System;

namespace EM.Data.Models
{
    public class Role
    {
        public Roles Value { get; set; }
        public string RoleName
        {
            get { return Value.ToString(); }
            set
            {
                Roles role;
                if (Enum.TryParse(value, true, out role))
                    Value = role;
            }
        }


    }
}