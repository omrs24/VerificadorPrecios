using Acr.UserDialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerificadorPrecios.Api;
using VerificadorPrecios.Models;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VerificadorPrecios.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Categoria_Picker : Popup
    {
        public Categoria_Picker()
        {
            InitializeComponent();

            Opened += Categoria_Picker_Opened;
        }

        private async void Categoria_Picker_Opened(object sender, PopupOpenedEventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Cargando informacion...");

            var result = await getMethods.CategoriasGetAsync();

            if (result[0] != "OK")
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Error al obtener las marcas, intente mas tarde. \nCode: {result[0]} \nError: {result[1]}", "Cerrar");
                return;
            }

            lstCategorias.ItemsSource = JsonConvert.DeserializeObject<List<Categorias>>(result[1]);

            UserDialogs.Instance.HideLoading();
        }

        private void lstCategorias_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Dismiss(e.SelectedItem);
        }

        async private void btnBuscar_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Cargando informacion...");

                var result = await getMethods.CategoriaSearchAsync(txtBuscar.Text);

                if (result[0] != "OK")
                {
                    //await App.Current.MainPage.DisplayAlert("Error", $"Error al buscar los proveedores, intente mas tarde. \nCode: {result[0]} \nError: {result[1]}", "Cerrar");
                    await App.Current.MainPage.DisplayAlert("", $"Sin coincidencias", "Cerrar");
                    UserDialogs.Instance.HideLoading();
                    return;
                }

                lstCategorias.ItemsSource = JsonConvert.DeserializeObject<List<Categorias>>(result[1]);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Error al obtener las categorias, intente mas tarde. \n Error: {ex.Message.ToString()}", "Cerrar");
                UserDialogs.Instance.HideLoading();
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Categorias cat = new Categorias();
            Dismiss(cat);
        }
    }
}