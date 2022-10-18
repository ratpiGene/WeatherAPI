using System;
using Gtk;
using System.Collections.Generic;
using UI = Gtk.Builder.ObjectAttribute;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;


namespace Application_meteo_csharp
{
    class Appmeteo: Window
    {   

        //Create the properties for the daily weather
        [UI] private Label city = null;
        [UI] private Label lat_lon = null;
        [UI] private Label description = null;
        [UI] private Label humidity = null;
        [UI] private Label temp = null;
        [UI] private Label wind = null;
        [UI] private Label pressure = null;
        [UI] private Label cloud = null; 
        [UI] private Label date = null;
        [UI] private Label time = null;
        

        //Creprivateoperties for the 5 Days Forecast Weather\

        [UI] private Label dayone = null;
        [UI] private Label daytwo = null;
        [UI] private Label daythree = null;
        [UI] private Label dayfour = null;
        [UI] private Label dayfive = null;

        [UI] private Label dayonetemp = null;
        [UI] private Label daytwotemp = null;
        [UI] private Label daythreetemp = null;
        [UI] private Label dayfourtemp = null;
        [UI] private Label dayfivetemp = null;

        [UI] private Label dayonedescription = null;
        [UI] private Label daytwodescription = null;
        [UI] private Label daythreedescription = null;
        [UI] private Label dayfourdescription = null;
        [UI] private Label dayfivedescription = null;
        [UI] private SearchEntry searchbar = null;


        [UI] private Image mainicon= null;
        [UI] private Image dayoneicon = null;
        [UI] private Image daytwoicon = null;
        [UI] private Image daythreeicon = null;

        [UI] private Button enter = null;
        [UI] private Button option = null;


        public Appmeteo() : this(new Builder("Appmeteo.glade")) { }

        private Appmeteo(Builder builder) : base(builder.GetRawOwnedObject("windowmeteo"))
        {
            builder.Autoconnect(this);
            UpDateUI();
            enter.Clicked += SearchBar_Entered;
            
            option.Clicked += OptionsWindow;
            DeleteEvent += Window_DeleteEvent;
        }

