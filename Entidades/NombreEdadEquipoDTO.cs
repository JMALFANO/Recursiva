using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class NombreEdadEquipoDTO
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public Equipo Equipo { get; set; }

        public NombreEdadEquipoDTO(){
            this.Equipo = new Equipo();
        }
    }
}
