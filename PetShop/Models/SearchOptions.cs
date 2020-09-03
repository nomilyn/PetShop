using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Models
{
    public class SearchOptions
    {
        private int id;
        private string name;
        public SearchOptions() { }

        public SearchOptions(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
    }
}
