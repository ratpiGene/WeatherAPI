using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Gtk;
using System.IO;
using UI = Gtk.Builder.ObjectAttribute;

namespace Application_meteo_csharp
{
    class OptionWindow: Window
    {

        public OptionWindow() : this(new Builder("optionwindow.glade")) { }

        [UI] private Button backbtn = null;
        [UI] private Button savecity = null;
        [UI] private Button savelang = null;
        [UI] private Entry defaultcity = null;
        [UI] private Entry defaultlang = null;
        private string[] ISOLang = 
        {
            "af","al","ar","az", "bg", "ca",
             "cz", "da", "de", "el", "en", "eu", 
             "fa", "fi", "fr", "gl","he", "hi", 
             "hr", "hu", "id", "it", "ja", "kr", 
             "la", "lt", "mk", "no", "nl", "pl",
             "pt", "pt_ro", "ru", "sv","sk", "sl", 
             "sp", "sr", "th", "tr", "ua","vi", "zh_cn", 
             "zh_tw","zu"
        };

        private OptionWindow(Builder builder) : base(builder.GetRawOwnedObject("optionwindow"))
        {

            builder.Autoconnect(this);
            backbtn.Clicked += BackToMainWindow;
            savecity.Clicked += CityDefault;
            savelang.Clicked += LangDefault;
            DeleteEvent += Window_DeleteEvent;
        }
        private void BackToMainWindow(object sender, EventArgs args)
        {
            Appmeteo win = new Appmeteo();
            win.Show();
            this.Hide();

        }

        private void CityDefault(object sender, EventArgs a)
        {
            string newcity = defaultcity.Text;
            string lang;
            string message;
            if(newcity != "")
            {
                var api = new API(newcity);
                JObject obj = api.DailyQuery();
                message = obj.Value<string>("cod");
                if(message == "200")
                {
                    string path = @".\options.json";
                    bool fileExist = File.Exists(path);
                    if(fileExist)
                    {   
                        using(StreamReader reader = new StreamReader(path))
                        {
                            string jsonString = reader.ReadToEnd();
                            JObject Options = JsonConvert.DeserializeObject<JObject>(jsonString);
                            lang = Convert.ToString(Options["Lang"]);
                        
                        }
                        using (StreamWriter file = File.CreateText(path))
                        using (JsonWriter writer = new JsonTextWriter(file))
                        {
                            JObject newOptions = new JObject
                            (
                                new JProperty("City",newcity),
                                new JProperty("Lang",lang)
                            );
                            newOptions.WriteTo(writer);
                        }
                    }
                    else{
                        StreamWriter optionsFile = new StreamWriter("options.json");
                    
                    }
                }
            }
        }

        private void LangDefault(object sender, EventArgs a)
        {
            string newlang = defaultlang.Text;
            string city;
            var check = Array.Exists(ISOLang, x => x == newlang);
            if(check)
            {
                string path = @".\options.json";
                bool fileExist = File.Exists(path);
            
                if(fileExist)
                {   
                    using(StreamReader reader = new StreamReader(path))
                    {
                        string jsonString = reader.ReadToEnd();
                        JObject Options = JsonConvert.DeserializeObject<JObject>(jsonString);
                        city = Convert.ToString(Options["City"]);
                    
                    }
                    using (StreamWriter file = File.CreateText(path))
                    using (JsonWriter writer = new JsonTextWriter(file))
                    {
                        JObject newOptions = new JObject
                        (
                            new JProperty("City",city),
                            new JProperty("Lang",newlang)
                        );
                        newOptions.WriteTo(writer);
                    }
                }
                else{
                    StreamWriter optionsFile = new StreamWriter("options.json");
                
                }
            }
            
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

    }

}
