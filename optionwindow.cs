using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace App_meteo_csharp
{
    class optionwindow: Window
    {

        public optionwindow() : this(new Builder("optionwindow.glade")) { }

        [UI] private Button mainwindow = null;

        private optionwindow(Builder builder) : base(builder.GetRawOwnedObject("optionwindow"))
        {

            builder.Autoconnect(this);

            mainwindow.Clicked += Newindow;

            DeleteEvent += Window_DeleteEvent;
        }
        private void Newindow(object sender, EventArgs args)
        {
            Appmeteo win = new Appmeteo();
            
            win.Show();
            this.Hide();

        }
        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

    }

}
