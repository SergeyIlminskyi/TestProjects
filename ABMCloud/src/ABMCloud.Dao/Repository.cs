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

        public List<EmployeeInfo> GetAllEmployees()
        {
            using (EmployeeContext db = new EmployeeContext())
            {
                var employeesResult = new List<EmployeeInfo>();

                foreach (var employee in db.Employees)
                {
                    DateTime? lastDate = null;

                    var item = new EmployeeInfo()
                    {
                        Id = employee.Id,
                        Name = employee.Name,
                        Surname = employee.Surname,
                        Patronymic = employee.Patronymic,
                        Birthday = employee.Birthday
                    };

                    employeesResult.Add(item);
                }

                return employeesResult;
            }
        }
        public List<EmployeeInfo> GetEmployees(EmployeeFilter filter, out long totalItems)
        {
            filter = filter ?? new EmployeeFilter();

            using (EmployeeContext db = new EmployeeContext())
            {
                var employeesResult = new List<EmployeeInfo>();

                var tempEmployees = db.Employees.Where(x => 
                       (String.IsNullOrEmpty(filter.Surname) ? true : x.Surname == filter.Surname)
                    && (String.IsNullOrEmpty(filter.Name) ? true : x.Name == filter.Name)
                    && (String.IsNullOrEmpty(filter.Patronymic) ? true : x.Patronymic == filter.Patronymic)
                ).ToList();
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

                    employeesResult.Add(item);
                }

                employeesResult = employeesResult.Where(x =>
                           (filter.DateFrom.HasValue ? x.LastVacationDate >= filter.DateFrom : true)
                        && (filter.DateTo.HasValue ? x.LastVacationDate <= filter.DateTo : true)).ToList();

                totalItems = employeesResult.Count();

                return employeesResult.OrderBy(x => x.Id).Skip(filter.PageSize * (filter.CurrenPage - 1)).Take(filter.PageSize).ToList();
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
        public List<VacationInfo> GetVacationsByVacationistId(int id, out long totalItems, VacationFilter filter = null)
        {
            filter = filter ?? new VacationFilter();
            using (EmployeeContext db = new EmployeeContext())
            {
                var vacationsList = new List<VacationInfo>();

                totalItems = db.EmployeesVacations.Count(x => x.Vacationist.Id == id);

                foreach (var vacation in db.EmployeesVacations.Include(v => v.Substitutional)
                    .Where(x => x.Vacationist.Id == id 
                        && (filter.StartDateFrom.HasValue ? x.StartDate >= filter.StartDateFrom : true)
                        && (filter.StartDateTo.HasValue ? x.StartDate <= filter.StartDateTo : true)
                        && (filter.EndDateFrom.HasValue ? x.EndDate >= filter.EndDateFrom : true)
                        && (filter.EndDateTo.HasValue ? x.EndDate <= filter.EndDateTo : true)
                ).OrderBy(x => x.Id).Skip(filter.PageSize * (filter.CurrenPage - 1)).Take(filter.PageSize))
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
                    Substitutional = ee1,
                    Vacationist = ee2,
                    StartDate = new DateTime(2012, 2, 15),
                    EndDate = new DateTime(2012, 3, 15),
                };
                var v2 = new EmployeesVacation()
                {
                    Substitutional = ee2,
                    Vacationist = ee1,
                    StartDate = new DateTime(2012, 3, 15),
                    EndDate = new DateTime(2012, 4, 15),
                };

                
                db.EmployeesVacations.Add(v1);
                db.EmployeesVacations.Add(v2);
                db.SaveChanges();
            }
        }
    }
}
