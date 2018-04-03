using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kebabvognen
{
    public class Menu
    {
        public string Name { get; private set; }
        public int Price { get; private set; }
        public List<Ingredient> Ingredients { get; private set; }

        public Menu(string name, int price, Ingredient[] ingredients)
        {
            Name = name;
            Price = price;
            Ingredients = new List<Ingredient>();
            for (int i = 0; i < ingredients.Length; i++)
                Ingredients.Add(ingredients[i]);
        }

    }
}
