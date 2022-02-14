using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SystemTests.Lib
{
    public class Driver
    {
        private static IWebDriver _driver;

        public static IWebDriver OpenDriver(string browserName)
        {
            _driver = null;

            switch (browserName)
            {
                case "Chrome":
                    _driver = new ChromeDriver();
                    _driver.Manage().Window.Maximize();
                    break;
                case "Firefox":
                    _driver = new FirefoxDriver();
                    _driver.Manage().Window.Maximize();
                    break;
                default:
                    break;
            }

            if(_driver != null)
            {
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            }

            return _driver;
        }

        public static IWebElement WaitUntilVisible(By by, IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            var searchResultTitle = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
            return searchResultTitle;
        }
    }
}