        private void UpDateUI()
        {

            var query = new QueryResponse();
            var queryforecast = new QueryResponseForeCast();

            string DayAndTime = queryforecast.DtForecast[3];

            string Day1Des = queryforecast.DescriptionForecast[0];
            string Day2Des = queryforecast.DescriptionForecast[1];
            string Day3Des = queryforecast.DescriptionForecast[2];
            string Day4Des = queryforecast.DescriptionForecast[3];
            string Day5Des = queryforecast.DescriptionForecast[4];

            double Day1Temp = Math.Round(queryforecast.TempForecast[0]);
            double Day2Temp = Math.Round(queryforecast.TempForecast[1]);
            double Day3Temp = Math.Round(queryforecast.TempForecast[2]);
            double Day4Temp = Math.Round(queryforecast.TempForecast[3]);
            double Day5Temp = Math.Round(queryforecast.TempForecast[4]);

            double Day1Fl = Math.Round(queryforecast.FeelsLikeForecast[0]);
            double Day2Fl = Math.Round(queryforecast.FeelsLikeForecast[1]);
            double Day3Fl = Math.Round(queryforecast.FeelsLikeForecast[2]);
            double Day4Fl = Math.Round(queryforecast.FeelsLikeForecast[3]);
            double Day5Fl = Math.Round(queryforecast.FeelsLikeForecast[4]);


            string Day1 = Epoch.FormatDateTime(queryforecast.DtForecast[0]);
            string Day2 = Epoch.FormatDateTime(queryforecast.DtForecast[1]);
            string Day3 = Epoch.FormatDateTime(queryforecast.DtForecast[2]);
            string Day4 = Epoch.FormatDateTime(queryforecast.DtForecast[3]);
            string Day5 = Epoch.FormatDateTime(queryforecast.DtForecast[4]);

            /*string Day1Icon = queryforecast.IconForecast[0];
            string Day2Icon = queryforecast.IconForecast[1];
            string Day3Icon = queryforecast.IconForecast[2];
            string Day4Icon = queryforecast.IconForecast[3];
            string Day5Icon = queryforecast.IconForecast[4];

            string Day1Humidity = queryforecast.HumidityForecast[0];
            string Day2Humidity = queryforecast.HumidityForecast[1];
            string Day3Humidity = queryforecast.HumidityForecast[2];
            string Day4Humidity = queryforecast.HumidityForecast[3];
            string Day5Humidity = queryforecast.HumidityForecast[4];*/
        
            string Description = query.Description;
            string  des = char.ToUpper(Description[0]) + Description.Substring(1);
            string City = query.City;
            string Country = query.Country;
            string Longitude = query.Longitude;
            string Latitude = query.Latitude;
            string Humidity = query.Humidity;
            double Temperature = Math.Round(query.Temp);
            long Timezone = query.Timezone;
            string Date = Epoch.GetTimeAndDateNow(Timezone).ToString("dddd, dd MMM yyyy");
            string Time = Epoch.GetTimeAndDateNow(Timezone).ToString("h:mm tt");
            string Wind = query.SpeedWind;
            string Pressure = query.Pressure;
            string Cloud = query.Cloudness;

            city.Text = String.Format("{0}, {1}", City,Country);
            description.Text = des;
            date.Text = Date;
            time.Text = Time;
            humidity.Text = String.Format("{0}%",Humidity);
            pressure.Text = String.Format("{0}mpa",Pressure);
            cloud.Text = String.Format("{0}%",Cloud);
            wind.Text = String.Format("{0}km/h",Wind);
            lat_lon.Text = Latitude + "/" + Longitude;
            temp.Text = String.Format("{0}C°",Temperature);

            dayonedescription.Text = char.ToUpper(Day1Des[0]) + Day1Des.Substring(1);
            daytwodescription.Text = char.ToUpper(Day2Des[0]) + Day2Des.Substring(1);
            daythreedescription.Text = char.ToUpper(Day3Des[0]) + Day3Des.Substring(1);
            dayfourdescription.Text = char.ToUpper(Day4Des[0]) + Day4Des.Substring(1);
            dayfivedescription.Text = char.ToUpper(Day5Des[0]) + Day5Des.Substring(1);

            dayonetemp.Text = String.Format("{0}/{1}C°",Day1Temp, Day1Fl);
            daytwotemp.Text = String.Format("{0}/{1}C°",Day2Temp, Day2Fl);
            daythreetemp.Text = String.Format("{0}/{1}C°",Day3Temp, Day3Fl);
            dayfourtemp.Text = String.Format("{0}/{1}C°",Day4Temp, Day4Fl);
            dayfivetemp.Text = String.Format("{0}/{1}C°",Day5Temp, Day5Fl);

            dayone.Text = Day1;
            daytwo.Text = Day2;
            daythree.Text = Day3;
            dayfour.Text = Day4;
            dayfive.Text = Day5;
        
        }
        

        /*private void Image()
        {
            var url = $"https://openweathermap.org/img/wn/10d@2x.png";
            Task<byte[]> dataArr = httpClient.GetByteArrayAsync(url);
            dataArr.Wait();
            dayoneicon.Pixbuf = new Gdk.Pixbuf (dataArr.Result);
        }*/
    
        
        
