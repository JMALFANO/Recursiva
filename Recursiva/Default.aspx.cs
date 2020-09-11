using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Compilation;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.WebPages;
using System.Windows.Forms;
using Entidades;

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
                objSocio.Equipo = valores[2];
                objSocio.EstadoCivil = valores[3];
                objSocio.NiveldeEstudios = valores[4];
                lstSocios.Add(objSocio);
            }
        
            return lstSocios;
        }

        public List<Socio> GetCasadosConEstudios()
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
            List<Socio> usersByAge = lstSocios.OrderBy(user => user.Edad).ToList();
            return usersByAge;
        }
               
        public void GetCantAll()
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

        public void GetPromEdadRacing()
        {
            string ruta = Server.MapPath("~/Upload/" + FileUploadArchivo.FileName);
            string[] lineas = File.ReadAllLines(ruta);

            MessageBox.Show(lineas.Length + " registros totales");
        }





        void RealizarBusquedas() {
            GetCantAll(); //Obtiene la cantidad total de registros
            GetPromEdadRacing(); //Obtiene el promedio de edad de los socios de Racing.
      //      GetCasadosConEstudios(); //Obtiene los primeras 100 personas casadas y con estudios ordenados por su edad.

           GridViewCasadosConEstudios.DataSource = GetCasadosConEstudios();
            GridViewCasadosConEstudios.DataBind();


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