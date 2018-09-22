using System;
using NUnit.Framework;
using ABMCloud.Entites;
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
            var collaborators = _repository.GetEmployees();
            Assert.IsTrue(collaborators.Count > 1);
        }
    }
}
