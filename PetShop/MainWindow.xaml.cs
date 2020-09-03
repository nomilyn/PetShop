using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using PetShop.Models;


namespace PetShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        ObservableCollection<Pet> displayPets = null;
        
        AnimalList petList = new AnimalList();

        public ObservableCollection<Pet> DisplayPets { get => displayPets; set => displayPets = value; }
        Pet aPet = new Pet();
        public Pet APet { get => aPet; set => aPet = value; }
        public MainWindow()
        {
            InitializeComponent();
            SetUpForm();

            displayPets = new ObservableCollection<Pet>();
            DataContext = this;
        }
        //To set up the form upon opening        
    private void SetUpForm()
        {
            txtPrice.IsEnabled = false;
            DisplayPetType();
            DisplayColors();
            DisplayGeneralService();
            DisplaySearchOptions();
            btnAdd.IsEnabled = true;
            btnSave.IsEnabled = false;
            txtSearch.IsEnabled = false;
        }
        //To add the data entered to a datagrid
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnAdd.Content = "Add";
                btnAdd.IsEnabled=false;
                btnReset.IsEnabled = false;
                //Validation for Pet Name
                if (txtPetName.Text == "")
                {
                    MessageBox.Show("Pet Name is empty!", "Error");
                    btnAdd.IsEnabled = true;
                }
                else if (Char.IsDigit(txtPetName.Text, 0))
                {
                    MessageBox.Show("Pet Name is Invalid! Number is Not Allowed.", "Error");
                    btnAdd.IsEnabled = true;
                }
                else if (txtPetName.Text.Length < 2)
                {
                    MessageBox.Show("Pet Name is Invalid! Character is Not Allowed.", "Error");
                    btnAdd.IsEnabled = true;
                }
                //For adding in the list if Pet Name and Type are valid and automatically diplayed in the datagrid
                else
                { 
                    Pet newPet = new Pet();
                    switch (cboType.SelectedIndex)
                    {
                        case 0:
                            newPet = new Cat(int.Parse(txtPetID.Text), txtPetName.Text, cboType.Text, cboRace.Text, cboColor.Text, double.Parse(txtAge.Text), cboGenService.Text, cboSpeService.Text, double.Parse(txtPrice.Text), txtOwner.Text,txtPhoneNo.Text);
                            break;
                        case 1:
                            newPet = new Dog(int.Parse(txtPetID.Text), txtPetName.Text, cboType.Text, cboRace.Text, cboColor.Text, double.Parse(txtAge.Text), cboGenService.Text, cboSpeService.Text, double.Parse(txtPrice.Text), txtOwner.Text, txtPhoneNo.Text);
                            break;
                        default:
                            MessageBox.Show("Invalid Pet Type", "Error");
                            return;
                    }
                    DisplayPets.Add(newPet);
                    ClearForm();
                }
                btnSave.IsEnabled = true;
                btnAdd.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message);
            }
        }
        //To save all the newly added data from datagrid to XML file
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            btnAdd.IsEnabled = true;
            btnSave.IsEnabled = false;
            btnReset.IsEnabled = true;
            dgDisplay.IsEnabled = true;
            petList.Clear();
            foreach (Pet p in DisplayPets)
            {
                petList.Add(p);
            }
            if (WriteToFile())
            {
                MessageBox.Show("File Saved Successfully!", "Information");
            }
        }
        //To read and display the content of XML file
        private void btnDisplay_Click(object sender, RoutedEventArgs e)
        {
            petList.Clear();
            ReadFromFile();
            DisplayPets.Clear();

            foreach (Pet p in petList.PetList)
            {
                DisplayPets.Add(p);
            }
            dgDisplay.ItemsSource = DisplayPets;
        }
        //To search a data based on the option in the combobox using ".Contains"
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var query = from pet in DisplayPets
                        select pet;
            var queryCount = (from pet in DisplayPets
                        select pet).Count();
            switch (cboSearchBy.SelectedIndex)
            {
                case 0:
                    query = from pet in DisplayPets
                                where pet.Owner.ToUpper().Contains(txtSearch.Text.ToUpper().Trim())
                                select pet;

                    queryCount = (from pet in DisplayPets
                            where pet.Owner.ToUpper().Contains(txtSearch.Text.ToUpper().Trim())
                            select pet).Count();
                    break;
                case 1:
                    query = from pet in DisplayPets
                                where pet.PetName.ToUpper().Contains(txtSearch.Text.ToUpper().Trim())
                                select pet;

                    queryCount = (from pet in DisplayPets
                            where pet.PetName.ToUpper().Contains(txtSearch.Text.ToUpper().Trim())
                            select pet).Count();
                    break;
                case 2:
                    query = from pet in DisplayPets
                            where pet.Type.ToUpper().Contains(txtSearch.Text.ToUpper().Trim())
                            select pet;

                    queryCount = (from pet in DisplayPets
                            where pet.Type.ToUpper().Contains(txtSearch.Text.ToUpper().Trim())
                            select pet).Count();
                    break;
                default:
                    MessageBox.Show("Invalid Search By Option","Information",MessageBoxButton.OK,MessageBoxImage.Information);
                    return;
            }

            if (txtSearch.Text == "")
            {
                MessageBox.Show("Invalid! Search textbox is empty.", "Information");
            }
            else if (queryCount == 0)
            {
                MessageBox.Show("Record Does Not Exist!", "Information");
            }
            else
            {
                dgSearchList.ItemsSource = query;
            }
            txtSearch.Focus();
        }
        //To save the data to the XML file
        private bool WriteToFile()
        {
            XmlSerializer xs = new XmlSerializer(typeof(AnimalList));
            TextWriter tw = new StreamWriter("pets.xml");
            xs.Serialize(tw, petList);
            tw.Close();
            return true;
        }
        //To read or retrieve data from XML file
        private void ReadFromFile()
        {
            XmlSerializer xs = new XmlSerializer(typeof(AnimalList));
            TextReader tr = new StreamReader("pets.xml");
            petList = (AnimalList)xs.Deserialize(tr);
            tr.Close();
        }
        //To clear the form
        private void ClearForm()
        {
            txtPetID.Text = string.Empty;
            txtPetName.Text = string.Empty;
            cboType.SelectedIndex = -1;
            cboRace.SelectedIndex = -1;
            txtAge.Text = string.Empty;
            cboGenService.SelectedIndex = -1;
            cboSpeService.SelectedIndex = -1;
            txtPrice.Text = string.Empty;
            cboColor.SelectedIndex = -1;
            txtOwner.Text = string.Empty;
            txtPhoneNo.Text = string.Empty;
            cboSearchBy.SelectedIndex = -1;
        }
        //To display the races of pet in a combobox based on the type of pet
        private void cboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboType.SelectedIndex == 0)
            {
                DisplayCatRaces();
            }
            else if (cboType.SelectedIndex == 1)
            {
                DisplayDogRaces();
            }
        }
        //To display the specific service of pet in a combobox based on the general service  
        private void cboGenService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboGenService.SelectedIndex == 0)
            {
                DisplayGroomSpeService();
            }
            else if (cboGenService.SelectedIndex == 1)
            {
                DisplayHotelSpeService();
            }
        }
        //To display the price of a specific service based on general service and type of pet
        private void cboSpeService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cbo = sender as ComboBox;                  

            switch (cboType.SelectedIndex)
            {
                case 0: //Cat
                    if (cboGenService.SelectedIndex == 0) //Grooming
                    {
                        GroomSpeService groomSpeService = (GroomSpeService)cbo.SelectedItem;
                        txtPrice.Text = groomSpeService.CatPrice.ToString();
                    }
                    else if (cboGenService.SelectedIndex == 1) //Hotel
                    {
                        HotelSpeService hotelSpeService = (HotelSpeService)cbo.SelectedItem;
                        txtPrice.Text = hotelSpeService.CatPrice.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Specific Service", "Error");
                        txtPrice.Text = "";
                    }
                    break;
                case 1: //Dog
                    if (cboGenService.SelectedIndex == 0) //Grooming
                    {
                        GroomSpeService groomSpeService = (GroomSpeService)cbo.SelectedItem;
                        txtPrice.Text = groomSpeService.DogPrice.ToString();
                    }
                    else if (cboGenService.SelectedIndex == 1) //Hotel
                    {
                        HotelSpeService hotelSpeService = (HotelSpeService)cbo.SelectedItem;
                        txtPrice.Text = hotelSpeService.DogPrice.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Specific Service", "Error");
                        txtPrice.Text = "";
                    }
                    break;
            }
        }
        ////To display the search option in combobox
        private void cboSearchBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cbo = sender as ComboBox;
            SearchOptions search  = (SearchOptions)cbo.SelectedItem;
            lblSearch.Content = "Search " + search.Name  + " that Contains"; //To display the selected option in a label
            txtSearch.IsEnabled = true;
        }

