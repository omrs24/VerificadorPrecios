using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VerificadorPrecios.ViewModels
{
    public class RegistrarMarcaViewModel : BaseViewModel
    {
        public RegistrarMarcaViewModel()
        {
            Title = "Registrar Marca";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));

        }

        public ICommand OpenWebCommand { get; }
    }
}
