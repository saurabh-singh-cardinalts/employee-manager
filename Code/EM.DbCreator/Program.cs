using System;
using System.Collections.Generic;
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
            new EMDbInitializer().InitializeDatabase(new EMContext());
        }
    }
}
