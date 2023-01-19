using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VerificadorPrecios.Models;

namespace VerificadorPrecios.Api
{
    public class patchMethods
    {
        public static string EndPoint = "https://hosofy.com/tienda/api/";

        public static async Task<string[]> UpdateProducto(Productos item, string reference)
        {
            try
            {
                string[] status = new string[2];
                Uri uri = new Uri(string.Format(EndPoint + "products/" + reference, string.Empty));

                var client = new HttpClient();

                string json = JsonConvert.SerializeObject(item);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                response = await client.PutAsync(uri, content);

                status[0] = ((int)response.StatusCode).ToString();
                status[1] = await response.Content.ReadAsStringAsync();

                return status;
                //return JsonConvert.DeserializeObject<List<Productos>>(content);
            }
            catch (Exception ex)
            {
                string[] response = new string[2];

                response[0] = "404";
                response[1] = ex.Message.ToString();

                return response;
            }

        }
    }
}
