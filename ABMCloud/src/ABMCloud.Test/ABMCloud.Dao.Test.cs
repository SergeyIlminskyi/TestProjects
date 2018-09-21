using System;

using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using ABMCloud.Entites;
using ABMCloud.Dao;

namespace ABMCloud.Test
{
    class ABMCloudDaoTest
    {
        IRepository _repository = new Repository();

        [Test, Order(1)]
        public void AddCollaboratorTest()
        {
            var item = new CollaboratorInfo()
            {
                Name = "Sergey",
                Surname = "Ilminskyi",
                Patronymic = "Vladimirovich",
                Birthday = new DateTime(1992, 2, 12)
            };

            Assert.IsTrue(_repository.AddCollaborator(item) > 0);
        }

        [Test, Order(2)]
        public void GetCollaboratorsTest()
        {
            var collaborators = _repository.GetCollaborators();
            Assert.IsTrue(collaborators.Count > 1);
        }
    }
}
