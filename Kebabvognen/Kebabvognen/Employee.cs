using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kebabvognen
{
    public class Employee
    {
        private string name;
        private DateTime born;
        private DateTime employmentDate;

        public string Name { get => name; }
        public DateTime Born { get => born; }
        public DateTime Employed { get => employmentDate; }

        public Employee(string name, DateTime born, DateTime employmentDate)
        {
            this.name = name;
            this.born = born;
            this.employmentDate = employmentDate;
        }
    }
}
