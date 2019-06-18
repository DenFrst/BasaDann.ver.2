using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasaDann.Classes;

namespace BasaDann.Utils
{
    class ContextDataBase : DbContext
    {

        public ContextDataBase() : base("DefaultConnection")
        {

        }

        public DbSet<Phone> Phones { get; set; }

    }
}
