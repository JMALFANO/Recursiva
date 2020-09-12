<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Recursiva._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Transformación de datos</h1>
        <p class="lead">Seleccione un archivo CSV.<asp:FileUpload ID="FileUploadArchivo" runat="server" Width="1079px" />
        </p>
            <asp:Button ID="ButtonUploadArchivo"  class="btn btn-primary btn-lg" runat="server" OnClick="ButtonUploadArchivo_Click" Text="Subir Archivo" />
        
    </div>

    <div class="row">
        <div class="col-md-4">
    
        </div>
        <div class="col-md-4">


            
            <asp:GridView ID="GridViewCasadosConEstudios" runat="server">
            </asp:GridView>

            <asp:GridView ID="GridViewCantSociosMayoryMenorEdad" runat="server">
            </asp:GridView>

            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>

            <asp:GridView ID="GridView2" runat="server">
            </asp:GridView>

        </div>
        <div class="col-md-4">
    
        </div>
    </div>

</asp:Content>
