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
                            string name = ingredientReader.GetValue(1).ToString();
                            Ingredient ingredient = new Ingredient(name);
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
            return reviews.ToArray();
        }



        public override Employee[] GetEmployees()
        {
            throw new NotImplementedException();
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
    }
}
