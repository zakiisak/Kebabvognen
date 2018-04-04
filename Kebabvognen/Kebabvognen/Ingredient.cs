using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kebabvognen
{
    public class Ingredient
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public Ingredient (int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
