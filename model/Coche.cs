namespace Concesionario.model
{
    internal class Coche
    {
        private string _marca;
        private string _modelo;
        public enum Motor { Gasolina, Diesel, Hibrido, Electrico };
        private Motor _motor;
        private int _stock;
        private double _precio;
        private int _año;

        public Coche(string marca, string modelo, Motor motor, int stock, double precio, int año)
        {
            _marca = marca;
            _modelo = modelo;
            _motor = motor;
            _stock = stock;
            _precio = precio;
            _año = año;
        }

        public string Marca
        {
            get { return _marca; }
            set { _marca = value; }
        }

        public string Modelo
        {
            get { return _modelo; }
            set { _modelo = value; }
        }

        public Motor TipoMotor
        {
            get { return _motor; }
            set { _motor = value; }
        }

        public int Stock
        {
            get { return _stock; }
            set { _stock = value; }
        }

        public double Precio
        {
            get { return _precio; }
            set { _precio = value; }
        }

        public int Año
        {
            get { return _año; }
            set { _año = value; }
        }

        public override string ToString()
        {
            return _marca + " " + _modelo + " " + _motor + " " + _stock + " " + _precio + " " + _año;
        }
    }
}
