using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kebabvognen
{
    public class Employee
    {
        private int id;
        private string name;
        private DateTime born;
        private DateTime employmentDate;
        private string phoneNumber;
        private string profileUrl;

        public int Id { get => id; }
        public string Name { get => name; }
        public DateTime Born { get => born; }
        public DateTime Employed { get => employmentDate; }
        public string Phone { get => phoneNumber; }
        public string ProfileUrl { get => profileUrl; }

        public Employee(int id, string name, DateTime born, DateTime employmentDate, string phoneNumber, string profileUrl)
        {
            this.id = id;
            this.name = name;
            this.born = born;
            this.employmentDate = employmentDate;
            this.phoneNumber = phoneNumber;
            this.profileUrl = profileUrl;
        }
    }
}
