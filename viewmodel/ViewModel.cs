using Concesionario.controller;
using Concesionario.model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;


namespace Concesionario.viewmodel
{
    public class MainViewModel : INotifyPropertyChanged
    {

        private GestorDatos gestorDatos;

        private ObservableCollection<Coche> listaCoches;

        public ObservableCollection<Coche> ListaCoches
        {
            get { return listaCoches; }
            set
            {
                listaCoches = value;
                OnPropertyChanged("ListaCoches");
            }
        }

        private string marca;
        private string modelo;
        private Coche.Motor motor;
        private int stock;
        private double precio;
        private int año;

        public string Marca
        {
            get { return marca; }
            set
            {
                marca = value;
                OnPropertyChanged("Marca");
            }
        }
        public string Modelo
        {
            get { return modelo; }
            set
            {
                modelo = value;
                OnPropertyChanged("Modelo");
            }
        }
        public Coche.Motor TipoMotor
        {
            get { return motor; }
            set
            {
                motor = value;
                OnPropertyChanged("Motor");
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
        public double Precio
        {
            get { return precio; }
            set
            {
                precio = value;
                OnPropertyChanged("Precio");
            }
        }
        public int Año
        {
            get { return año; }
            set
            {
                año = value;
                OnPropertyChanged("Año");
            }
        }


        private Coche cocheSeleccionado;
        public Coche CocheSeleccionado
        {
            get { return cocheSeleccionado; }
            set
            {
                cocheSeleccionado = value;
                OnPropertyChanged("CocheSeleccionado");

                if (cocheSeleccionado == null)
                    ActivarEliminacionYEdicion = false;
                else
                {
                    ActivarEliminacionYEdicion = true;

                    Modelo = cocheSeleccionado.Modelo;

                    Marca = cocheSeleccionado.Marca;

                    TipoMotor = cocheSeleccionado.TipoMotor;

                    Stock = cocheSeleccionado.Stock;

                    Precio = cocheSeleccionado.Precio;

                    Año = cocheSeleccionado.Año;
                }
            }
        }

        private bool activarEliminacionYEdicion;

        public bool ActivarEliminacionYEdicion
        {
            get { return activarEliminacionYEdicion; }
            set
            {
                activarEliminacionYEdicion = value;
                OnPropertyChanged("ActivarEliminacionYEdicion");
            }
        }

        public ICommand ComandoNuevoCoche { get; set; }
        public ICommand ComandoEliminarCoche { get; set; }
        public ICommand ComandoGuardarTodo { get; set; }

        public MainViewModel()
        {
            gestorDatos = new GestorDatos();

            Coche[] coches = gestorDatos.LeerTodosLosRegistros();
            ListaCoches = new ObservableCollection<Coche>(coches);

            ComandoNuevoCoche = new Command(AccionNuevoCoche);
            ComandoEliminarCoche = new Command(AccionEliminarCoche);
            ComandoGuardarTodo = new Command(AccionGuardarTodo);
        }

        private void AccionGuardarTodo(object parametro)
        {
            gestorDatos.Guardar(ListaCoches);
        }

        private void AccionEliminarCoche(object parametro)
        {
            if (CocheSeleccionado != null)
            {
                ListaCoches.Remove(CocheSeleccionado);

                Modelo = "";
                Marca = "";
                TipoMotor = Coche.Motor.Gasolina;
                Stock = 0;
                Precio = 0.0;
                Año = 0;
            }
        }



        private void AccionNuevoCoche(object parametro)
        {
            if (Modelo == "" ||
                Marca == "" ||
                Stock == 0 ||
                Precio == 0.0 ||
                Año == 0)
            {
                // Show error window
                ShowWarning("Modelo, Marca, Stock, Precio y Año son campos obligatorios");
            }
            else
            {
                if (CocheSeleccionado != null)
                {
                    CocheSeleccionado.Modelo = Modelo;
                    CocheSeleccionado.Marca = Marca;
                    CocheSeleccionado.TipoMotor = TipoMotor;
                    CocheSeleccionado.Stock = Stock;
                    CocheSeleccionado.Precio = Precio;
                    CocheSeleccionado.Año = Año;

                    //Notificamos que el elemento ha sido modificado
                    OnPropertyChanged("CocheSeleccionado");

                    //Refrescamos el LisBox después de modificar el elemento modificado
                    gestorDatos.Guardar(ListaCoches);
                    Coche[] coches = gestorDatos.LeerTodosLosRegistros();
                    ListaCoches = new ObservableCollection<Coche>(coches);

                }
                else
                {
                    Coche coche = new Coche()
                    {
                        Marca = Marca,
                        Modelo = Modelo,
                        TipoMotor = TipoMotor,
                        Stock = Stock,
                        Precio = Precio,
                        Año = Año
                    };
                    ListaCoches.Add(coche);
                }
                EmptyFields();
            }
        }

        private void EmptyFields()
        {
            Modelo = "";
            Marca = "";
            TipoMotor = Coche.Motor.Gasolina;
            Stock = 0;
            Precio = 0.0;
            Año = 0;
        }

        private static void ShowWarning(string message)
        {
            MessageBox.Show(message, "Input Error!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}