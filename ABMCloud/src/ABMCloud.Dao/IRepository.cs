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
        List<CollaboratorInfo> GetCollaborators();
    }
}
