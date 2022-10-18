using System;
using System.Net.Http;

namespace Application_meteo_csharp
{
    public class Config
    {
        public readonly string _apiKey;

        public Config()
        {
            _apiKey = "ba451f0133b7a835607a7c2dd1ea5968";
        }

        public Config(string apikey)
        {
            _apiKey = apikey;
        }
    }
}
