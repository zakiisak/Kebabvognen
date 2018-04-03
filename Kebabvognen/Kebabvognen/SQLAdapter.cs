using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Kebabvognen
{
    public class SQLAdapter : DalAdapter
    {
        private string connectionString = @"Persist Security Info=False;Integrated Security=true;Initial Catalog=Kebabvogn;server=Localhost;Connection Timeout=3000";
        private bool running;

        private SqlConnection connection;

        public SQLAdapter()
        {
            connection = new SqlConnection(connectionString);
            
            SqlCommand command = new SqlCommand();
            
        }

        public override void Start()
        {
            connection.Open();
            running = true;
    }

        public override void Stop()
        {
            connection.Close();
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
            while(menuReader.HasRows)
            {
                string menuName = menuReader.GetValue(0).ToString();
                int menuPrice = (int) menuReader.GetValue(1);
                int menuID = (int) menuReader.GetValue(2);

                List<Ingredient> ingredients = new List<Ingredient>();
                SqlDataReader ingredientReader = Query("SELECT * FROM Ingredients Where MenuID = " + menuID);
                while(ingredientReader.HasRows)
                {
                    string name = ingredientReader.GetValue(0).ToString();
                    Ingredient ingredient = new Ingredient(name);
                    ingredients.Add(ingredient);
                }
                ingredientReader.Close();
                Menu menu = new Menu(menuName, menuPrice, ingredients.ToArray());
                menus.Add(menu);
            }
            menuReader.Close();
            return menus.ToArray();
        }


        private SqlDataReader Query(string command)
        {
            SqlCommand com = new SqlCommand(command, connection);
            return com.ExecuteReader();
        }


    }
}
