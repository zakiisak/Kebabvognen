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
        public abstract OpeningHours[] GetOpeningHours();
        public abstract Review[] GetReviews();
        public abstract Employee[] GetEmployees();

        public abstract void AddMenu(Menu menu);
        public abstract void RemoveMenu(int menu);
        public abstract void SetOpeningHours(OpeningHours hours);
        public abstract void AddReview(Review review);
        public abstract void AddEmployee(Employee employee);


        
    }
}
