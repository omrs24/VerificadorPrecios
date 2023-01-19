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
    public partial class RegistrarCategoria : ContentPage
    {
        public RegistrarCategoria()
        {
            InitializeComponent();
        }

        async private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Cargando informacion...");
            Categorias cat = new Categorias()
            {
                catName = txtNombre.Text,
                remarks = txtComentarios.Text,
            };

            var response = await postMethods.CreateCategoria(cat);

            if (response[0] != "201")
            {
                await DisplayAlert("Error", $"Error al crear categoria, intente mas tarde. \nCode: {response[0]} \nError: {response[1]}", "Cerrar");
                UserDialogs.Instance.HideLoading();
                return;
            }


            await DisplayAlert("", $"Categoria creada correctamente codigo: {response[1]}", "Cerrar");
            UserDialogs.Instance.HideLoading();
        }
    }
}