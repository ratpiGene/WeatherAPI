using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace WeatherAPI
{
     class WeatherInfo
    {
        public class coord
        {
            public double lon { get; set; }
            public double lat { get; set; }
        }
        public class weather{
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }
        public class main
        {
            public double temp { get; set; }
            public double humidity { get; set; }
        }
        public class root
        {
            public coord coord { get; set; }
            public List <weather> weather { get; set; }
            public main main { get; set; }
        }
        
    }
    public class MainWindow : Window
    {
        [UI] private Search _search = null;
        [UI] private Name _name = null;
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
            Search.Clicked += Search_Clicked;
            
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

        private void Search_Clicked(object sender, EventArgs a)
        {
            ;
        }
    }
}
