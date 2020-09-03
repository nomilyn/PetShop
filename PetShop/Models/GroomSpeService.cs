using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Models
{
    public class GroomSpeService
    {
        private int id;
        private string name;
        private double catPrice;
        private double dogPrice;
        public GroomSpeService() { }

        public GroomSpeService(int id, string name, double catPrice, double dogPrice)
        {
            this.id = id;
            this.name = name;
            this.catPrice = catPrice;
            this.dogPrice = dogPrice;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public double CatPrice { get => catPrice; set => catPrice = value; }
        public double DogPrice { get => dogPrice; set => dogPrice = value; }
    }
}
