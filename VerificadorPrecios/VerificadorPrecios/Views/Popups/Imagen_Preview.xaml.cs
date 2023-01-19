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
    public partial class Imagen_Preview : Popup
    {
        public Imagen_Preview(string imagen)
        {
            InitializeComponent();

            //App.Current.MainPage.DisplayAlert("",imagen.ToString(),"OK");
            try
            {
                
                ImagenPreview.Source = ImageSource.FromFile(imagen);
            }
            catch (Exception)
            {
                
            }

            
        }

        private void btnCerrar_Clicked(object sender, EventArgs e)
        {
            Dismiss("");
        }
    }
}