using AudenQATest.Config;
using Microsoft.Extensions.Configuration;

namespace AudenUITest.Context
{
    public class CommonContext
    {
        public static IConfiguration _config => Configuration.GetConfigurationRoot();
        public string url = _config["Url"];
    }
}