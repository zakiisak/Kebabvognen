using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kebabvognen
{
    public abstract class DalAdapter
    {
        public abstract bool IsRunning();
        public abstract void Start();
        public abstract void Stop();
        public abstract Menu[] GetMenus();
        public abstract Menu GetMenu(int id);
        public abstract OpeningHours[] GetOpeningHours();
        public abstract OpeningHours GetOpeningHour(int id);
        public abstract Review[] GetReviews();
        public abstract Review GetReview(int id);
        public abstract Review[] GetNewestReviews(int limit);
        public abstract Employee[] GetEmployees();
        public abstract Employee GetEmployee(int id);
        public abstract Address[] GetAddresses();
        public abstract Address GetAddress(int id);

        public abstract void ChangeMenu(Menu menu);
        public abstract void ChangeOpeningHours(OpeningHours hours);
        public abstract void ChangeEmployee(Employee employee);
        
        public abstract void AddMenu(Menu menu);
        public abstract void RemoveMenu(int menu);
        public abstract void SetOpeningHours(OpeningHours hours);
        public abstract void AddReview(Review review);
        public abstract void AddEmployee(Employee employee);
        public abstract void RemoveEmployee(int id);

        public abstract Menu[] SearchMenus(string ingredientNamePart);

        public abstract void ChangeColumn(string table, string column, string idColumnName, int id, object value);


        
    }
}
