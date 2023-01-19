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
    public partial class Marca_picker : Popup
    {
        public Marca_picker()
        {
            InitializeComponent();

            Opened += Marca_picker_Opened;

        }

        private async void Marca_picker_Opened(object sender, PopupOpenedEventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Cargando informacion...");

            var result = await getMethods.MarcasGetAsync();

            if (result[0] != "OK")
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Error al obtener las marcas, intente mas tarde. \nCode: {result[0]} \nError: {result[1]}", "Cerrar");
                return;
            }

            lstMarcas.ItemsSource = JsonConvert.DeserializeObject<List<Marcas>>(result[1]);

            UserDialogs.Instance.HideLoading();
        }

        private void lstMarcas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Dismiss(e.SelectedItem);
        }

        private async void btnBuscar_Clicked(object sender, EventArgs e)
        {

            try
            {
                UserDialogs.Instance.ShowLoading("Cargando informacion...");

                var result = await getMethods.MarcaSearchAsync(txtBuscar.Text);

                if (result[0] != "OK")
                {
                    //await App.Current.MainPage.DisplayAlert("Error", $"Error al obtener las marcas, intente mas tarde. \nCode: {result[0]} \nError: {result[1]}", "Cerrar");
                    await App.Current.MainPage.DisplayAlert("", $"Sin coincidencias", "Cerrar");
                    UserDialogs.Instance.HideLoading();
                    return;
                }

                lstMarcas.ItemsSource = JsonConvert.DeserializeObject<List<Marcas>>(result[1]);

                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Error al obtener las marcas, intente mas tarde. \n Error: {ex.Message.ToString()}", "Cerrar");
                UserDialogs.Instance.HideLoading();
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Marcas marca = new Marcas();
            Dismiss(marca);
        }
    }
}