using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kebabvognen
{
    public class Review
    {
        private string name;
        private byte rating;
        private string description;
        private DateTime date;

        public string Name { get => name; }
        public byte Rating { get => rating; }
        public string Description { get => description; }
        public DateTime Date { get => date; }

        public Review(string name, byte rating, string description, DateTime date)
        {
            this.name = name;
            this.rating = rating;
            this.description = description;
            this.date = date;
        }
    }
}
