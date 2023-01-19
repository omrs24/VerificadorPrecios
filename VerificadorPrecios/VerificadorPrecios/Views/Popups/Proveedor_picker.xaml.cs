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
    public partial class Proveedor_picker : Popup
    {
        public Proveedor_picker()
        {
            InitializeComponent();

            Opened += Proveedor_picker_Opened;
        }

        private async void Proveedor_picker_Opened(object sender, PopupOpenedEventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Cargando informacion...");
            
            var result = await getMethods.ProveedoresGetAsync();

            if (result[0] != "OK")
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Error al obtener los productos, intente mas tarde. \nCode: {result[0]} \nError: {result[1]}", "Cerrar");
                return;
            }

            lstProveedores.ItemsSource = JsonConvert.DeserializeObject<List<Proveedores>>(result[1]);

            UserDialogs.Instance.HideLoading();
        }

        private void lstProveedores_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Dismiss(e.SelectedItem);
        }

        private async void btnBuscar_Clicked(object sender, EventArgs e)
        {
            
            try
            {
                UserDialogs.Instance.ShowLoading("Cargando informacion...");
                
                var result = await getMethods.ProveedorSearchAsync(txtBuscar.Text);

                if (result[0] != "OK")
                {
                    //await App.Current.MainPage.DisplayAlert("Error", $"Error al obtener los proveedores, intente mas tarde. \nCode: {result[0]} \nError: {result[1]}", "Cerrar");
                    await App.Current.MainPage.DisplayAlert("", $"Sin coincidencias", "Cerrar");
                    UserDialogs.Instance.HideLoading();
                    return;
                }

                lstProveedores.ItemsSource = JsonConvert.DeserializeObject<List<Proveedores>>(result[1]);

                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Error al obtener los proveedores, intente mas tarde. \n Error: {ex.Message.ToString()}", "Cerrar");
                UserDialogs.Instance.HideLoading();
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Proveedores prov = new Proveedores();
            Dismiss(prov);
        }
    }
}