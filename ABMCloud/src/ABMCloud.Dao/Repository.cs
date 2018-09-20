using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ABMCloud.Entites;
using ABMCloud.Dao.Context;

namespace ABMCloud.Dao
{
    public class Repository : IRepository
    {
        public List<CollaboratorInfo> GetCollaborators()
        {
            using (CollaboratorContext db = new CollaboratorContext())
            {
                db.Collaborators.Add(new Entitis.Collaborator() {
                    Name = "Sergey",
                    Surname = "Ilminskyi",
                    Patronymic = "Vladimirovich",
                    Birthday = new DateTime(1992,2,12)
                });
                db.SaveChanges();
                var collaborators = db.Collaborators.ToList();

                return new List<CollaboratorInfo>();
            }
        }
    }
}
