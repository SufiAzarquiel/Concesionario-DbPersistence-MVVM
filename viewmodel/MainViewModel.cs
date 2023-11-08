using Concesionario_DbPersistence_MVVM.connection;
using Concesionario_DbPersistence_MVVM.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Concesionario_DbPersistence_MVVM.viewmodel
{
    public class MainViewModel : INotifyPropertyChanged
    {

        private Connection dataConnection;

        public ICommand New { get; set; }
        public ICommand Save { get; set; }
        public ICommand Delete { get; set; }
        public ICommand Modify { get; set; }
        public ICommand Update { get; set; }
        public ICommand Cancel { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public MainViewModel()
        {
            dataConnection = new Connection();

            CarList = dataConnection.GetCars();


            ControlB1 = true;
            ControlB2 = true;
            ControlB3 = true;

            New = new Command(ActionNew);
            Save = new Command(ActionSave);
            Delete = new Command(ActionDelete);
            Modify = new Command(ActionModify);
            Update = new Command(ActionUpdate);
            Cancel = new Command(ActionCancel);


        }

        private ObservableCollection<Car> carList;

        public ObservableCollection<Car> CarList
        {
            get { return carList; }
            set
            {
                carList = value;
                OnPropertyChanged("CarList");
            }
        }

        private Car selectedCar;
        public Car SelectedCar
        {
            get { return selectedCar; }
            set
            {
                selectedCar = value;
                OnPropertyChanged("SelectedCar");

                if (selectedCar != null)

                {
                    Brand = selectedCar.Brand;
                    Model = selectedCar.Model;
                    SelectedEngine = selectedCar.EngineType;
                    Stock = selectedCar.Stock;
                    Price = selectedCar.Price;
                    Year = selectedCar.Year;
                }
            }
        }

        private string brand, model;
        private int stock, year;
        private double price;
        public string Brand
        {
            get { return brand; }
            set
            {
                brand = value;
                OnPropertyChanged("Brand");
            }
        }
        public string Model
        {
            get { return model; }
            set
            {
                model = value;
                OnPropertyChanged("Model");
            }
        }

        // COMBOBOX
        public List<Car.Engine> EngineTypes { get; } = Enum.GetValues(typeof(Car.Engine)).OfType<Car.Engine>().ToList();

        private Car.Engine selectedEngine;
        public Car.Engine SelectedEngine
        {
            get { return selectedEngine; }
            set
            {
                selectedEngine = value;
                Debug.WriteLine(selectedEngine);
                OnPropertyChanged("SelectedEngine");
            }
        }

        public int Stock
        {
            get { return stock; }
            set
            {
                stock = value;
                OnPropertyChanged("Stock");
            }
        }
        public double Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged("Price");
            }
        }
        public int Year
        {
            get { return year; }
            set
            {
                year = value;
                OnPropertyChanged("Year");
            }
        }

        private bool controlText;

        public bool ControlText
        {
            get { return controlText; }
            set
            {
                controlText = value;
                OnPropertyChanged("ControlText");
            }
        }


        private bool controlB1;

        public bool ControlB1
        {
            get { return controlB1; }
            set
            {
                controlB1 = value;
                OnPropertyChanged("ControlB1");
            }
        }

        private bool controlB2;

        public bool ControlB2
        {
            get { return controlB2; }
            set
            {
                controlB2 = value;
                OnPropertyChanged("ControlB2");
            }
        }
        private bool controlB3;
        public bool ControlB3
        {
            get { return controlB3; }
            set
            {
                controlB3 = value;
                OnPropertyChanged("ControlB3");
            }
        }

        private bool controlB4;
        public bool ControlB4
        {
            get { return controlB4; }
            set
            {
                controlB4 = value;
                OnPropertyChanged("ControlB4");
            }
        }

        private bool controlB5;
        public bool ControlB5
        {
            get { return controlB5; }
            set
            {
                controlB5 = value;
                OnPropertyChanged("ControlB5");
            }
        }

        private bool controlB6;
        public bool ControlB6
        {
            get { return controlB6; }
            set
            {
                controlB6 = value;
                OnPropertyChanged("ControlB6");
            }
        }


        private void ActionNew(object parameter)
        {

            ControlText = true;

            ControlB1 = false;
            ControlB2 = false;
            ControlB3 = false;
            ControlB4 = true;
            ControlB5 = false;
            ControlB6 = true;


            Brand = "";
            Model = "";
            SelectedEngine = Car.Engine.Gasoline;
            Stock = 0;
            Price = 0;
            Year = 0;


        }

        private void ActionDelete(object parameter)
        {
            if (SelectedCar != null)
            {
                MessageBoxResult ss = MessageBox.Show("Are you sure you want to delete the selected CAR?",
                        "Confirm record deletion", MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);
                if (ss == MessageBoxResult.Yes)
                //if (MessageBox.Show("¿Seguro que desea eliminar el LIBRO seleccionado?",
                //       "Confirmar eliminacion de registro", MessageBoxButton.YesNo,
                //       MessageBoxImage.Warning) == MessageBoxResult.Yes) // Si al mostrar el cuadro de diálogo el usuario presiona el botón "yes" ...
                {

                    int result = 0;

                    try
                    {
                        result = dataConnection.DeleteCar(Brand, Model);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    if (result > 0)// Por que afectó a un registro
                    {

                        CarList.Remove(SelectedCar);

                        Brand = "";
                        Model = "";
                        SelectedEngine = Car.Engine.Gasoline;
                        Stock = 0;
                        Price = 0;
                        Year = 0;

                    }
                }

                SelectedCar = null;
            }
            else
            {
                MessageBox.Show("To delete a CAR, select one first.",
                    "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void ActionModify(object parameter)
        {

            if (SelectedCar != null)
            {
                ControlText = true;

                ControlB1 = false;
                ControlB2 = false;
                ControlB3 = false;
                ControlB4 = false;
                ControlB5 = true;
                ControlB6 = true;

            }
            else
            {
                MessageBox.Show("To modify a CAR, select one first.");
            }


        }

        private void ActionUpdate(object parameter)
        {

            try
            {


                int result = 0;

                result = dataConnection.UpdateExistingCar(Brand, Model, SelectedEngine.ToString(), Stock, Price, Year);


                if (result > 0) // si se actualizó el registro en la tabla ...
                {
                    MessageBox.Show("CAR modified", "Info", MessageBoxButton.OK, MessageBoxImage.Information);


                    // Actualizamos Listalibros para actualizar el ListBox
                    CarList = dataConnection.GetCars();
                }
                else
                {
                    MessageBox.Show("CAR not modified!", "Info", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                ControlText = false;

                ControlB1 = true;
                ControlB2 = true;
                ControlB3 = true;
                ControlB4 = false;
                ControlB5 = false;
                ControlB6 = false;


            }



        }


        private void ActionSave(object parameter)
        {
            Car newCar;



            try
            {

                newCar = new Car();

                newCar.Brand = Brand;
                newCar.Model = Model;
                newCar.EngineType = SelectedEngine;
                newCar.Stock = Stock;
                newCar.Price = Price;
                newCar.Year = Year;

                dataConnection.SaveNewCar(newCar);

                CarList.Add(newCar);



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                ControlText = false;

                ControlB1 = true;
                ControlB2 = true;
                ControlB3 = true;
                ControlB4 = false;
                ControlB5 = false;
                ControlB6 = false;

            }


        }

        private void ActionCancel(object parameter)
        {

            SelectedCar = null;

            Brand = "";
            Model = "";
            SelectedEngine = Car.Engine.Gasoline;
            Stock = 0;
            Price = 0;
            Year = 0;

            ControlText = false;
            ControlB1 = true;
            ControlB2 = true;
            ControlB3 = true;
            ControlB4 = false;
            ControlB5 = false;
            ControlB6 = false;



        }
    }
}

