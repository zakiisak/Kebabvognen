using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kebabvognen
{
    public class AdapterManager
    {
        private static DalAdapter adapter = new SQLAdapter();

        public AdapterManager()
        {
            
        }

        private static void AssertStart()
        {
            if (adapter.IsRunning() == false)
                adapter.Start();
        }

        public static Menu[] GetMenus()
        {
            AssertStart();
            return adapter.GetMenus();
        }

        public static Menu GetMenu(int id)
        {
            return adapter.GetMenu(id);
        }

        public static OpeningHours GetOpeningHour(int id)
        {
            return adapter.GetOpeningHour(id);
        }

        public static Employee GetEmployee(int id)
        {
            return adapter.GetEmployee(id);
        }

        public static Review GetReview(int id)
        {
            return adapter.GetReview(id);
        }

        public static Review[] Get5NewestReviews()
        {
            AssertStart();
            return adapter.GetNewestReviews(5);
        }


        public static OpeningHours[] GetOpeningHours()
        {
            AssertStart();
            return adapter.GetOpeningHours();
        }

        public static Employee[] GetEmployees()
        {
            AssertStart();
            return adapter.GetEmployees();
        }

        public static Address[] GetAddresses()
        {
            AssertStart();
            return adapter.GetAddresses();
        }

        public static Address GetAddress(int id)
        {
            AssertStart();
            return adapter.GetAddress(id);
        }

        public static void ChangeMenu(Menu menu)
        {
            adapter.ChangeMenu(menu);
        }

        public static void ChangeOpeningHours(OpeningHours hours)
        {
            adapter.ChangeOpeningHours(hours);
        }

        public static void ChangeEmployee(Employee employee)
        {
            adapter.ChangeEmployee(employee);
        }

        public static void ChangeColumn(string table, string column, int id, object value)
        {
            if(table.ToLower().StartsWith("opening"))
            {
                adapter.ChangeColumn(table, column, "DayNum", id, value);
            }
            else
            {
                adapter.ChangeColumn(table, column, "ID", id, value);
            }
        }

        public static Menu[] SearchMenus(string search)
        {
            AssertStart();
            return adapter.SearchMenus(search);
        }

        public static void Dispose()
        {
            if(adapter.IsRunning())
                adapter.Stop();
        }

    }
}
