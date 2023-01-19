using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using VerificadorPrecios.Behaviors;
using VerificadorPrecios.Renderers;
using VerificadorPrecios.Droid.Renderers;

[assembly: ExportRenderer(typeof(Entry), typeof(EntryLineColorRenderer))]
namespace VerificadorPrecios.Droid.Renderers
{
    class EntryLineColorRenderer : EntryRenderer
    {
        public EntryLineColorRenderer(Context context) : base(context)
        {

        }
        
    }
}