
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
        /*
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
        */
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
        public List<CantSociosMayoryMenorEdadDTO> GetDataEquipos()
        {            
            
            string ruta = Server.MapPath("~/Upload/" + FileUploadArchivo.FileName);
            string[] lineas = File.ReadAllLines(ruta);

            List<Equipo> lstEquipos = new List<Equipo>();
            List<CantSociosMayoryMenorEdadDTO> lst = new List<CantSociosMayoryMenorEdadDTO>();
            List<CantSociosMayoryMenorEdadDTO> lstretornada = new List<CantSociosMayoryMenorEdadDTO>();



            List<Socio> lstSocios = new List<Socio>();

            foreach (string linea in lineas)
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
                double promedio= 0;
                int cont =0;

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
        public List<NombresComunesDTO> NombresMasComunes(string equipo) {


            string ruta = Server.MapPath("~/Upload/" + FileUploadArchivo.FileName);
            string[] lineas = File.ReadAllLines(ruta);
            List<NombresComunesDTO> lstSocios = new List<NombresComunesDTO>();



            foreach (string linea in lineas)
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
                                               group p by new { p.Nombre, p.Equipo, p.Cantidad} into grupo
                                               let count = grupo.Count()
                                               select new NombresComunesDTO()
                                                {
                                                    Nombre = grupo.Key.Nombre,
                                                    Equipo = grupo.Key.Equipo,
                                                    Cantidad = count
                                               }).OrderByDescending(x => x.Cantidad).Take(5).ToList();
            return lstNombresRepetidos;
        }
        public void GetPromEdad(string equipo)
        {
            string ruta = Server.MapPath("~/Upload/" + FileUploadArchivo.FileName);
            string[] lineas = File.ReadAllLines(ruta);

            int sumador = 0;
            int contador = 0;
            float promedio = 0;

            foreach (string linea in lineas)
            {

                var valores = linea.Split(';');


                if (valores[2] == equipo) //valores[2] = Equipo
                {
                    sumador += int.Parse(valores[1]); //valores[1] = Edad
                    contador++;
                }
              
            }

            promedio = sumador / contador;

            MessageBox.Show(promedio + " Promedio de edad socios de " + equipo);
        }
        public void GetCantAll()
        {
            string ruta = Server.MapPath("~/Upload/" + FileUploadArchivo.FileName);
            string[] lineas = File.ReadAllLines(ruta);

            MessageBox.Show(lineas.Length + " registros totales");
        }
        void RealizarBusquedas() {
            GetCantAll(); //Obtiene la cantidad total de registros
           
            GetPromEdad("Racing"); //Obtiene el promedio de edad de los socios de Racing.

            GridViewCasadosConEstudios.DataSource = GetCasadosConEstudios(); //Obtiene los primeras 100 personas casadas y con estudios ordenados por su edad.
            GridViewCasadosConEstudios.DataBind();

            GridView2.DataSource = NombresMasComunes("River");
            GridView2.DataBind();

            GridView1.DataSource = GetDataEquipos();
            GridView1.DataBind();
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