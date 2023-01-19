using Acr.UserDialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VerificadorPrecios.Api;
using VerificadorPrecios.Behaviors;
using VerificadorPrecios.Models;
using VerificadorPrecios.Views.Popups;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Mobile;

namespace VerificadorPrecios.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registrar : ContentPage
    {
        public Registrar()
        {
            InitializeComponent();

            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            imgProducto.Source = ImageSource.FromFile(strimgpath.Text);

        }

        private async void imgProducto_Clicked(object sender, EventArgs e)
        {
            try
            {
                //var imgsource = imgProducto.Source;

                var result = await Navigation.ShowPopupAsync(new Imagen_Preview(strimgpath.Text)
                {
                    IsLightDismissEnabled = false
                });
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Error al previsualizar imagen.", "OK");
            }
        }

        private async void txtProveedorTGR_Tapped(object sender, EventArgs e)
        {
            try
            {
                Proveedores result = (Proveedores)await Navigation.ShowPopupAsync(new Proveedor_picker()
                {
                    IsLightDismissEnabled = false
                });

                if (result == null)
                    return;

                lblIdProveedor.Text = result.id.ToString();
                txtProveedor.Text = result.name;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $" Error al seleccionar proveedor: \n{ex.Message.ToString()}", "Cerrar");

            }
        }

        private async void txtMarcaTGR_Tapped(object sender, EventArgs e)
        {
            try
            {
                Marcas result = (Marcas)await Navigation.ShowPopupAsync(new Marca_picker()
                {
                    IsLightDismissEnabled = false
                });

                lblIdMarca.Text = result.id.ToString();
                txtMarca.Text = result.name;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $" Error al seleccionar la marca: \n{ex.Message.ToString()}", "Cerrar");
            }
        }

        private async void txtCategoriaTGR_Tapped(object sender, EventArgs e)
        {
            try
            {
                Categorias result = (Categorias)await Navigation.ShowPopupAsync(new Categoria_Picker()
                {
                    IsLightDismissEnabled = false
                });

                lblIdCategoria.Text = result.id.ToString();
                txtCategoria.Text = result.catName;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $" Error al seleccionar la marca: \n{ex.Message.ToString()}", "Cerrar");
            }
        }

        async private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Cargando informacion...");

                if (!await validar())
                {
                    UserDialogs.Instance.HideLoading();
                    return;
                }

                //Primero subimos la imagen al servidor, conla ruta del dispositivo
                FileResult fr = new FileResult(strimgpath.Text);

                var content = new MultipartFormDataContent();

                //Varaible que contiene la imagen 
                var stream = await fr.OpenReadAsync();

                content.Add(new StreamContent(stream), "file", fr.FileName);
                //await DisplayAlert("", file.FileName.ToString() , "Ok");
                var httpclient = new HttpClient();

                string url = "https://hosofy.com/tienda/api/products/upload";

                httpclient.BaseAddress = new Uri(url);

                var responseimg = await httpclient.PostAsync("", content);
                //Obtener la respuesta del servidor 
                var result = await responseimg.Content.ReadAsStringAsync();

                if (responseimg.IsSuccessStatusCode)
                {

                    Productos prod = new Productos()
                    {
                        name = txtTitulo.Text,
                        code = txtcodigobarras.Text,
                        reference = txtcodigobarras.Text,
                        detail = txtdetalle.Text,
                        vendorID = Convert.ToInt32(lblIdProveedor.Text),
                        marca = Convert.ToInt32(lblIdMarca.Text),
                        catID = Convert.ToInt32(lblIdCategoria.Text),
                        cost = Convert.ToDecimal(txtCosto.Text),
                        price1 = Convert.ToDecimal(txtprecio.Text),
                        lists = '0',
                        inventory = Convert.ToInt32(txtinventario.Text),
                        remarks = txtComentarios.Text,
                        active = "Y",
                        image = "https://hosofy.com/tienda/imagenes/" + fr.FileName,
                        listas = 0
                    };

                    var responseprod = await postMethods.CreateProducto(prod);

                    if (responseprod[0] != "201")
                    {
                        await DisplayAlert("Error", $"Error al crear producto, intente mas tarde. \nCode: {responseprod[0]} \nError: {responseprod[1]}", "Cerrar");
                        UserDialogs.Instance.HideLoading();
                        return;
                    }


                    await DisplayAlert("", $"Producto creado correctamente codigo: {responseprod[1]}", "Cerrar");
                    limpiarform();
                    UserDialogs.Instance.HideLoading();
                    return;
                }

                await DisplayAlert("Error", $"Error al subir la imagen, intente de nuevo: \n { result }", "Ok");
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                await DisplayAlert("", $"Error al crear el producto: \n Mensaje: { ex.Message.ToString() } \n Linea:  { ex.StackTrace } ", "Ok");
                UserDialogs.Instance.HideLoading();
            }

            
        }


        #region 'BarCode Scanner'

        async void btnScan_Clicked(object sender, EventArgs e)
        {
            try
            {
                var scanner = new ZXing.Mobile.MobileBarcodeScanner();
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
                //lenamos el cuadro de busqueda con el codigo escaneado
                txtcodigobarras.Text = result.Text;
            }
            else
            {
                DisplayAlert("", msg, "Ok");
            }
        }

        #endregion


        private async void btnSelecImagen_Clicked(object sender, EventArgs e)
        {
            try
            {
                var file = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Selecciona una imagen del producto"
                });

                if (file == null)
                {
                    await DisplayAlert("", "Imagen no seleccionada", "Ok");
                    return;
                }

                //Varaible que contiene la imagen 
                var stream = await file.OpenReadAsync();
                
                imgProducto.Source = ImageSource.FromStream(() => stream);

                strimgpath.Text = file.FullPath;
            }
            catch (Exception ex)
            {
                await DisplayAlert("", $"Error al seleccionar la imagen: \n { ex.Message.ToString() }", "");
            }
        }

        private async void btnTomarImagen_Clicked(object sender, EventArgs e)
        {
            try
            {
                var file = await MediaPicker.CapturePhotoAsync();

                if (file == null)
                {
                    await DisplayAlert("", "Imagen no tomada", "Ok");
                    return;
                }

                //Varaible que contiene la imagen 
                var stream = await file.OpenReadAsync();
                
                imgProducto.Source = ImageSource.FromStream(() => stream);

                strimgpath.Text = file.FullPath;
            }
            catch (Exception ex)
            {
                await DisplayAlert("", $"Error al tomar la imagen: \n { ex.Message.ToString() }", "");
            }
        }

        private async Task<bool> validar()
        {
            //bool status = true;


            if (String.IsNullOrEmpty(txtTitulo.Text))
            {
                await DisplayAlert("Alerta", "Nombre del producto requerido. ", "OK");
                txtTitulo.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(txtcodigobarras.Text))
            {
                await DisplayAlert("Alerta", "Codigo de barras requerido. ", "OK");
                txtcodigobarras.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(txtdetalle.Text))
            {
                await DisplayAlert("Alerta", "Descripcion del producto requerido. ", "OK");
                txtdetalle.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(txtProveedor.Text))
            {
                await DisplayAlert("Alerta", "Proveedor requerido. ", "OK");
                return false;
            }

            if (String.IsNullOrEmpty(txtMarca.Text))
            {
                await DisplayAlert("Alerta", "Marca requerida. ", "OK");
                return false;
            }

            if (String.IsNullOrEmpty(txtCategoria.Text))
            {
                await DisplayAlert("Alerta", "Categoria requerida. ", "OK");
                return false;
            }

            if (String.IsNullOrEmpty(txtCosto.Text))
            {
                await DisplayAlert("Alerta", "Costo requerido. ", "OK");
                txtCosto.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(txtprecio.Text))
            {
                await DisplayAlert("Alerta", "Precio requerido. ", "OK");
                txtprecio.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(txtinventario.Text))
            {
                await DisplayAlert("Alerta", "Cantidad requerida. ", "OK");
                txtinventario.Focus();
                return false;
            }



            return true;
        }


        public void limpiarform()
        {
            imgProducto.Source = null;
            strimgpath.Text = "";

            txtTitulo.Text = "";
            txtcodigobarras.Text = "";
            txtdetalle.Text = "";

            txtProveedor.Text = "";
            lblIdProveedor.Text = "";

            txtMarca.Text = "";
            lblIdMarca.Text = "";

            txtCategoria.Text = "";
            lblIdCategoria.Text = "";

            txtCosto.Text = "";
            txtprecio.Text = "";
            txtinventario.Text = "";

            txtComentarios.Text = "";

        }

        //Funcion para guardar imagen en servidor Hosofy
        /*private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var file = await MediaPicker.PickPhotoAsync();

                if (file == null)
                    return;

                var content = new MultipartFormDataContent();

                //Varaible que contiene la imagen 
                var stream = await file.OpenReadAsync();

                content.Add(new StreamContent(stream), "file", file.FileName);
                //await DisplayAlert("", file.FileName.ToString() , "Ok");
                var httpclient = new HttpClient();

                string url = "https://hosofy.com/tienda/api/products/upload";

                httpclient.BaseAddress = new Uri(url);

                var response = await httpclient.PostAsync("", content);
                //Obtener la respuesta del servidor 
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {

                    await DisplayAlert("Listo", $"Imagen subida correctamente \n { result }", "OK");

                    //imgProducto.Source = $"https://hosofy.com/tienda/imagenes/{ file.FileName }" ;
                    imgProducto.Source = ImageSource.FromStream( () => stream);
                    

                    return;
                }

                await DisplayAlert("Error", $"Error al subir la imagen, intente de nuevo: \n { result }", "Ok");


            }
            catch (Exception ex)
            {
                await DisplayAlert("", $"Error al subir la imagen: \n { ex.Message.ToString() }","");
            }
        }*/
    }
}