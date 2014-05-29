using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EM.Data;

namespace EM.DbCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new EMDbInitializer());
            var context = new EMContext();
            context.Database.Initialize(true);
        }
    }
}
