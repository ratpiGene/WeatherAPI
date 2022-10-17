using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace WeatherAPI
{
    public class MainWindow : Window
    {
        [UI] private Search _search = null;
        [UI] private Icon _icon = null;
        [UI] private Humidity _humidity = null;
        [UI] private Lat _lat = null;
        [UI] private Lon _lon = null;
        [UI] private Temperature _temperature = null;
        [UI] private Desc _desc = null;
        [UI] private Label _condition = null;



        public MainWindow() : this(new Builder("MainWindow.glade")) { }

        private MainWindow(Builder builder) : base(builder.GetRawOwnedObject("MainWindow"))
        {
            builder.Autoconnect(this);

            DeleteEvent += Window_DeleteEvent;
            _button1.Clicked += Button1_Clicked;
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

        private void Button1_Clicked(object sender, EventArgs a)
        {
            _label1.Text = "Hello World! This button has been clicked " + _counter + " time(s).";
        }
    }
}
