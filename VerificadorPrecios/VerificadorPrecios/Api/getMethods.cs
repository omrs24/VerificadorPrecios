using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VerificadorPrecios.Models;
using VerificadorPrecios.Views;
using Xamarin.Forms;

namespace VerificadorPrecios.Api
{
    public class getMethods : ContentPage
    {

        public static string EndPoint = "https://hosofy.com/tienda/api/";

        #region 'Productos'
        //
        //      Funcion para llamar la api de hosofy, obtener todos los productos de la tienda
        //      y llenar la listview con todos los registros de forma asyncrona
        //
        public static async Task<string[]> ProductosGetAsync()
        {
            try
            {
                Uri uri = new Uri(string.Format(EndPoint + "products", string.Empty));
                var client = new HttpClient();
                string[] content = new string[2];

                HttpResponseMessage response = await client.GetAsync(uri);


                if (response.IsSuccessStatusCode)
                {
                    content[0] = response.StatusCode.ToString();
                    content[1] = await response.Content.ReadAsStringAsync();

                    return content;
                    //return JsonConvert.DeserializeObject<List<Productos>>(content);

                }

                return content;
            }
            catch (Exception ex)
            {
                string[] response = new string[2];

                response[0] = "404";
                response[1] = ex.Message.ToString();

                return response;
            }
        }

        public List<Productos> ProductosGet()
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    //Allow all certificates
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    webClient.BaseAddress = EndPoint;
                    var json = webClient.DownloadString("products");
                    var list = JsonConvert.DeserializeObject<List<Productos>>(json);
                    return list.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<string[]> ProductoGetAsync(string reference)
        {
            try
            {
                Uri uri = new Uri(string.Format(EndPoint + "product/" + reference, string.Empty));
                var client = new HttpClient();
                string[] content = new string[2];

                HttpResponseMessage response = await client.GetAsync(uri);


                if (response.IsSuccessStatusCode)
                {
                    content[0] = response.StatusCode.ToString();
                    content[1] = await response.Content.ReadAsStringAsync();

                    return content;
                    //return JsonConvert.DeserializeObject<List<Productos>>(content);

                }

                return content;
            }
            catch (Exception ex)
            {
                string[] response = new string[2];

                response[0] = "404";
                response[1] = ex.Message.ToString();

                return response;
            }
        }

        public List<Productos> ProductoGet(string reference)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    //Allow all certificates
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    webClient.BaseAddress = EndPoint;
                    var json = webClient.DownloadString("products/" + reference);
                    var list = JsonConvert.DeserializeObject<List<Productos>>(json);
                    return list.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Productos> ProductoSearch(string reference)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    //Allow all certificates
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    webClient.BaseAddress = EndPoint;
                    var json = webClient.DownloadString("products/search/" + reference);
                    var list = JsonConvert.DeserializeObject<List<Productos>>(json);
                    return list.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<string[]> ProductosSearchAsync(string reference)
        {
            try
            {
                Uri uri = new Uri(string.Format(EndPoint + "products/search/" + reference, string.Empty));
                var client = new HttpClient();
                string[] content = new string[2];

                HttpResponseMessage response = await client.GetAsync(uri);


                if (response.IsSuccessStatusCode)
                {
                    content[0] = response.StatusCode.ToString();
                    content[1] = await response.Content.ReadAsStringAsync();

                    return content;
                    //return JsonConvert.DeserializeObject<List<Productos>>(content);

                }

                return content;
            }
            catch (Exception ex)
            {
                string[] response = new string[2];

                response[0] = "404";
                response[1] = ex.Message.ToString();

                return response;
            }
        }

        #endregion

        #region 'Proveedores'
        //
        //      Funcion para llamar la api de hosofy, obtener todos los proveedores de la tienda de forma asyncrona
        //
        public static async Task<string[]> ProveedoresGetAsync()
        {
            try
            {
                Uri uri = new Uri(string.Format(EndPoint + "vendors", string.Empty));
                var client = new HttpClient();
                string[] content = new string[2];

                HttpResponseMessage response = await client.GetAsync(uri);


                if (response.IsSuccessStatusCode)
                {
                    content[0] = response.StatusCode.ToString();
                    content[1] = await response.Content.ReadAsStringAsync();

                    return content;
                    //return JsonConvert.DeserializeObject<List<Productos>>(content);

                }

                return content;
            }
            catch (Exception ex)
            {
                string[] response = new string[2];

                response[0] = "404";
                response[1] = ex.Message.ToString();

                return response;
            }
        }

