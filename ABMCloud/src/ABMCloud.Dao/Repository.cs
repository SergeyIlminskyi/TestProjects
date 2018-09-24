using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using ABMCloud.Entities;
using ABMCloud.Dao.Context;
using ABMCloud.Dao.Entitis;

namespace ABMCloud.Dao
{
    public class Repository : IRepository
    {
        public int AddEmployee(EmployeeInfo employee)
        {
            using (EmployeeContext db = new EmployeeContext())
            {
                var item = db.Employees.Add(new Entitis.Employee()
                {
                    Name = employee.Name,
                    Surname = employee.Surname,
                    Patronymic = employee.Patronymic,
                    Birthday = employee.Birthday,
                    CreatedOn = DateTime.Now
                });

                db.SaveChanges();

                return item.Id;
            }
        }

        public void EditEmployee(EmployeeInfo employee)
        {
            using (EmployeeContext db = new EmployeeContext())
            {
                var e = db.Employees.FirstOrDefault(x => x.Id == employee.Id);

                e.Surname = employee.Surname;
                e.Name = employee.Name;
                e.Patronymic = employee.Patronymic;
                e.Birthday = employee.Birthday;
                e.ModifiedOn = DateTime.Now;

                db.SaveChanges();
            }
        }
        public List<EmployeeInfo> GetEmployees()
        {
            using (EmployeeContext db = new EmployeeContext())
            {
                var employees = new List<EmployeeInfo>();
                var tempEmployees = db.Employees.ToList();
                foreach (var employee in tempEmployees)
                {
                    DateTime? lastDate = null;

                    if (db.EmployeesVacations.Any(x => x.Vacationist.Id == employee.Id))
                        lastDate = db.EmployeesVacations.Where(x => x.Vacationist.Id == employee.Id).Max(x => x.EndDate);

                    var item = new EmployeeInfo()
                    {
                        Id = employee.Id,
                        Name = employee.Name,
                        Surname = employee.Surname,
                        Patronymic = employee.Patronymic,
                        Birthday = employee.Birthday,
                        LastVacationDate = lastDate
                    };

                    employees.Add(item);
                }
                return employees;
            }
        }

        public EmployeeInfo GetEmployeeDetailsById(int id)
        {
            using (EmployeeContext db = new EmployeeContext())
            {
                var employee = db.Employees.FirstOrDefault(x => x.Id == id);

                DateTime? lastDate = null;

                if (db.EmployeesVacations.Any(x => x.Vacationist.Id == id))
                    lastDate = db.EmployeesVacations.Where(x => x.Vacationist.Id == employee.Id).Max(x => x.EndDate);

                return  new EmployeeInfo()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Surname = employee.Surname,
                    Patronymic = employee.Patronymic,
                    Birthday = employee.Birthday,
                    LastVacationDate = lastDate
                };      
            }
        }
        public int AddVacation(VacationInfo vacation)
        {
            using (EmployeeContext db = new EmployeeContext())
            {
                var item = db.EmployeesVacations.Add(new EmployeesVacation()
                {
                    Substitutional = db.Employees.FirstOrDefault(x => x.Id == vacation.Vacationist.Id),
                    Vacationist = db.Employees.FirstOrDefault(x => x.Id == vacation.Substitutional.Id),
                    StartDate = vacation.StartDate,
                    EndDate = vacation.EndDate,
                    CreatedOn = DateTime.Now
                });

                db.SaveChanges();

                return item.Id;
            }
                
        }
        public List<VacationInfo> GetVacationsByVacationistId(int id, VacationFilter filter = null)
        {
            filter = filter ?? new VacationFilter();
            using (EmployeeContext db = new EmployeeContext())
            {
                var vacationsList = new List<VacationInfo>();

                foreach (var vacation in db.EmployeesVacations.Include(v => v.Substitutional)
                    .Where(x => x.Vacationist.Id == id 
                        && (filter.StartDateFrom.HasValue ? x.StartDate >= filter.StartDateFrom : true)
                        && (filter.StartDateTo.HasValue ? x.StartDate <= filter.StartDateTo : true)
                        && (filter.EndDateFrom.HasValue ? x.EndDate >= filter.EndDateFrom : true)
                        && (filter.EndDateTo.HasValue ? x.EndDate <= filter.EndDateTo : true)
                ))
                {
                    var item = new VacationInfo()
                    {
                        Id = vacation.Id,
                        StartDate = vacation.StartDate,
                        EndDate = vacation.EndDate,
                        Substitutional = new EmployeeInfo()
                        {
                            Id = vacation.Substitutional.Id,
                            Name = vacation.Substitutional.Name,
                            Surname = vacation.Substitutional.Surname,
                            Patronymic = vacation.Substitutional.Patronymic,
                            Birthday = vacation.Substitutional.Birthday
                        }

                    };
                    vacationsList.Add(item);
                }

                return vacationsList;
            }
        }
        public void Test()
        {
            using (EmployeeContext db = new EmployeeContext())
            {

                var e1 = new Employee()
                {
                    Id = 1,
                    Name = "Danil",
                    Surname = "Petrov",
                    Patronymic = "Ivanovich",
                    Birthday = new DateTime(1978, 6, 25)
                };
                var e2 = new Employee()
                {
                    Id = 2,
                    Name = "Stanislav",
                    Surname = "Honcharov",
                    Patronymic = "Sergeevich",
                    Birthday = new DateTime(1989, 2, 15)
                };
                var ee1 = db.Employees.FirstOrDefault(x => x.Id == 1);
                var ee2 = db.Employees.FirstOrDefault(x => x.Id == 2);


                var v1 = new EmployeesVacation()
                {
                    Substitutional = e1,
                    Vacationist = e2,
                    StartDate = new DateTime(2018, 2, 15),
                    EndDate = new DateTime(2018, 3, 15),
                };
                var v2 = new EmployeesVacation()
                {
                    Substitutional = e2,
                    Vacationist = e1,
                    StartDate = new DateTime(2018, 3, 15),
                    EndDate = new DateTime(2018, 4, 15),
                };

                
                db.EmployeesVacations.Add(v1);
                db.EmployeesVacations.Add(v2);
                db.SaveChanges();
            }
        }
    }
}
