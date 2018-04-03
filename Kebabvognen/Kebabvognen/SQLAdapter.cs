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
        private string connectionString = @"Persist Security Info=False;Integrated Security=true;Initial Catalog=Kebabvogn;server=Localhost\SQLEXPRESS;Connection Timeout=3000";
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
                    Debug.WriteLine(menuReader.GetValue(0));
                    string menuName = menuReader.GetValue(0).ToString();
                    int menuPrice = (int)menuReader.GetValue(1);
                    int menuID = (int)menuReader.GetValue(2);

                    List<Ingredient> ingredients = new List<Ingredient>();
                    SqlDataReader ingredientReader = Query("SELECT * FROM Ingredients Where MenuID = " + menuID);
                    if (ingredientReader.HasRows)
                    {
                        while (ingredientReader.Read())
                        {
                            string name = ingredientReader.GetValue(0).ToString();
                            Ingredient ingredient = new Ingredient(name);
                            ingredients.Add(ingredient);
                        }
                    }
                    ingredientReader.Close();
                    Menu menu = new Menu(menuName, menuPrice, ingredients.ToArray());
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


    }
}
