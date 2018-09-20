using System;
using System.Collections.Generic;
using System.Data.Entity;
using ABMCloud.Dao.Entitis;

namespace ABMCloud.Dao.Context
{
    class CollaboratorContext : DbContext
    {
        public CollaboratorContext() : base("DbConnection") { }

        public DbSet<Collaborator> Collaborators { get; set; }
    }
}
