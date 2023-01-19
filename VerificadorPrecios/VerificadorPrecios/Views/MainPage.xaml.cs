using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using VerificadorPrecios.Api;
using Xamarin.CommunityToolkit.Extensions;
using VerificadorPrecios.Models;
using ZXing.Mobile;
using System.Net.Http;
using Newtonsoft.Json;
using Acr.UserDialogs;

namespace VerificadorPrecios.Views
{
    public partial class MainPage : ContentPage
    {
        public static string EndPoint = "https://hosofy.com/tienda/api/";
        public MainPage()
        {
            InitializeComponent();

        }

        
        //api call todos
        async public Task ListaProdsAll()
        {
            try
            {
                lstProds.SelectedItem = Color.LightGray;
                
                var result = await getMethods.ProductosGetAsync();

                if (result[0] != "OK")
                {
                    await DisplayAlert("Error", $"Error al obtener los productos, intente mas tarde. \nCode: {result[0]} \nError: {result[1]}", "Cerrar");
                    return;
                }

                lstProds.ItemsSource = JsonConvert.DeserializeObject<List<Productos>>(result[1]);
            }
            catch(Exception ex)
            {
                await DisplayAlert("Error", $"Error al obtener los productos, intente mas tarde. \n Error: {ex.Message.ToString()}" , "Cerrar");
            }
        }
        

        async private void btnAllProds_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Cargando informacion...");
            await ListaProdsAll();
            UserDialogs.Instance.HideLoading();
        }

        async private void btnBuscarFolio_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Cargando informacion...");
            await ListaProd(txtRefence.Text);
            UserDialogs.Instance.HideLoading();
        }
        //api call por id
        async public Task ListaProd(string reference)
        {
            try
            {
                lstProds.SelectedItem = Color.LightGray;


                var result = await getMethods.ProductosSearchAsync(reference);

                if (result[0] != "OK")
                {
                    await DisplayAlert("Error", $"Error al buscar los productos, intente mas tarde. \nCode: {result[0]} \nError: {result[1]}", "Cerrar");
                    return;
                }

                lstProds.ItemsSource = JsonConvert.DeserializeObject<List<Productos>>(result[1]);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error al obtener los productos, intente mas tarde. \n Error: {ex.Message.ToString()}", "Cerrar");
            }

        }

        async private void lstProds_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var menu = e.SelectedItem as Productos;
            
            if (menu != null)
            {
                
                var result = await Navigation.ShowPopupAsync(new DetalleProducto(menu)
                {
                    IsLightDismissEnabled = false
                });
                lstProds.SelectedItem = false;
                
            }
            

        }

        #region 'BarCode Scanner'

        async void btnScan_Clicked(object sender, EventArgs e)
        {
            try
            {
                var scanner = new MobileBarcodeScanner();
                scanner.TopText = "Aleja la camara a 15 cm de distancia\ndel código de barras.";
                scanner.BottomText = "El codigo se escaneará solo.";
                scanner.FlashButtonText = "Linterna";
                scanner.AutoFocus();

                //This will start scanning
                ZXing.Result result = await scanner.Scan();

                //Show the result returned.
                HandleResult(result);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Advertencia", "¡Producto no encontrado!", "Cerrar");
            }
        }

        void HandleResult(ZXing.Result result)
        {
            var msg = "No se completo el escaneo";
            if (result != null)
            {
                //msg = "Barcode: " + result.Text + " (" + result.BarcodeFormat + ")";

                //lenamos el cuadro de busqueda con el codigo escaneado
                txtRefence.Text = result.Text;

                //Despues de obtener el resutlado del escaneo, creamos una instanacia a la clase de la api para solicitar la info del producto a traves del metodo productoget
                getMethods getOb = new getMethods();

                //La api regresa un arreglo json por lo que solo usamos el primer elemento para pasarlo al popup de detalle
                Productos prod = getOb.ProductoGet(result.Text)[0];

                Navigation.ShowPopup(new DetalleProducto(prod));
            }
            else
            {
                DisplayAlert("", msg, "Ok");
            }
        }
        #endregion
    }
}
