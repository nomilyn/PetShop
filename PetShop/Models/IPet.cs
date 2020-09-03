using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Models
{
    public interface IPet : IComparable<IPet>
    {
        public int PetID { get; set; }
        public string PetName { get; set; }
        public string Type { get; set; }
        public string Race { get; set; }
        public string Color { get; set; }
        public double Age { get; set; }
        public string GenService { get; set; }
        public string SpeService { get; set; }
        public double Price { get; set; }
        public string Owner { get; set; }
        public string PhoneNo { get; set; } 
    }
}
