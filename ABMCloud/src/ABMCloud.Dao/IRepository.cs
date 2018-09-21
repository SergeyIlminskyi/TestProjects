using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABMCloud.Entites;

namespace ABMCloud.Dao
{
    public interface IRepository
    {
        long AddCollaborator(CollaboratorInfo collaborator);

        List<CollaboratorInfo> GetCollaborators();
    }
}
