using VerificadorPrecios.Models;
using VerificadorPrecios.ViewModels;
using VerificadorPrecios.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace VerificadorPrecios
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(Registrar), typeof(Registrar));
            Routing.RegisterRoute(nameof(RegistrarProveedor), typeof(RegistrarProveedor));
            Routing.RegisterRoute(nameof(RegistrarProveedor), typeof(RegistrarProveedor));
            Routing.RegisterRoute(nameof(RegistrarCategoria), typeof(RegistrarCategoria));
            Routing.RegisterRoute(nameof(RegistrarMarca), typeof(RegistrarMarca));
        }
    }
}