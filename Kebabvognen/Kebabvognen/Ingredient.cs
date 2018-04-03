using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kebabvognen
{
    public class Ingredient
    {
        public string Name { get; private set; }

        public Ingredient (string name)
        {
            Name = name;
        }
    }
}
