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
        public long AddCollaborator(CollaboratorInfo collaborator)
        {
            using (CollaboratorContext db = new CollaboratorContext())
            {
                var item = db.Collaborators.Add(new Entitis.Collaborator()
                {
                    Name = collaborator.Name,
                    Surname = collaborator.Surname,
                    Patronymic = collaborator.Patronymic,
                    Birthday = collaborator.Birthday,
                    CreatedOn = DateTime.Now
                });

                db.SaveChanges();

                return item.Id;
            }
        }

        public List<CollaboratorInfo> GetCollaborators()
        {
            using (CollaboratorContext db = new CollaboratorContext())
            {
                var collaborators = new List<CollaboratorInfo>();

                foreach(var collaborator in db.Collaborators.ToList())
                {
                    var item = new CollaboratorInfo()
                    {
                        Id = collaborator.Id,
                        Name = collaborator.Name,
                        Surname = collaborator.Surname,
                        Patronymic = collaborator.Patronymic,
                        Birthday = collaborator.Birthday,
                        LastVacationDate = DateTime.Now
                    };

                    collaborators.Add(item);
                }
                return collaborators;
            }
        }
    }
}
