using System;
using NUnit.Framework;
using ABMCloud.Entities;
using ABMCloud.Dao;

namespace ABMCloud.Test
{
    class ABMCloudDaoTest
    {
        IRepository _repository = new Repository();

        [Test, Order(1)]
        public void AddEmployeeTest()
        {
            var item = new EmployeeInfo()
            {
                Name = "Sergey",
                Surname = "Ilminskyi",
                Patronymic = "Vladimirovich",
                Birthday = new DateTime(1992, 2, 12)
            };

            Assert.IsTrue(_repository.AddEmployee(item) > 0);
        }

        [Test, Order(2)]
        public void GetEmployeesTest()
        {
            var item1 = new EmployeeInfo()
            {
                Surname = "Poberezets",
                Name = "Vladislav",
                Patronymic = "Olegovich",
                Birthday = new DateTime(1990, 12, 2) };
            var item2 = new EmployeeInfo()
            {
                Surname = "Antonov",
                Name = "Dmitriy",
                Patronymic = "Valentinovich",
                Birthday = new DateTime(1984, 7, 21)
            };

            _repository.AddEmployee(item1);
            _repository.AddEmployee(item2);
            long totalItems;
            var filter = new EmployeeFilter()
            {
                CurrenPage = 1,
                PageSize = 10
            };
            var employees = _repository.GetEmployees(filter, out totalItems);
            Assert.IsTrue(employees.Count > 1);
        }

        [Test, Order(3)]
        public void GetEmployeeDetailsTest()
        {
            var item = new EmployeeInfo()
            {
                Surname = "Petrov",
                Name = "Stas",
                Patronymic = "Valentinovich",
                Birthday = new DateTime(1985, 6, 22)
            };

            var id = _repository.AddEmployee(item);
            var employee = _repository.GetEmployeeDetailsById(id);

            Assert.IsTrue(employee.Id > 0);
        }

        [Test, Order(4)]
        public void AddVacationTest()
        {
            var e1 = new EmployeeInfo()
            {
                Surname = "Kot",
                Name = "Danil",
                Patronymic = "Ivanovich",
                Birthday = new DateTime(1978, 6, 25)
            };
            var e2 = new EmployeeInfo()
            {
                Surname = "Honcharov",
                Name = "Stanislav", 
                Patronymic = "Sergeevich",
                Birthday = new DateTime(1989, 2, 15)
            };
            var id1 = _repository.AddEmployee(e1);
            var id2 = _repository.AddEmployee(e2);

            var vacation = new VacationInfo()
            {
                Substitutional = _repository.GetEmployeeDetailsById(id1),
                Vacationist = _repository.GetEmployeeDetailsById(id2),
                StartDate = new DateTime(2016, 2, 15),
                EndDate = new DateTime(2016, 3, 15),
            };

            Assert.IsTrue(_repository.AddVacation(vacation) > 0);
        }

    }
}
