﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Entidades
{
    public class CantSociosMayoryMenorEdadDTO
    {
        public Equipo Equipo { get; set; }
        public int CantidadSocios { get; set; }
        public int SocioEdad { get; set; }
        public double EdadPromedioSocios { get; set; }
        public double EdadMinima { get; set; }
        public double EdadMaxima { get; set; }

        public CantSociosMayoryMenorEdadDTO() {
            this.Equipo = new Equipo();
        }
    }
}
