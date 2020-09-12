
using CapaNegocio;
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
        void RealizarBusquedas(string [] data) {

            Negocio negocio = new Negocio();

            LabelCantRegistrosTotales.Text = negocio.GetCantAll(data); //Obtiene la cantidad total de registros
            LabelEdadPromedioEquipo.Text= negocio.GetPromEdad(data, "Racing"); //Obtiene el promedio de edad de los socios de un equipo.

            GridViewCasadosConEstudios.DataSource = negocio.GetCasadosConEstudios(data); //Obtiene los primeras 100 personas casadas y con estudios ordenados por su edad.
            GridViewCasadosConEstudios.DataBind();

            LabelCasadosConEstudios.Text = "Listado de socios casados con estudios universitarios.";

            GridViewNombresMasComunes.DataSource = negocio.GetNombresMasComunes(data, "River"); //Obtiene los 5 nombres más comunes de un equipo
            GridViewNombresMasComunes.DataBind();

            LabelNombresMasComunes.Text = "Listado de nombres más comunes por equipo";

            GridViewSociosMayoryMenorEdad.DataSource = negocio.GetSociosMayoryMenorEdad(data); //Obtiene la edad promedio de todos los socios de un equipo, su edad mayor y menor registrada.
            GridViewSociosMayoryMenorEdad.DataBind();

            LabelSociosMayoryMenorEdad.Text = "Listado de socios: Promedio de edad. Mayor y Menor edad registrada";

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

                if (ext == ".csv" && tam <= 5000000)
                {
                    FileUploadArchivo.SaveAs(Server.MapPath("~/Upload/" + FileUploadArchivo.FileName));
                    LabelEstado.Text = "Archivo subido correctamente";

                    string ruta = Server.MapPath("~/Upload/" + FileUploadArchivo.FileName);
                    string[] data = File.ReadAllLines(ruta);

                    RealizarBusquedas(data);
                }
                else
                {
                    LabelEstado.Text = "Formato de archivo o tamaño no soportado.";
                }
            }
            else
            {
                LabelEstado.Text = "Seleccione un archivo a subir";
            }
        }
    }
}