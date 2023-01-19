using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerificadorPrecios.Api;
using VerificadorPrecios.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VerificadorPrecios.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarProveedor : ContentPage
    {
        public RegistrarProveedor()
        {
            InitializeComponent();
        }

        async private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Cargando informacion...");
            Proveedores prov = new Proveedores()
            {
                name = txtNombre.Text,
                email = txtEmail.Text,
                telefono = txtTel.Text,
                address = txtDireccion.Text,
                remarks = txtComentarios.Text,
            };

            var response = await postMethods.CreateProveedor(prov);

            if (response[0] != "201")
            {
                await DisplayAlert("Error", $"Error al crear proveedor, intente mas tarde. \nCode: {response[0]} \nError: {response[1]}", "Cerrar");
                UserDialogs.Instance.HideLoading();
                return;
            }


            await DisplayAlert("", $"Proveedor creado correctamente codigo: {response[1]}", "Cerrar");
            UserDialogs.Instance.HideLoading();
        }
    }
}