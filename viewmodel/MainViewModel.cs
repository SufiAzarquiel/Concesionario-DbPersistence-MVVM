using Concesionario_DbPersistence_MVVM.connection;
using Concesionario_DbPersistence_MVVM.model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

            New = new Command(AccionNuevo);
            Save = new Command(AccionGuardar);
            Delete = new Command(AccionEliminar);
            Modify = new Command(AccionModificar);
            Update = new Command(AccionActualizar);
            Cancel = new Command(AccionCancelar);


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
                    EngineType = selectedCar.EngineType;
                    Stock = selectedCar.Stock;
                    Price = selectedCar.Price;
                    Year = selectedCar.Year;
                }
            }
        }

        private string brand, model;
        private Car.Engine engineType;
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
        public Car.Engine EngineType
        {
            get { return engineType; }
            set
            {
                engineType = value;
                OnPropertyChanged("EngineType");
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

        private bool controlAll;

        public bool ControlAll
        {
            get { return controlAll; }
            set
            {
                controlAll = value;
                OnPropertyChanged("ControlAll");
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


        private void AccionNuevo(object parametro)
        {

            ControlAll = true;

            ControlB1 = false;
            ControlB2 = false;
            ControlB3 = false;
            ControlB4 = true;
            ControlB5 = false;
            ControlB6 = true;


            Titulo = "";
            ISBN = "";
            Autor = "";
            Editorial = "";


        }

        private void AccionEliminar(object parametro)
        {
            if (SelectedCar != null)
            {
                MessageBoxResult ss = MessageBox.Show("¿Seguro que desea eliminar el LIBRO seleccionado?",
                        "Confirmar eliminacion de registro", MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);
                if (ss == MessageBoxResult.Yes)
                //if (MessageBox.Show("¿Seguro que desea eliminar el LIBRO seleccionado?",
                //       "Confirmar eliminacion de registro", MessageBoxButton.YesNo,
                //       MessageBoxImage.Warning) == MessageBoxResult.Yes) // Si al mostrar el cuadro de diálogo el usuario presiona el botón "yes" ...
                {

                    int resultado = 0;

                    try
                    {


                        resultado = dataConnection.DeleteCar(ISBN);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    if (resultado > 0)// Por que afectó a un registro
                    {

                        CarList.Remove(SelectedCar);

                        Titulo = "";
                        Autor = "";
                        ISBN = "";
                        Editorial = "";

                    }
                }

                SelectedCar = null;
            }
            else
            {
                MessageBox.Show("Tienes que seleccionar un LIBRO de la lista para poder borrarlo",
                    "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void AccionModificar(object parametro)
        {

            if (SelectedCar != null)
            {
                ControlAll = true;

                ControlB1 = false;
                ControlB2 = false;
                ControlB3 = false;
                ControlB4 = false;
                ControlB5 = true;
                ControlB6 = true;

            }
            else
            {
                //MessageBox.Show("Tienes que seleccionar un LIBRO de la lista para poder modificarlo",
                //    "Información", MessageBoxButton.OK, MessageBoxImage.Information);

                MessageBox.Show("Tienes que seleccionar un LIBRO de la lista para poder modificarlo");
            }


        }

        private void AccionActualizar(object parametro)
        {

            try
            {


                int resultado = 0;


                resultado = dataConnection.UpdateExistingBook(Titulo, ISBN,
                    Autor, Editorial);


                if (resultado > 0) // si se actualizó el registro en la tabla ...
                {
                    MessageBox.Show("Libro modificado", "Información", MessageBoxButton.OK, MessageBoxImage.Information);


                    // Actualizamos Listalibros para actualizar el ListBox
                    CarList = dataConnection.ObtenerLibros();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                ControlAll = false;

                ControlB1 = true;
                ControlB2 = true;
                ControlB3 = true;
                ControlB4 = false;
                ControlB5 = false;
                ControlB6 = false;


            }



        }


        private void AccionGuardar(object parametro)
        {
            Car nuevoLibro;



            try
            {

                nuevoLibro = new Car();

                nuevoLibro.Titulo = Titulo;
                nuevoLibro.Isbn = ISBN;
                nuevoLibro.Autor = Autor;
                nuevoLibro.Editorial = Editorial;

                dataConnection.GuardarNuevoLibro(nuevoLibro);

                CarList.Add(nuevoLibro);



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                ControlAll = false;

                ControlB1 = true;
                ControlB2 = true;
                ControlB3 = true;
                ControlB4 = false;
                ControlB5 = false;
                ControlB6 = false;

            }


        }

        private void AccionCancelar(object parametro)
        {

            SelectedCar = null;

            Titulo = "";
            ISBN = "";
            Autor = "";
            Editorial = "";

            ControlAll = false;
            ControlB1 = true;
            ControlB2 = true;
            ControlB3 = true;
            ControlB4 = false;
            ControlB5 = false;
            ControlB6 = false;



        }
    }
}

