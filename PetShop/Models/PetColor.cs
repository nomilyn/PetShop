using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Models
{
    public class PetColor
    {
        private int id;
        private string name;
        public PetColor() { }

        public PetColor(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
    }
}
