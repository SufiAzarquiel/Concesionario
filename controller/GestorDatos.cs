using Concesionario.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Concesionario.controller
{
    internal class GestorDatos
    {
        private const string archivoDatos = "coches.csv";

        public Coche[] LeerTodosLosRegistros()
        {
            if (!File.Exists(archivoDatos))
                return Array.Empty<Coche>();

            string[] registros = File.ReadAllLines(archivoDatos);
            Coche[] resultado = new Coche[registros.Length];

            for (int i = 0; i < registros.Length; i++)
            {
                // Dividir en campos de un csv
                string[] campos = registros[i].Split(';');

                var coche = new Coche
                {
                    Modelo = campos[0],
                    Marca = campos[1],
                    TipoMotor = (Coche.Motor)int.Parse(campos[2]),
                    Stock = int.Parse(campos[3]),
                    Precio = double.Parse(campos[4]),
                    Año = int.Parse(campos[5])
                };
                resultado[i] = coche;
            }
            return resultado;
        }

        public void Guardar(IEnumerable<Coche> coches)
        {
            StringBuilder builder = new();

            foreach (Coche c in coches)
            {
                string registro = string.Format("{0};{1};{2};{3};{4};{5}",
                                                     c.Modelo, c.Marca, (int)c.TipoMotor,
                                                     c.Stock, c.Precio, c.Año);
                builder.AppendLine(registro);
            }


            File.WriteAllText(archivoDatos, builder.ToString());

        }

    }
}
