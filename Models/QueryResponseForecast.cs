
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Globalization;
using System;


namespace Application_meteo_csharp 
{
    public class QueryResponseForeCast
    {
        public long Timezone {get;}
        public List<string> DescriptionForecast {get;} =
        new List<string>();
        public List<string> IconForecast {get;} =
        new List<string>();
        public List<float> TempForecast {get;} =
        new List<float>();
        public List<string> HumidityForecast {get;} =
        new List<string>();
        public List<string> DtForecast {get;} =
        new List<string>();
        public List<float> FeelsLikeForecast {get;} =
        new List<float>();

        public string TestFr {get;}
        public QueryResponseForeCast()
		{
            //Getting the response format from the API and call the data needed
			var api = new API();
            JObject objForecast = api.QueryForecast(); //Call api for 5 days forecast

            JArray ObjList= objForecast.Value<JArray>("list"); 


            for(int i = 0; i < ObjList.Count; i++ )
            {
                JObject Days = ObjList.Value<JObject>(i);
                string dtString = Days.Value<string>("dt_txt");
                if(dtString.Contains("12:00:00"))
                {
                    JArray DaysObjWeather = Days.Value<JArray>("weather");
                    JObject DayWeather = DaysObjWeather.Value<JObject>(0);
                    JObject DayMain = Days.Value<JObject>("main");
                    string DayDescription = DayWeather.Value<string>("description");
                    DescriptionForecast.Add(DayDescription);
                    float DayTemp = DayMain.Value<float>("temp");
                    TempForecast.Add(DayTemp);
                    float DayFl = DayMain.Value<float>("feels_like");
                    FeelsLikeForecast.Add(DayFl);
                    string DayIcon = DayWeather.Value<string>("icon");
                    IconForecast.Add(DayIcon);
                    DtForecast.Add(dtString);
                }
            }
           
		}
    
    
    }
}