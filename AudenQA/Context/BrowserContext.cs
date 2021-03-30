using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using TechTalk.SpecFlow;
using Configuration = AudenQATest.Config.Configuration;

namespace AudenQATest.Context
{
  public  class BrowserContext
    {
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver _webDriver;
        private static IConfiguration _config => Configuration.GetConfigurationRoot();
        private readonly string _fileSuffix = $"_{DateTime.UtcNow.Year}{DateTime.UtcNow.Month}{DateTime.UtcNow.Day}_{DateTime.UtcNow.Hour}{DateTime.UtcNow.Minute}{DateTime.UtcNow.Second}";
        public BrowserContext(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        public IWebDriver WebDriver
        {
            get
            {
                if (_webDriver == null)
                {
                    _webDriver= CreateWebDriver();
                }
                return _webDriver;
            }                                            
            private set { _webDriver = value; }
        }

       
        internal IWebDriver CreateWebDriver()
        {          
            var browser = _config["Browser"] ?? "chrome";
            var chromeDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            IWebDriver driver;

            switch (browser.ToLowerInvariant())
            {
                case "chrome":
                    var options = new ChromeOptions();
                    options.AddArgument("disable-save-password-bubble");
                    options.AddUserProfilePreference("credentials-enable-service", false);
                    options.AddArgument("start-maximised");
                    options.AddArgument("lang-en-GB");
                    driver= new ChromeDriver(chromeDirectory, options);
                    break;
                case "firefox":
                    FirefoxOptions firefoxOptions = new FirefoxOptions();
                    driver = new FirefoxDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                    break;
                case "remote":
                    var Options = new ChromeOptions();
                    Options.PlatformName = "windows";
                    Options.AddAdditionalCapability("platform", "WIN10", true); // Supported values: "VISTA" (Windows 7), "WIN8" (Windows 8), "WIN8_1" (windows 8.1), "WIN10" (Windows 10), "LINUX" (Linux)
                    Options.AddAdditionalCapability("version", "latest", true); // for Chrome only version=latest can be used.         
                    driver = new RemoteWebDriver(
                      new Uri("http://172.17.0.3:4444/wd/hub"), Options.ToCapabilities(), TimeSpan.FromSeconds(600));
                    return driver;
                default:
                    throw new ConfigurationErrorsException($"Unsupported {browser}");
            }
            return driver;
        }

        public void CaptureScreenshot()
        {
            //if(_scenarioContext.ScenarioExecutionStatus==ScenarioExecutionStatus.TestError)
            //{
                var fileNameBase = _scenarioContext.ScenarioInfo.Title.Replace(' ', '_') + _fileSuffix;
                var outputFolder = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase ?? string.Empty).LocalPath) + "\\TestOutput";
                TakeScreenshot(outputFolder, fileNameBase);
            //}
        }


        public void TakeScreenshot(string outputFolder,string fileNameBase)
        {
            try
            {
                if (!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }

                ITakesScreenshot captureScreenshot = _webDriver as ITakesScreenshot;

                if (captureScreenshot != null)
                {
                    var screenshot = captureScreenshot.GetScreenshot();
                    string screenshotFilePath = Path.Combine(outputFolder, fileNameBase + ".png");
                    screenshot.SaveAsFile(screenshotFilePath, ScreenshotImageFormat.Png);
                    Console.WriteLine("screenshot:{0}", new Uri(screenshotFilePath));

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("error while capturing screenshot:{0}", ex);
            }
            }

        public static void KillDriverProcesses()
        {
            foreach(var process in Process.GetProcessesByName("chromedriver"))
            {
                process.Kill();
            }
            foreach (var process in Process.GetProcessesByName("gekodriver"))
            {
                process.Kill();
            }
        }
        }

    }