/*        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var editPet = DisplayPets.FirstOrDefault(en => en.PetID.ToString() == txtDelete.Text.ToString());
            txtPetID.Text = editPet.PetID.ToString();
            txtPetName.Text = editPet.PetName;
            cboType.Text= editPet.Type;
            cboRace.Text = editPet.Race;
            txtAge.Text = editPet.Age.ToString();
            cboColor.Text = editPet.Color;
            cboGenService.Text= editPet.GenService;
            cboSpeService.Text = editPet.SpeService;
            txtPrice.Text = editPet.Price.ToString();
            txtOwner.Text = editPet.Owner;
            txtPhoneNo.Text = editPet.PhoneNo;          
        }*/
        //When this button is clicked, data will be transferred to the controls to be updated
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Pet editPet = (Pet)dgDisplay.SelectedItem;
            if (editPet != null)
            {
                DisplayPets.Remove(editPet);
                txtPetID.Text = editPet.PetID.ToString();
                txtPetName.Text = editPet.PetName;
                cboType.Text = editPet.Type;
                cboRace.Text = editPet.Race;
                txtAge.Text = editPet.Age.ToString();
                cboColor.Text = editPet.Color;
                cboGenService.Text = editPet.GenService;
                cboSpeService.Text = editPet.SpeService;
                txtPrice.Text = editPet.Price.ToString();
                txtOwner.Text = editPet.Owner;
                txtPhoneNo.Text = editPet.PhoneNo;
            }
            btnAdd.Content = "Update";
            btnSave.IsEnabled = false;
            btnReset.IsEnabled = false;
            btnUpdate.IsEnabled = false;
            btnDelete.IsEnabled = false;
            dgDisplay.IsEnabled = false;
        }
        //To reset or clear the controls
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        /*private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are You Sure?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var deletePet = DisplayPets.FirstOrDefault(en => en.PetID.ToString() == txtDelete.Text.ToString());
                DisplayPets.Remove(deletePet);
                

                petList.Clear();
                foreach (Pet p in DisplayPets)
                {
                    petList.Add(p);
                }
                if (WriteToFile())
                {
                    MessageBox.Show("File Deleted Successfully!", "Information");
                }
                dgDisplay.ItemsSource = DisplayPets;
            }
        }*/
        //To delete the data selected with confirmation
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Pet deletePet = (Pet)dgDisplay.SelectedItem;
            if (deletePet != null)
            {
                MessageBoxResult result = MessageBox.Show("Are You Sure, You Want to Delete This Record?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    DisplayPets.Remove(deletePet);
                    petList.Clear();
                    foreach (Pet p in DisplayPets)
                    {
                        petList.Add(p);
                    }
                    if (WriteToFile())
                    {
                        MessageBox.Show("File Deleted Successfully!", "Information");
                    }
                    dgDisplay.ItemsSource = DisplayPets;
                }
            }
            else
            {
                MessageBox.Show("No Record to Delete", "Information");
            }
        }
        //To display the type of pet in a combobox from the observable collection
        public void DisplayPetType()
        {
            ObservableCollection<PetType> petType = new ObservableCollection<PetType>();
            petType.Add(new PetType(1, "Cat"));
            petType.Add(new PetType(2, "Dog"));

            cboType.ItemsSource = petType;
            cboType.DisplayMemberPath = "Name";
        }
        //To display the colors in a combobox from the observable collection
        public void DisplayColors()
        {
            ObservableCollection<PetColor> petColor = new ObservableCollection<PetColor>();
            petColor.Add(new PetColor(1, "Black"));
            petColor.Add(new PetColor(2, "Brown"));
            petColor.Add(new PetColor(3, "Gray"));
            petColor.Add(new PetColor(4, "Orange"));
            petColor.Add(new PetColor(5, "White"));

            cboColor.ItemsSource = petColor;
            cboColor.DisplayMemberPath = "Name";
        }
        //To display the general service in a combobox from the observable collection
        public void DisplayGeneralService()
        {
            ObservableCollection<GeneralService> petGeneralService = new ObservableCollection<GeneralService>();
            petGeneralService.Add(new GeneralService(1, "Grooming"));
            petGeneralService.Add(new GeneralService(2, "Pets Hotel"));

            cboGenService.ItemsSource = petGeneralService;
            cboGenService.DisplayMemberPath = "Name";
        }
        //To display the races of cat in a combobox from the observable collection
        public void DisplayCatRaces()
        {
            ObservableCollection<CatRaces> catRaces = new ObservableCollection<CatRaces>();
            catRaces.Add(new CatRaces(1, "Munchkin"));
            catRaces.Add(new CatRaces(2, "Napoleon"));
            catRaces.Add(new CatRaces(3, "Persian"));
            catRaces.Add(new CatRaces(4, "Savannah"));
            catRaces.Add(new CatRaces(5, "Siberia"));

            cboRace.ItemsSource = catRaces;
            cboRace.DisplayMemberPath = "Name";
        }
        //To display the races of dog in a combobox from the observable collection
        public void DisplayDogRaces()
        {
            ObservableCollection<DogRaces> dogRaces = new ObservableCollection<DogRaces>();
            dogRaces.Add(new DogRaces(1, "Akita"));
            dogRaces.Add(new DogRaces(2, "Beagle"));
            dogRaces.Add(new DogRaces(3, "German Shepherd"));
            dogRaces.Add(new DogRaces(4, "Pug"));
            dogRaces.Add(new DogRaces(5, "Shih Tzu"));

            cboRace.ItemsSource = dogRaces;
            cboRace.DisplayMemberPath = "Name";
        }
        //To display the search options in a combobox from the observable collection
        public void DisplaySearchOptions()
        {
            ObservableCollection<SearchOptions> searchOptions = new ObservableCollection<SearchOptions>();
            searchOptions.Add(new SearchOptions(1, "Owner Name"));
            searchOptions.Add(new SearchOptions(2, "Pet Name"));
            searchOptions.Add(new SearchOptions(3, "Type"));

            cboSearchBy.ItemsSource = searchOptions;
            cboSearchBy.DisplayMemberPath = "Name";
        }
        //To display the grooming service in a combobox from the observable collection
        public void DisplayGroomSpeService()
        {
            ObservableCollection<GroomSpeService> groomSpeService = new ObservableCollection<GroomSpeService>();
            groomSpeService.Add(new GroomSpeService(1, "Bath and Full Haircut", 20, 25));
            groomSpeService.Add(new GroomSpeService(2, "Bath and Haircut with FURminator", 25, 30));
            groomSpeService.Add(new GroomSpeService(3, "Bath and Brush with FURminator", 23, 28));
            groomSpeService.Add(new GroomSpeService(4, "Bath and Brush", 18, 20));
            groomSpeService.Add(new GroomSpeService(5, "Bath and Trim", 12, 15));

            cboSpeService.ItemsSource = groomSpeService;
            cboSpeService.DisplayMemberPath = "Name";
        }
        //To display the hotel service in a combobox from the observable collection
        public void DisplayHotelSpeService()
        {
            ObservableCollection<HotelSpeService> hotelSpeService = new ObservableCollection<HotelSpeService>();
            hotelSpeService.Add(new HotelSpeService(1, "Standard Guest Room", 35, 40));
            hotelSpeService.Add(new HotelSpeService(2, "Private Suite", 50, 60));

            cboSpeService.ItemsSource = hotelSpeService;
            cboSpeService.DisplayMemberPath = "Name";
        }

        private void btnDelete_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void btnSaveUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        //private void btnUpdate_Click(object sender, RoutedEventArgs e)
        //{

        //        }
    }
}
