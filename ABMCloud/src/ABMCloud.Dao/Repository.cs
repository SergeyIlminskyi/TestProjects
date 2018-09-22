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
        public long AddEmployee(EmployeeInfo collaborator)
        {
            using (EmployeeContext db = new EmployeeContext())
            {
                var item = db.Employees.Add(new Entitis.Employee()
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

        public List<EmployeeInfo> GetEmployees()
        {
            using (EmployeeContext db = new EmployeeContext())
            {
                var collaborators = new List<EmployeeInfo>();

                foreach(var collaborator in db.Employees.ToList())
                {
                    var item = new EmployeeInfo()
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
