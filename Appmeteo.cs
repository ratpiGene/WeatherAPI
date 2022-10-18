using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace App_meteo_csharp
{
    class Appmeteo: Window
    {

        public Appmeteo() : this(new Builder("Appmeteo.glade")) { }

        [UI] private Button option = null;

        private Appmeteo(Builder builder) : base(builder.GetRawOwnedObject("windowmeteo"))
        {

            builder.Autoconnect(this);

            option.Clicked += Newindow;


            DeleteEvent += Window_DeleteEvent;
        }

        private void Newindow(object sender, EventArgs args)
        {
            optionwindow win = new optionwindow();
            
            win.Show();
            this.Hide();

        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

    }

}
