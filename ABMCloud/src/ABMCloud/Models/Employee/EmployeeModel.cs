using System.Collections.Generic;

namespace ABMCloud.Models
{
    public class EmployeeModel
    {
        public IReadOnlyList<EmployeeDetailsModel> EmployeesList { get; set; }

        public long EmployeesCount { get; set; } //вынести в базовый
    }
}