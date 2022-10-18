using System;
using Gtk;
using System.Net;
using System.Net.Http;

namespace Application_meteo_csharp
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.Init();

            var app = new Application("org.app_meteo_csharp.app_meteo_csharp", GLib.ApplicationFlags.None);
            app.Register(GLib.Cancellable.Current);

            var win = new Appmeteo();
            app.AddWindow(win);

            win.Show();
            Application.Run();
        }
    }
}
