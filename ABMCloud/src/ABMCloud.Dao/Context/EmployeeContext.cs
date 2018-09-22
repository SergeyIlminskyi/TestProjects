using System;
using System.Collections.Generic;
using System.Data.Entity;
using ABMCloud.Dao.Entitis;

namespace ABMCloud.Dao.Context
{
    class EmployeeContext : DbContext
    {
        public EmployeeContext() : base("DbConnection") { }

        public DbSet<Employee> Employees { get; set; }
    }
}
