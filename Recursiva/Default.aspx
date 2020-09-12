<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Recursiva._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>

        <h1>Transformación de datos</h1>
           Seleccione un archivo CSV.
                 
            <asp:FileUpload ID="FileUploadArchivo" runat="server" Height="31px" Width="768px"/>
      
            <asp:Button ID="ButtonUploadArchivo"  class="btn btn-primary btn-lg" runat="server" OnClick="ButtonUploadArchivo_Click" Text="Subir Archivo" />
        
        <asp:Label ID="LabelEstado" runat="server"></asp:Label>
        
    </div>

    <div>   
        <p> 
        <asp:Label ID="LabelEdadPromedioEquipo" runat="server" Font-Bold="True"></asp:Label>
        <asp:Label ID="LabelCantRegistrosTotales" runat="server" Font-Bold="True"></asp:Label>
        </p>  
    </div>


    <div class="row">

        <div class="col-md-6">
            <asp:Label ID="LabelSociosMayoryMenorEdad" runat="server" Font-Bold="True"></asp:Label>
            <asp:GridView ID="GridViewSociosMayoryMenorEdad" runat="server">
            </asp:GridView>  
        </div>


        <div class="col-md-4">  
            <asp:Label ID="LabelCasadosConEstudios" runat="server" Font-Bold="True"></asp:Label>
            <asp:GridView ID="GridViewCasadosConEstudios" runat="server">
            </asp:GridView>    
        </div>

        <div class="col-md-2">
            <asp:Label ID="LabelNombresMasComunes" runat="server" Font-Bold="True"></asp:Label>
            <asp:GridView ID="GridViewNombresMasComunes" runat="server">
            </asp:GridView>
        </div>

    </div>

</asp:Content>
