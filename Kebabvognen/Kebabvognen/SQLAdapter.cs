using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Kebabvognen
{
    public class SQLAdapter : DalAdapter
    {
        private string connectionString = @"Persist Security Info=False;Integrated Security=true;Initial Catalog=Kebabvogn;server=localhost\SQLEXPRESS;Connection Timeout=3000";
        private bool running;

        public SQLAdapter()
        {
        }

        public override void Start()
        {
            running = true;
    }

        public override void Stop()
        {
            running = false;
        }

        public override bool IsRunning()
        {
            return running;
        }

        public override Menu[] GetMenus()
        {
            List<Menu> menus = new List<Menu>();
            SqlDataReader menuReader = Query("SELECT * FROM Menu");
            if (menuReader.HasRows)
            {
                while (menuReader.Read())
                {
                    int menuId = (int)menuReader.GetInt32(0);
                    string menuName = menuReader.GetString(1);
                    int menuPrice = menuReader.GetInt32(2);
                    string imageUrl = menuReader.GetString(3);
                    Debug.WriteLine("Image URL " + imageUrl);

                    List<Ingredient> ingredients = new List<Ingredient>();
                    SqlDataReader ingredientReader = Query("SELECT * FROM Ingredients Where MenuID = " + menuId);
                    if (ingredientReader.HasRows)
                    {
                        while (ingredientReader.Read())
                        {
                            int id = ingredientReader.GetInt32(0);
                            string name = ingredientReader.GetString(1);
                            Ingredient ingredient = new Ingredient(id, name);
                            ingredients.Add(ingredient);
                        }
                    }
                    ingredientReader.Close();
                    Menu menu = new Menu(menuId, menuName, menuPrice, imageUrl, ingredients.ToArray());
                    menus.Add(menu);
                } 
            }
            menuReader.Close();
            return menus.ToArray();
        }


        private SqlDataReader Query(string command)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand com = new SqlCommand(command, connection);
            return com.ExecuteReader();
        }

        public override Menu GetMenu(int id)
        {
            SqlDataReader reader = Query("SELECT * FROM Menu WHERE ID=" + id);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int menuId = (int)reader.GetInt32(0);
                    string menuName = reader.GetString(1);
                    int menuPrice = reader.GetInt32(2);
                    string imageUrl = reader.GetString(3);

                    List<Ingredient> ingredients = new List<Ingredient>();
                    SqlDataReader ingredientReader = Query("SELECT * FROM Ingredients Where MenuID = " + menuId);
                    if (ingredientReader.HasRows)
                    {
                        while (ingredientReader.Read())
                        {
                            int ingredientId = ingredientReader.GetInt32(0);
                            string name = ingredientReader.GetString(1);
                            Ingredient ingredient = new Ingredient(ingredientId, name);
                            ingredients.Add(ingredient);
                        }
                    }
                    ingredientReader.Close();
                    reader.Close();
                    return new Menu(menuId, menuName, menuPrice, imageUrl, ingredients.ToArray());
                }
            }
            reader.Close();
            return null;
        }
        public override OpeningHours GetOpeningHour(int dayNumber)
        {
            SqlDataReader reader = Query("SELECT * FROM OpeningHours WHERE DayNum=" + dayNumber);
            
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    byte dayNum = reader.GetByte(0);
                    TimeSpan startTime = reader.IsDBNull(1) == false ? reader.GetTimeSpan(1) : TimeSpan.MinValue;
                    TimeSpan endTime = reader.IsDBNull(2) == false ? reader.GetTimeSpan(2) : TimeSpan.MinValue;
                    OpeningHours day = new OpeningHours(dayNum, startTime, endTime);
                    reader.Close();
                    return day;
                }
            }
            reader.Close();
            return null;
        }
        public override Review GetReview(int id)
        {
            SqlDataReader reader = Query("SELECT * FROM Reviews WHERE ID=" + id);
            List<Review> reviews = new List<Review>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int reviewId = reader.GetInt32(0);
                    string reviewerName = reader.GetString(1);
                    byte rating = reader.GetByte(2);
                    string description = reader.GetString(3);
                    DateTime reviewDate = reader.GetDateTime(4);
                    Review review = new Review(reviewId, reviewerName, rating, description, reviewDate);
                    reader.Close();
                    return review;
                }
            }
            reader.Close();
            return null;
        }
        public override Employee GetEmployee(int id)
        {
            SqlDataReader reader = Query("SELECT * FROM Staff WHERE ID=" + id);
            List<Employee> employees = new List<Employee>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int employeeId = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    DateTime born = reader.GetDateTime(2);
                    DateTime employed = reader.GetDateTime(3);
                    string phoneNumber = reader.GetString(4);
                    string profileUrl = reader.GetString(5);
                    Employee employee = new Employee(id, name, born, employed, phoneNumber, profileUrl);
                    reader.Close();
                    return employee;
                }
            }
            reader.Close();
            return null;
        }

        public override OpeningHours[] GetOpeningHours()
        {
            SqlDataReader reader = Query("SELECT * FROM OpeningHours");
            List<OpeningHours> openingHours = new List<OpeningHours>();
            if(reader.HasRows)
            {
                while(reader.Read())
                {
                    byte dayNum = reader.GetByte(0);
                    TimeSpan startTime = reader.IsDBNull(1) == false ? reader.GetTimeSpan(1) : TimeSpan.MinValue;
                    TimeSpan endTime = reader.IsDBNull(2) == false ? reader.GetTimeSpan(2) : TimeSpan.MinValue;
                    OpeningHours day = new OpeningHours(dayNum, startTime, endTime);
                    openingHours.Add(day);
                }
            }
            reader.Close();
            return openingHours.ToArray();
        }

        public override Review[] GetReviews()
        {
            SqlDataReader reader = Query("SELECT * FROM Reviews");
            List<Review> reviews = new List<Review>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string reviewerName = reader.GetString(1);
                    byte rating = reader.GetByte(2);
                    string description = reader.GetString(3);
                    DateTime reviewDate = reader.GetDateTime(4);
                    Review review = new Review(id, reviewerName, rating, description, reviewDate);
                    reviews.Add(review);
                }
            }
            reader.Close();
            return reviews.ToArray();
        }

        public override Review[] GetNewestReviews(int limit)
        {
            SqlDataReader reader = Query("SELECT top " + limit + " * FROM Reviews ORDER BY ReviewDate DESC");
            List<Review> reviews = new List<Review>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string reviewerName = reader.GetString(1);
                    byte rating = reader.GetByte(2);
                    string description = reader.GetString(3);
                    DateTime reviewDate = reader.GetDateTime(4);
                    Review review = new Review(id, reviewerName, rating, description, reviewDate);
                    reviews.Add(review);
                }
            }
            reader.Close();
            return reviews.ToArray();
        }



        public override Employee[] GetEmployees()
        {
            SqlDataReader reader = Query("SELECT * FROM Staff");
            List<Employee> employees = new List<Employee>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    DateTime born = reader.GetDateTime(2);
                    DateTime employed = reader.GetDateTime(3);
                    string phoneNumber = reader.GetString(4);
                    string profileUrl = reader.GetString(5);
                    Employee employee = new Employee(id, name, born, employed, phoneNumber, profileUrl);
                    employees.Add(employee);
                }
            }
            reader.Close();
            return employees.ToArray();
        }

        public override void AddMenu(Menu menu)
        {
            throw new NotImplementedException();
        }

        public override void RemoveMenu(int menu)
        {
            throw new NotImplementedException();
        }

        public override void SetOpeningHours(OpeningHours hours)
        {
            throw new NotImplementedException();
        }

        public override void AddReview(Review review)
        {
            throw new NotImplementedException();
        }

        public override void AddEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public override void RemoveEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public override void ChangeMenu(Menu menu)
        {
            Query("UPDATE Menu Set Name='" + menu.Name + "', Price='" + menu.Price + "', imageUrl='" + menu.ImageUrl + "' WHERE ID=" + menu.Id);
            for(int i = 0; i < menu.Ingredients.Count; i++)
            {
                Ingredient ingredient = menu.Ingredients[i];
                Query("UPDATE Ingredients Set Name='" + ingredient.Name + "' WHERE MenuID=" + menu.Id);
            }
        }

        public override void ChangeOpeningHours(OpeningHours hours)
        {
            Query("UPDATE OpeningHours Set StartTime='" + hours.From.ToString() + "', EndTime='" + hours.To.ToString() + "' WHERE DayNum=" + hours.Day);
        }

        public override void ChangeEmployee(Employee employee)
        {
            Query("UPDATE Staff Set Name='" + employee.Name + "', Born='" + employee.Born + "', EmploymentDate='" + employee.Employed + "' WHERE ID=" + employee.Id);
        }

        public override void ChangeColumn(string table, string column, string idColumnName, int id, object value)
        {
            //Numeric
            if (value is int || value is byte || value is long || value is double || value is float)
            {
                Query("UPDATE " + table + " SET " + column + "=" + value + " WHERE " + idColumnName + "=" + id);
            }
            else
            {
                Query("UPDATE " + table + " SET " + column + "='" + value + "' WHERE " + idColumnName + "=" + id);
            }
        }

        public override Address[] GetAddresses()
        {
            SqlDataReader reader = Query("SELECT * FROM Address");
            List<Address> addresses = new List<Address>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string city = reader.GetString(1);
                    int zipCode = reader.GetInt32(2);
                    string billingAddress = reader.GetString(3);
                    Address address = new Address(id, city, zipCode, billingAddress);
                    addresses.Add(address);
                }
            }
            reader.Close();
            return addresses.ToArray();
        }


        public override Address GetAddress(int id)
        {
            SqlDataReader reader = Query("SELECT * FROM Address WHERE ID=" + id);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int addressId = reader.GetInt32(0);
                    string city = reader.GetString(1);
                    int zipCode = reader.GetInt32(2);
                    string billingAddress = reader.GetString(3);
                    Address address = new Address(addressId, city, zipCode, billingAddress);
                    reader.Close();
                    return address;
                }
            }
            reader.Close();
            return null;
        }
    }
}
