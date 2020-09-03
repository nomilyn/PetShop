using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace PetShop.Models
{
    public class Pet : IPet
    {
        private int petID;
        private string petName;
        private string type;
        private string race;
        private string color;
        private double age;
        private string genService;
        private string speService;
        private double price;
        private string owner;
        private string phoneNo; 
        public int PetID { get => petID ; set => petID = value; }
        public string PetName { get => petName; set => petName = value; }
        public string Type { get => type; set => type = value; }
        public string Race { get => race; set => race = value; }
        public string Color { get => color; set => color = value; }
        public double Age { get => age; set => age = value; }
        public string GenService { get => genService; set => genService = value; }
        public string SpeService { get => speService; set => speService = value; }
        public double Price { get => price; set => price = value; }
        public string Owner { get => owner; set => owner = value; }
        public string PhoneNo { get => phoneNo; set => phoneNo = value; } 

        public Pet()
        {
            this.petID = 0;
            this.petName = "";
            this.type = "";
            this.race = "";
            this.color = "";
            this.age = 0;
            this.genService = "";
            this.speService = "";
            this.price = 0;
            this.owner = "";
            this.phoneNo = "";
        }
        public Pet(int petID, string petName, string type, string race, string color, double age, 
                  string genService, string speService, double price, string owner, string phoneNo)
        {
            this.petID = petID;
            this.petName = petName;
            this.type = type;
            this.race = race;
            this.color = color;
            this.age = age;
            this.genService =genService;
            this.speService = speService;
            this.price = price;
            this.owner = owner;
            this.phoneNo = phoneNo; 
        }
        public int CompareTo(IPet other)
        {
            return age.CompareTo(other.Age);
        }
        public override string ToString()
        {
            return string.Format("Pet ID : {0}, Pet Name : {1} Type : {2} Race : {3} Color: {4} Age: {5} General Service : {6} Specific Service : {7} Price : {8}  Owner : {9} Phone Number : {10}", petID, petName, type, race, color, age, genService, speService, price, owner, phoneNo); 
        }
    }
} 
