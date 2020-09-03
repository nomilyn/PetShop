using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace PetShop.Models
{
    [XmlRoot("PetList")]
    [XmlInclude(typeof(Dog))]
    [XmlInclude(typeof(Cat))]
    public class AnimalList
    {
        private List<Pet> petList = null;
        
        [XmlArray("Pets")]
        [XmlArrayItem("Pet", typeof(Pet))]
        public List<Pet> PetList { get => petList; set => petList = value; }

        public AnimalList()
        {
            PetList = new List<Pet>();
        }
        public void Add(Pet pet)
        {
            PetList.Add(pet);
        }
        public void Remove(Pet pet)
        {
            PetList.Remove(pet);
        }
        public int Count()
        {
            return PetList.Count;
        }
        public void Clear()
        {
            PetList.Clear();
        }
    }
}


