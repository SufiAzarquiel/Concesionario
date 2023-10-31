namespace Concesionario.model
{
    public class Coche
    {
        public string Marca { get; set; }

        public string Modelo { get; set; }

        public enum Motor { Gasolina, Diesel, Hibrido, Electrico };
        public Motor TipoMotor { get; set; }
        public int Stock { get; set; }
        public double Precio { get; set; }
        public int Año { get; set; }
    }
}
