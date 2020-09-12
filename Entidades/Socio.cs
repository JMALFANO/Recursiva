using System;
using System.Collections.Generic;

namespace Entidades
{
    public class Socio
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public Equipo Equipo { get; set; }
        public string EstadoCivil { get; set; }
        public string NiveldeEstudios { get; set; }

        public Socio() {
            this.Equipo = new Equipo();
        }
    }
}
