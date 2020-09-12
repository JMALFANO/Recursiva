
using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Recursiva
{



    public partial class _Default : Page
    {

        public List<Socio> GetAll()
        {
            string ruta = Server.MapPath("~/Upload/" + FileUploadArchivo.FileName);
            string[] lineas = File.ReadAllLines(ruta);
            List<Socio> lstSocios = new List<Socio>();


            foreach (string linea in lineas)
            {
                var valores = linea.Split(';');

                Socio objSocio = new Socio();

                objSocio.Nombre = valores[0];
                objSocio.Edad = int.Parse(valores[1]);
                objSocio.Equipo.Nombre = valores[2];
                objSocio.EstadoCivil = valores[3];
                objSocio.NiveldeEstudios = valores[4];
                lstSocios.Add(objSocio);
            }

            return lstSocios;
        }

        public List<NombreEdadEquipoDTO> GetCasadosConEstudios()
        {
            string ruta = Server.MapPath("~/Upload/" + FileUploadArchivo.FileName);
            string[] lineas = File.ReadAllLines(ruta);
            List<Socio> lstSocios = new List<Socio>();

            string estadocivil = "Casado";
            string estudios = "Universitario";

            foreach (string linea in lineas)
            {
                var valores = linea.Split(';');

                if (valores[3] == estadocivil && valores[4] == estudios) //valores[3] = EstadoCivil | valores[4] = NiveldeEstudios
                {
                    Socio objSocio = new Socio();

                    objSocio.Nombre = valores[0];
                    objSocio.Edad = int.Parse(valores[1]);
                    objSocio.Equipo.Nombre = valores[2];
                    lstSocios.Add(objSocio);
                }
            }

            List<Socio> sociosPorEdad = lstSocios.OrderBy(user => user.Edad).Take(100).ToList();

            List<NombreEdadEquipoDTO> socioFiltrado = new List<NombreEdadEquipoDTO>();
           
                foreach (Socio socio in sociosPorEdad)
                {

                    NombreEdadEquipoDTO nuevosocio = new NombreEdadEquipoDTO();

                    nuevosocio.Nombre = socio.Nombre;
                    nuevosocio.Edad = socio.Edad;
                    nuevosocio.Equipo = socio.Equipo;

                    socioFiltrado.Add(nuevosocio);

                }
            



            return socioFiltrado;

        }

        /*
        public List<CantSociosMayoryMenorEdadDTO> GetCantSociosMAyoryMenorEdad() {
            string ruta = Server.MapPath("~/Upload/" + FileUploadArchivo.FileName);
            string[] lineas = File.ReadAllLines(ruta);
            List<Socio> lstSocios = new List<Socio>();
            List<CantSociosMayoryMenorEdadDTO> lst = new List<CantSociosMayoryMenorEdadDTO>();

          

            foreach (string linea in lineas)
            {
                var valores = linea.Split(';');

                    Socio objSocio = new Socio();
                    objSocio.Nombre = valores[0];
                    objSocio.Edad = int.Parse(valores[1]);
                    objSocio.Equipo.Nombre = valores[2];
                    lstSocios.Add(objSocio);
               
            }

          

            return lstSocios;
        }*/

        public List<CantSociosMayoryMenorEdadDTO> GetDataEquipos()
        {            string ruta = Server.MapPath("~/Upload/" + FileUploadArchivo.FileName);
            string[] lineas = File.ReadAllLines(ruta);

            List<Equipo> lstEquipos = new List<Equipo>();
            List<CantSociosMayoryMenorEdadDTO> lst = new List<CantSociosMayoryMenorEdadDTO>();
            List<CantSociosMayoryMenorEdadDTO> lstretornada = new List<CantSociosMayoryMenorEdadDTO>();
            foreach (string linea in lineas)
            {
                var valores = linea.Split(';');

                CantSociosMayoryMenorEdadDTO objSocio = new CantSociosMayoryMenorEdadDTO();            
                objSocio.SocioEdad = int.Parse(valores[1]);
                lst.Add(objSocio);

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


            CantSociosMayoryMenorEdadDTO socio = new CantSociosMayoryMenorEdadDTO();
            
            foreach (Equipo eq in listadoSinRepetidos)
            {
                var edadMinima = lst.Min(x => x.SocioEdad);
                var edadMaxima = lst.Max(x => x.SocioEdad);
                var edadPromedio = lst.Average(x => x.SocioEdad);
                socio.EdadPromedioSocios = double.Parse(edadPromedio.ToString());
                socio.EdadMinima= double.Parse(edadMinima.ToString());
                socio.EdadMaxima= double.Parse(edadMaxima.ToString());
                socio.Equipo.Nombre = eq.Nombre;
                lstretornada.Add(socio);
            }

            
       
            return lstretornada;
        }
        public List<NombresComunesRiverDTO> NombresMasComunesRiver() {


            string ruta = Server.MapPath("~/Upload/" + FileUploadArchivo.FileName);
            string[] lineas = File.ReadAllLines(ruta);
            List<NombresComunesRiverDTO> lstSocios = new List<NombresComunesRiverDTO>();


            foreach (string linea in lineas)
            {
                var valores = linea.Split(';');

                NombresComunesRiverDTO objSocio = new NombresComunesRiverDTO();
                objSocio.Nombre = valores[0];

                lstSocios.Add(objSocio);
            }

            List<NombresComunesRiverDTO> lstNombresRepetidos = (from p in lstSocios
                                               group p by new { p.Nombre } into grupo
                                               let count = grupo.Count()
                                               select new NombresComunesRiverDTO()
                                                {
                                                    Nombre = grupo.Key.Nombre,
                                                }).Take(5).ToList();
            return lstNombresRepetidos;
        }

        public void GetPromEdadRacing()
        {
            string ruta = Server.MapPath("~/Upload/" + FileUploadArchivo.FileName);
            string[] lineas = File.ReadAllLines(ruta);

            string filto = "Racing";
            int sumador = 0;
            int contador = 0;
            float promedio = 0;

            foreach (string linea in lineas)
            {

                var valores = linea.Split(';');


                if (valores[2] == filto) //valores[2] = Equipo
                {
                    sumador += int.Parse(valores[1]); //valores[1] = Edad
                    contador++;
                }
              
            }

            promedio = sumador / contador;

            MessageBox.Show(promedio + " Promedio de edad socios de Racing");
        }

        public void GetCantAll()
        {
            string ruta = Server.MapPath("~/Upload/" + FileUploadArchivo.FileName);
            string[] lineas = File.ReadAllLines(ruta);

            MessageBox.Show(lineas.Length + " registros totales");
        }

        void RealizarBusquedas() {
            GetCantAll(); //Obtiene la cantidad total de registros
           
            GetPromEdadRacing(); //Obtiene el promedio de edad de los socios de Racing.



          GridViewCasadosConEstudios.DataSource = GetCasadosConEstudios(); //Obtiene los primeras 100 personas casadas y con estudios ordenados por su edad.
          GridViewCasadosConEstudios.DataBind();


          GridView1.DataSource = GetDataEquipos();
          GridView1.DataBind();

            GridView2.DataSource = NombresMasComunesRiver();
            GridView2.DataBind();


        }

        protected void ButtonUploadArchivo_Click(object sender, EventArgs e)
        {

            //Verificamos si se seleccionó un archivo
            if (FileUploadArchivo.HasFile)
            {
               
                //Obtiene la extension del archivo seleccionado
                string ext = System.IO.Path.GetExtension(FileUploadArchivo.FileName);

                ext = ext.ToLower();

                //Tamaño en bytes
                int tam = FileUploadArchivo.PostedFile.ContentLength;

                Response.Write(ext + "," + tam);
                if (ext == ".csv" && tam <= 5000000)
                {
                    FileUploadArchivo.SaveAs(Server.MapPath("~/Upload/" + FileUploadArchivo.FileName));
                    Response.Write("Archivo subido correctamente");

                    RealizarBusquedas();
                    
                }                   
            }
            else
            {
                Response.Write("Seleccione un archivo a subir");
            }
        }

    }
}