        private void SearchBar_Entered(object sender, EventArgs a)
        {  //mettre sa route 
            System.Media.SoundPlayer player = new System.Media.SoundPlayer("C:\\Users\\emman\\Desktop\\Ynov-B2\\WeatherAPI\\sound\\meteogulli.wav");  
            player.Play();     
            int count = 0;
            string message;
            string result = searchbar.Text;
            if(result != "")
            {
                var api = new API(result);
                JObject obj = api.DailyQuery();
                JObject objForecast = api.QueryForecast();
                message = obj.Value<string>("cod");
                if(message == "200")
                {
                    JArray ObjList= objForecast.Value<JArray>("list");
                    JArray Objweather = obj.Value<JArray>("weather");
                    JObject Weather = Objweather.Value<JObject>(0);
                    JObject objMain = obj.Value<JObject>("main");
                    JObject objSys = obj.Value<JObject>("sys");
                    JObject objWind = obj.Value<JObject>("wind");
                    JObject objCloud = obj.Value<JObject>("clouds");
                    JObject objCoord = obj.Value<JObject>("coord");
                
                    //Get all the data needed from the API
                    string des  = Weather.Value<string>("description");
                    //Icon = Weather.Value<string>("icon");
                    double Temp = Math.Round(objMain.Value<float>("temp"));
                    string Pressure = objMain.Value<string>("pressure");
                    string Wind = objWind.Value<string>("speed");
                    string Cloudness = objCloud.Value<string>("all");
                    string Humidity = objMain.Value<string>("humidity");
                    long Tz = obj.Value<long>("timezone");
                    string Country = objSys.Value<string>("country");
                    string City = obj.Value<string>("name");
                    string Lat = objCoord.Value<string>("lat");
                    string Lon = objCoord.Value<string>("lon");

                    string Date = Epoch.GetTimeAndDateNow(Tz).ToString("dddd, dd MMM yyyy");
                    string Time = Epoch.GetTimeAndDateNow(Tz).ToString("h:mm tt");
                    
                    city.Text = String.Format("{0}, {1}", City,Country);
                    date.Text = Date;
                    time.Text = Time;
                    description.Text  = char.ToUpper(des[0]) + des.Substring(1);
                    humidity.Text = String.Format("{0}%",Humidity);
                    pressure.Text = String.Format("{0}mpa",Pressure);
                    cloud.Text = String.Format("{0}%",Cloudness);
                    wind.Text = String.Format("{0}km/h",Wind);
                    lat_lon.Text = Lat + "/" + Lon;
                    temp.Text = String.Format("{0}C°",Temp);

                    for(int i = 0; i < ObjList.Count; i++ )
                    {
                        JObject Days = ObjList.Value<JObject>(i);
                        string dtString = Days.Value<string>("dt_txt");
                        if(dtString.Contains("12:00:00"))
                        {
                            count ++;
                            JArray DaysObjWeather = Days.Value<JArray>("weather");
                            JObject DayWeather = DaysObjWeather.Value<JObject>(0);
                            JObject DayMain = Days.Value<JObject>("main");
                            if(count == 1)
                            {
                                dayone.Text = Epoch.FormatDateTime(dtString);
                                string DayDescription = DayWeather.Value<string>("description");
                                dayonedescription.Text = char.ToUpper(DayDescription[0]) + DayDescription.Substring(1);
                                float DayTemp = DayMain.Value<float>("temp");
                                float DayFl = DayMain.Value<float>("feels_like");
                                dayonetemp.Text = String.Format("{0}/{1}C°",Math.Round(DayTemp), Math.Round(DayFl));

                            }
                            else if(count == 2)
                            {
                                daytwo.Text = Epoch.FormatDateTime(dtString);
                                string DayDescription = DayWeather.Value<string>("description");
                                daytwodescription.Text = char.ToUpper(DayDescription[0]) + DayDescription.Substring(1);
                                float DayTemp = DayMain.Value<float>("temp");
                                float DayFl = DayMain.Value<float>("feels_like");
                                daytwotemp.Text = String.Format("{0}/{1}C°",Math.Round(DayTemp), Math.Round(DayFl));
                            }
                            else if(count == 3)
                            {
                                daythree.Text = Epoch.FormatDateTime(dtString);
                                string DayDescription = DayWeather.Value<string>("description");
                                daythreedescription.Text = char.ToUpper(DayDescription[0]) + DayDescription.Substring(1);
                                float DayTemp = DayMain.Value<float>("temp");
                                float DayFl = DayMain.Value<float>("feels_like");
                                daythreetemp.Text = String.Format("{0}/{1}C°",Math.Round(DayTemp), Math.Round(DayFl));
                            }
                            else if(count == 4)
                            {
                                dayfour.Text = Epoch.FormatDateTime(dtString);
                                string DayDescription = DayWeather.Value<string>("description");
                                dayfourdescription.Text = char.ToUpper(DayDescription[0]) + DayDescription.Substring(1);
                                float DayTemp = DayMain.Value<float>("temp");
                                float DayFl = DayMain.Value<float>("feels_like");
                                dayfourtemp.Text = String.Format("{0}/{1}C°",Math.Round(DayTemp), Math.Round(DayFl));
                            }
                            else if(count == 5)
                            {
                                dayfive.Text = Epoch.FormatDateTime(dtString);
                                string DayDescription = DayWeather.Value<string>("description");
                                dayfivedescription.Text = char.ToUpper(DayDescription[0]) + DayDescription.Substring(1);
                                float DayTemp = DayMain.Value<float>("temp");
                                float DayFl = DayMain.Value<float>("feels_like");
                                dayfivetemp.Text = String.Format("{0}/{1}C°",Math.Round(DayTemp), Math.Round(DayFl));
                            }
                            string DayIcon = DayWeather.Value<string>("icon");
                        }
                    }
                }
                
            }
            
        }

        private void OptionsWindow(object sender, EventArgs args)
        {
            OptionWindow win = new OptionWindow();
            win.Show();
            this.Hide();
        }
        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

        

    }
}
