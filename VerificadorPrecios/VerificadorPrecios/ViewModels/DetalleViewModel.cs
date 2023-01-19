using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VerificadorPrecios.ViewModels
{
    public class DetalleViewModel : BaseViewModel
    {
        public DetalleViewModel()
        {
            Title = "Inicio";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }

        public ICommand OpenWebCommand { get; }
    }
}
