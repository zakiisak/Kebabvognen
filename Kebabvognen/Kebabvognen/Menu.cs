using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kebabvognen
{
    public class Menu
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Price { get; private set; }
        public string ImageUrl { get; private set; }
        public List<Ingredient> Ingredients { get; private set; }

        public Menu(int id, string name, int price, string imageUrl, Ingredient[] ingredients)
        {
            Id = id;
            Name = name;
            Price = price;
            ImageUrl = imageUrl;
            Ingredients = new List<Ingredient>();
            for (int i = 0; i < ingredients.Length; i++)
                Ingredients.Add(ingredients[i]);
        }

    }
}
