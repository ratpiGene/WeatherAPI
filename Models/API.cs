using System;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.IO;



namespace Application_meteo_csharp
{
    public class API : Config
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly HttpClient _httpClientForecast = new HttpClient();
        public string City{get;set;}
        public string Lang{get;set;}

        public API()
        {
            OptionByDefault();
            
        }
         public API(string city)
        {
            OptionByDefault();
            City = city;
        }
    
        private Uri GenerateRequestUrl() => new Uri($"https://api.openweathermap.org/data/2.5/weather?q={City}&appid={_apiKey}&lang={Lang}&units=metric");
        private Uri GenerateRequestUrlForeCast() => new Uri($"https://api.openweathermap.org/data/2.5/forecast?q={City}&appid={_apiKey}&lang={Lang}&units=metric");


        public void OptionByDefault()
        {
            string path = @".\options.json";
            bool fileExist = File.Exists(path);
            if(fileExist)
            {
                using(StreamReader reader = new StreamReader(path))
                {
                    string jsonString = reader.ReadToEnd();
                    JObject Options = JsonConvert.DeserializeObject<JObject>(jsonString);
                    City = Convert.ToString(Options["City"]);
                    Lang = Convert.ToString(Options["Lang"]);
                }
                
            
            }

        }

     
        public async Task<JObject> GetApi()
        {
            var response = await _httpClient.GetAsync(GenerateRequestUrl());
            var reponseString = await response.Content.ReadAsStringAsync();
            var obj = JObject.Parse(reponseString);
            return obj;

        }

        public async Task<JObject> GetApiForecast()
        {
            var response = await _httpClientForecast.GetAsync(GenerateRequestUrlForeCast());
            var reponseString = await response.Content.ReadAsStringAsync();
            var obj = JObject.Parse(reponseString);
            return obj;
            
        }

        public JObject DailyQuery()
        {
            return Task.Run(async () => await GetApi().ConfigureAwait(false)).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public JObject QueryForecast()
        {
            return Task.Run(async () => await GetApiForecast().ConfigureAwait(false)).ConfigureAwait(false).GetAwaiter().GetResult();

        }
    }
}