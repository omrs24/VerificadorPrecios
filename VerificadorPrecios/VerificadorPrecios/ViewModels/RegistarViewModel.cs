using VerificadorPrecios.Views;
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;

namespace VerificadorPrecios.ViewModels
{
    public class RegistrarViewModel : BaseViewModel
    {

        #region "Propiedades"
        private bool costoValid;

        public bool CostoValid
        {
            get => costoValid;
            set
            {
                costoValid = value;
                OnPropertyChanged();
            }
        }
        #endregion
        public RegistrarViewModel()
        {
            Title = "Registrar Producto";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
            OpenRegProveedor = new Command(async () => await Shell.Current.GoToAsync(nameof(RegistrarProveedor)));
            OpenRegCategoria = new Command(async () => await Shell.Current.GoToAsync(nameof(RegistrarCategoria)));
            OpenRegMarca = new Command(async () => await Shell.Current.GoToAsync(nameof(RegistrarMarca)));
        }

        public ICommand OpenWebCommand { get; }
        public ICommand OpenRegProveedor { get; }
        public ICommand OpenRegCategoria { get; }
        public ICommand OpenRegMarca { get; }

    }

}