        public static List<Proveedores> ProveedoresGet()
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    //Allow all certificates
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    webClient.BaseAddress = EndPoint;
                    var json = webClient.DownloadString("vendors");
                    var list = JsonConvert.DeserializeObject<List<Proveedores>>(json);
                    return list.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static List<Productos> ProveedorSearch(string reference)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    //Allow all certificates
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    webClient.BaseAddress = EndPoint;
                    var json = webClient.DownloadString("vendors/search/" + reference);
                    var list = JsonConvert.DeserializeObject<List<Productos>>(json);
                    return list.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<string[]> ProveedorSearchAsync(string reference)
        {
            try
            {
                Uri uri = new Uri(string.Format(EndPoint + "vendors/search/" + reference, string.Empty));
                var client = new HttpClient();
                string[] content = new string[2];

                HttpResponseMessage response = await client.GetAsync(uri);


                if (response.IsSuccessStatusCode)
                {
                    content[0] = response.StatusCode.ToString();
                    content[1] = await response.Content.ReadAsStringAsync();

                    return content;
                    //return JsonConvert.DeserializeObject<List<Productos>>(content);

                }

                return content;
            }
            catch (Exception ex)
            {
                string[] response = new string[2];

                response[0] = "404";
                response[1] = ex.Message.ToString();

                return response;
            }
        }

        #endregion

        #region 'Categorias'
        //
        //      Funcion para llamar la api de hosofy, obtener todas las categorias de la tienda de forma asyncrona
        //
        public static async Task<string[]> CategoriasGetAsync()
        {
            try
            {
                Uri uri = new Uri(string.Format(EndPoint + "categories", string.Empty));
                var client = new HttpClient();
                string[] content = new string[2];

                HttpResponseMessage response = await client.GetAsync(uri);


                if (response.IsSuccessStatusCode)
                {
                    content[0] = response.StatusCode.ToString();
                    content[1] = await response.Content.ReadAsStringAsync();

                    return content;
                    //return JsonConvert.DeserializeObject<List<Productos>>(content);

                }

                return content;
            }
            catch (Exception ex)
            {
                string[] response = new string[2];

                response[0] = "404";
                response[1] = ex.Message.ToString();

                return response;
            }
        }

        public static List<Categorias> CategoriasGet()
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    //Allow all certificates
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    webClient.BaseAddress = EndPoint;
                    var json = webClient.DownloadString("categories");
                    var list = JsonConvert.DeserializeObject<List<Categorias>>(json);
                    return list.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Productos> CategoriaSearch(string reference)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    //Allow all certificates
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    webClient.BaseAddress = EndPoint;
                    var json = webClient.DownloadString("categories/search/" + reference);
                    var list = JsonConvert.DeserializeObject<List<Productos>>(json);
                    return list.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<string[]> CategoriaSearchAsync(string reference)
        {
            try
            {
                Uri uri = new Uri(string.Format(EndPoint + "categories/search/" + reference, string.Empty));
                var client = new HttpClient();
                string[] content = new string[2];

                HttpResponseMessage response = await client.GetAsync(uri);


                if (response.IsSuccessStatusCode)
                {
                    content[0] = response.StatusCode.ToString();
                    content[1] = await response.Content.ReadAsStringAsync();

                    return content;
                    //return JsonConvert.DeserializeObject<List<Productos>>(content);

                }

                return content;
            }
            catch (Exception ex)
            {
                string[] response = new string[2];

                response[0] = "404";
                response[1] = ex.Message.ToString();

                return response;
            }
        }

        #endregion

        #region 'Marcas'
        //
        //      Funcion para llamar la api de hosofy, obtener todas las marcas de la tienda de forma asyncrona
        //
        public static async Task<string[]> MarcasGetAsync()
        {
            try
            {
                Uri uri = new Uri(string.Format(EndPoint + "brands", string.Empty));
                var client = new HttpClient();
                string[] content = new string[2];

                HttpResponseMessage response = await client.GetAsync(uri);


                if (response.IsSuccessStatusCode)
                {
                    content[0] = response.StatusCode.ToString();
                    content[1] = await response.Content.ReadAsStringAsync();

                    return content;
                    //return JsonConvert.DeserializeObject<List<Productos>>(content);

                }

                return content;
            }
            catch (Exception ex)
            {
                string[] response = new string[2];

                response[0] = "404";
                response[1] = ex.Message.ToString();

                return response;
            }
        }


        public static List<Marcas> MarcasGet()
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    //Allow all certificates
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    webClient.BaseAddress = EndPoint;
                    var json = webClient.DownloadString("brands");
                    var list = JsonConvert.DeserializeObject<List<Marcas>>(json);
                    return list.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Productos> MarcaSearch(string reference)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    //Allow all certificates
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    webClient.BaseAddress = EndPoint;
                    var json = webClient.DownloadString("brands/search/" + reference);
                    var list = JsonConvert.DeserializeObject<List<Productos>>(json);
                    return list.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<string[]> MarcaSearchAsync(string reference)
        {
            try
            {
                Uri uri = new Uri(string.Format(EndPoint + "brands/search/" + reference, string.Empty));
                var client = new HttpClient();
                string[] content = new string[2];

                HttpResponseMessage response = await client.GetAsync(uri);


                if (response.IsSuccessStatusCode)
                {
                    content[0] = response.StatusCode.ToString();
                    content[1] = await response.Content.ReadAsStringAsync();

                    return content;
                    //return JsonConvert.DeserializeObject<List<Productos>>(content);

                }

                return content;
            }
            catch (Exception ex)
            {
                string[] response = new string[2];

                response[0] = "404";
                response[1] = ex.Message.ToString();

                return response;
            }
        }

        #endregion
    }
}
