using System.Collections.Generic;
using System.Globalization;
using System;
using Newtonsoft.Json.Linq;


namespace Application_meteo_csharp
{
    public class QueryResponse
    {
        
        public string Description {get;}
        public string Icon {get;}
        public float Temp{get;}
        public string Pressure {get;}
        public string Humidity{get;}
        public string SpeedWind{get;}
        public string Cloudness{get;}
        public long Timezone{get;}
        public string City{get;}
        public string Country{get;}
        public string Longitude{get;}
        public string Latitude{get;}

        public QueryResponse()
		{
            //Getting the response format from the API and call the data needed
            var api = new API();
            JObject obj = api.DailyQuery();

            JArray Objweather = obj.Value<JArray>("weather");
            JObject Weather = Objweather.Value<JObject>(0);
            JObject objMain = obj.Value<JObject>("main");
            JObject objSys = obj.Value<JObject>("sys");
            JObject objWind = obj.Value<JObject>("wind");
			JObject objCloud = obj.Value<JObject>("clouds");
            JObject objCoord = obj.Value<JObject>("coord");
          
            //Get all the data needed from the API
            Description  = Weather.Value<string>("description");
            Icon = Weather.Value<string>("icon");
            Temp = objMain.Value<float>("temp");
            Pressure = objMain.Value<string>("pressure");
            SpeedWind = objWind.Value<string>("speed");
            Cloudness = objCloud.Value<string>("all");
            Humidity = objMain.Value<string>("humidity");
            Timezone = obj.Value<long>("timezone");
            Country = objSys.Value<string>("country");
            City = obj.Value<string>("name");
            Latitude = objCoord.Value<string>("lat");
            Longitude = objCoord.Value<string>("lon");
           
		}
    
    
    }
}