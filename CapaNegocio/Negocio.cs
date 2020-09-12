using System;
using System.Collections.Generic;
using System.Linq;
using Entidades;

namespace CapaNegocio
{
    public class Negocio
    {
        public List<NombreEdadEquipoDTO> GetCasadosConEstudios(string[] data)
        {

            List<Socio> lstSocios = new List<Socio>();

            string estadocivil = "Casado";
            string estudios = "Universitario";

            foreach (string linea in data)
            {
                var valores = linea.Split(';');

                if (valores[3] == estadocivil && valores[4] == estudios) //valores[3] = EstadoCivil | valores[4] = NiveldeEstudios
                {
                    Socio objSocio = new Socio();

                    objSocio.Nombre = valores[0];
                    objSocio.Edad = int.Parse(valores[1]);
                    objSocio.Equipo = valores[2];
                    lstSocios.Add(objSocio);
                }
            }

            List<NombreEdadEquipoDTO> lst = (from p in lstSocios
                                             group p by new { p.Nombre, p.Edad, p.Equipo } into grupo
                                             let count = grupo.Count()
                                             select new NombreEdadEquipoDTO()
                                             {
                                                 Nombre = grupo.Key.Nombre,
                                                 Edad = grupo.Key.Edad,
                                                 Equipo = grupo.Key.Equipo
                                             }).OrderBy(x => x.Edad).Take(100).ToList();
            return lst;

        }
        public List<CantSociosMayoryMenorEdadDTO> GetSociosMayoryMenorEdad(string[]data)
        {

            List<Equipo> lstEquipos = new List<Equipo>();
            List<CantSociosMayoryMenorEdadDTO> lst = new List<CantSociosMayoryMenorEdadDTO>();
            List<CantSociosMayoryMenorEdadDTO> lstretornada = new List<CantSociosMayoryMenorEdadDTO>();



            List<Socio> lstSocios = new List<Socio>();

            foreach (string linea in data)
            {
                var valores = linea.Split(';');

                Socio objSocio = new Socio();

                objSocio.Nombre = valores[0];
                objSocio.Edad = int.Parse(valores[1]);
                objSocio.Equipo = valores[2];
                objSocio.EstadoCivil = valores[3];
                objSocio.NiveldeEstudios = valores[4];
                lstSocios.Add(objSocio);

                Equipo equipo = new Equipo();
                equipo.Nombre = valores[2];
                lstEquipos.Add(equipo);
            }

            List<Equipo> listadoSinRepetidos = (from p in lstEquipos
                                                group p by new { p.Nombre } into grupo
                                                where grupo.Count() > 1
                                                select new Equipo()
                                                {
                                                    Nombre = grupo.Key.Nombre,
                                                }).ToList();

            foreach (Equipo eq in listadoSinRepetidos)
            {

                string equiponombre = eq.Nombre.ToString();
                int maxedad = int.MinValue;
                int minedad = int.MaxValue;
                double promedio = 0;
                int cont = 0;

                for (int i = lstSocios.Count - 1; i > -1; i--)
                {
                    if (lstSocios[i].Equipo == eq.Nombre)
                    {

                        if (lstSocios[i].Equipo == eq.Nombre && lstSocios[i].Edad > maxedad)
                            maxedad = lstSocios[i].Edad;

                        if (lstSocios[i].Equipo == eq.Nombre && lstSocios[i].Edad < minedad)
                            minedad = lstSocios[i].Edad;

                        promedio += lstSocios[i].Edad;
                        cont++;

                        lstSocios.RemoveAt(i);
                    }

                }

                CantSociosMayoryMenorEdadDTO socio = new CantSociosMayoryMenorEdadDTO();
                socio.Equipo = equiponombre;
                socio.EdadMaxima = maxedad;
                socio.EdadMinima = minedad;
                socio.EdadPromedioSocios = promedio / cont;
                socio.CantidadSocios = cont;
                lstretornada.Add(socio);
            }

            return lstretornada;
        }
        public List<NombresComunesDTO> GetNombresMasComunes(string[] data, string equipo)
        {

            List<NombresComunesDTO> lstSocios = new List<NombresComunesDTO>();

            foreach (string linea in data)
            {
                var valores = linea.Split(';');

                NombresComunesDTO objSocio = new NombresComunesDTO();

                if (equipo == valores[2])
                {
                    objSocio.Equipo = valores[2];
                    objSocio.Nombre = valores[0];
                    lstSocios.Add(objSocio);
                }

            }

            List<NombresComunesDTO> lstNombresRepetidos = (from p in lstSocios
                                                           group p by new { p.Nombre, p.Equipo, p.Cantidad } into grupo
                                                           let count = grupo.Count()
                                                           select new NombresComunesDTO()
                                                           {
                                                               Nombre = grupo.Key.Nombre,
                                                               Equipo = grupo.Key.Equipo,
                                                               Cantidad = count
                                                           }).OrderByDescending(x => x.Cantidad).Take(5).ToList();
            return lstNombresRepetidos;
        }
        public string GetPromEdad(string[]data, string equipo)
        {

            int sumador = 0;
            int contador = 0;
            float promedio = 0;

            foreach (string linea in data)
            {

                var valores = linea.Split(';');


                if (valores[2] == equipo) //valores[2] = Equipo
                {
                    sumador += int.Parse(valores[1]); //valores[1] = Edad
                    contador++;
                }

            }

            promedio = sumador / contador;

            return "Socios de "  + equipo + ": "+ promedio + " años de edad promedio." ;
        }
        public string GetCantAll(string[] data)
        {       
           return "Registros totales: " + data.Length +".";
        }
    }
}
