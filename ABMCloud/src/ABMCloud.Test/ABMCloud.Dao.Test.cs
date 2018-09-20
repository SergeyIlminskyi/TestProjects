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
        public void GetCollaboratorsTest()
        {
            var collaborators = _repository.GetCollaborators();
            Assert.IsTrue(collaborators.Count > 1);
        }
    }
}
