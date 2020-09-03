using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Models
{
    public class Cat : Pet
    {
        public Cat()
        {
        }
        public Cat(int petID, string petName, string type, string race, string color, double age, string genService, string speService, double price, string owner, string phoneNo) : base(petID, petName, type, race, color, age, genService,speService,price,owner,phoneNo)
        {
        }
    }
}


