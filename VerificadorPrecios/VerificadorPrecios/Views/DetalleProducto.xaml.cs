    using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.CommunityToolkit.UI.Views;
using VerificadorPrecios.Api;
using VerificadorPrecios.Models;
using System.Threading.Tasks;
using ZXing.Mobile;
using Acr.UserDialogs;

namespace VerificadorPrecios.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleProducto : Popup
    {
        public DetalleProducto(Productos selected_prod)
        {
            InitializeComponent();

            //getMethods getOb = new getMethods();

            //Como la funcion regresa una lista de objetos solo ocupamos el primer elemento
            //Productos prod = getOb.ProductoGet(selected_prod.reference)[0];

            //la llamada no es awaited para que se llenen los pck antes que la info
            

            LlenarInfo(selected_prod);

        }

        async private void LlenarInfo(Productos prodinfo)
        {
            UserDialogs.Instance.ShowLoading("Cargando informacion...");


            await LlenarProveedoresasync();
            await LlenarCategoriasasync();
            await LlenarMarcasasync();

            lblid.Text = prodinfo.reference;

            txttitulo.Text = prodinfo.name;

            txtcodigobarras.Text = prodinfo.reference;

            txtdetalle.Text = prodinfo.detail;

            txtinventario.Text = prodinfo.inventory.ToString();

            //pickers

            pckProveedor.SelectedIndex = prodinfo.vendorID - 1;

            pckCategoria.SelectedIndex = prodinfo.catID - 1;

            pckMarca.SelectedIndex = prodinfo.marca - 1;

            txtprecio.Text = prodinfo.price1.ToString();

            txtcosto.Text = prodinfo.cost.ToString();

            txtComentarios.Text = prodinfo.remarks.ToString();

            imgproducto.Source = prodinfo.image;

            //falta llenar proveedor, marca y categoria
            UserDialogs.Instance.HideLoading();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Dismiss("");
        }

        async private void btnUpdate_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Cargando informacion...");
            
            Productos prod = new Productos()
            {
                name = txttitulo.Text,
                code = txtcodigobarras.Text,
                reference = txtcodigobarras.Text,
                detail = txtdetalle.Text,
                vendorID = pckProveedor.SelectedIndex + 1,
                marca = pckMarca.SelectedIndex + 1,
                catID = pckCategoria.SelectedIndex + 1,
                cost = Convert.ToDecimal(txtcosto.Text),
                price1 = Convert.ToDecimal(txtprecio.Text),
                lists = '0',
                inventory = Convert.ToInt32(txtinventario.Text),
                remarks = txtComentarios.Text,
                active = "Y",
                image = "",
                listas = 0
            };

            var response = await patchMethods.UpdateProducto(prod, lblid.Text);
            var msg = JsonConvert.DeserializeObject(response[1]);
            if (response[0] != "200")
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Error al actualizar el producto, intente mas tarde. \nCode: {response[0]} \nError: {msg}", "Cerrar");
                return;
            }


            await App.Current.MainPage.DisplayAlert("", $"Producto actualizado correctamente codigo: {response[1]}", "Cerrar");

            UserDialogs.Instance.HideLoading();

        }

        /*private void pckProveedor_Focused(object sender, FocusEventArgs e)
        {
            LlenarProveedores();
        }

        private void pckMarca_Focused(object sender, FocusEventArgs e)
        {
            LlenarMarcas();
        }

        private void pckCategoria_Focused(object sender, FocusEventArgs e)
        {
            LlenarCategorias();
        }*/

        #region 'Llenarpickers'
        public void LlenarProveedores()
        {
            try
            {
                
                var result = getMethods.ProveedoresGet();

                /*if (result[0] != "OK")
                {
                    await App.Current.MainPage.DisplayAlert("Error", $"Error al obtener los proveedores, intente mas tarde. \nCode: {result[0]} \nError: {result[1]}", "Cerrar");
                    return;
                }*/

                pckProveedor.ItemsSource = result;
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Error", $"Error al obtener los proveedores, intente mas tarde. \n Error: {ex.Message.ToString()}", "Cerrar");
            }
        }

        public void LlenarCategorias()
        {
            try
            {

                var result = getMethods.CategoriasGet();

                /*if (result[0] != "OK")
                {
                    await App.Current.MainPage.DisplayAlert("Error", $"Error al obtener las categorias, intente mas tarde. \nCode: {result[0]} \nError: {result[1]}", "Cerrar");
                    return;
                }*/

                pckCategoria.ItemsSource = result;
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Error", $"Error al obtener las categorias, intente mas tarde. \n Error: {ex.Message.ToString()}", "Cerrar");
            }
        }

        public void LlenarMarcas()
        {
            try
            {

                var result = getMethods.MarcasGet();

                /*if (result[0] != "OK")
                {
                    await App.Current.MainPage.DisplayAlert("Error", $"Error al obtener las marcas, intente mas tarde. \nCode: {result[0]} \nError: {result[1]}", "Cerrar");
                    return;
                }*/

                pckMarca.ItemsSource = result;
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Error", $"Error al obtener las marcas, intente mas tarde. \n Error: {ex.Message.ToString()}", "Cerrar");
            }
        }

        #endregion

        #region 'Llenarpickersasync'
        async public Task LlenarProveedoresasync()
        {
            try
            {
                //pckProveedor.ItemsSource = getMethods.ProveedoresGet();
                var result = await getMethods.ProveedoresGetAsync();

                if (result[0] != "OK")
                {
                    await App.Current.MainPage.DisplayAlert("Error", $"Error al obtener los proveedores, intente mas tarde. \nCode: {result[0]} \nError: {result[1]}", "Cerrar");
                    return;
                }

                pckProveedor.ItemsSource = JsonConvert.DeserializeObject<List<Proveedores>>(result[1]);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Error al obtener los proveedores, intente mas tarde. \n Error: {ex.Message.ToString()}", "Cerrar");
            }
        }

        async public Task LlenarCategoriasasync()
        {
            try
            {

                var result = await getMethods.CategoriasGetAsync();

                if (result[0] != "OK")
                {
                    await App.Current.MainPage.DisplayAlert("Error", $"Error al obtener las categorias, intente mas tarde. \nCode: {result[0]} \nError: {result[1]}", "Cerrar");
                    return;
                }

                pckCategoria.ItemsSource = JsonConvert.DeserializeObject<List<Categorias>>(result[1]);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Error al obtener las categorias, intente mas tarde. \n Error: {ex.Message.ToString()}", "Cerrar");
            }
        }

        async public Task LlenarMarcasasync()
        {
            try
            {

                var result = await getMethods.MarcasGetAsync();

                if (result[0] != "OK")
                {
                    await App.Current.MainPage.DisplayAlert("Error", $"Error al obtener las marcas, intente mas tarde. \nCode: {result[0]} \nError: {result[1]}", "Cerrar");
                    return;
                }

                pckMarca.ItemsSource = JsonConvert.DeserializeObject<List<Marcas>>(result[1]);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Error al obtener las marcas, intente mas tarde. \n Error: {ex.Message.ToString()}", "Cerrar");
            }
        }

        #endregion

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
                await App.Current.MainPage.DisplayAlert("Advertencia", "¡Producto no encontrado!", "Cerrar");
            }
        }

        void HandleResult(ZXing.Result result)
        {
            var msg = "No se completo el escaneo";
            if (result != null)
            {
                //msg = "Barcode: " + result.Text + " (" + result.BarcodeFormat + ")";

                //lenamos el texto con el codigo escaneado
                txtcodigobarras.Text = result.Text;

            }
            else
            {
                App.Current.MainPage.DisplayAlert("", msg, "Ok");
            }
        }
        #endregion
    }
}