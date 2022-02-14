using OpenQA.Selenium;
using SystemTests.Lib;

namespace SystemTests.Pages
{
    class HomePage
    {
        private IWebDriver _driver;
        public HomePage(IWebDriver driver)
        {
            this._driver = driver;
        }

        IWebElement acceptCookiesBtn => _driver.FindElement(By.XPath("//button[@jsname='higCR']"));
        IWebElement searchLocationInput => _driver.FindElement(By.Id("searchboxinput"));
        IWebElement searchLocationBtn => _driver.FindElement(By.Id("searchbox-searchbutton"));
        IWebElement directionsBtn => _driver.FindElement(By.XPath("//button[contains(@jsaction,'placeActions.directions')]"));

        public void acceptCookies()
        {
            if (acceptCookiesBtn.Displayed) 
            {
                acceptCookiesBtn.Click();
            }
        }

        public IWebElement searchLocation(string location)
        {
            searchLocationInput.SendKeys(location);

            searchLocationBtn.Click();

            //Sometimes element not yet visble(only present in DOM), so we need to ensure visibility with wait driver
            IWebElement searchResultTitle = Driver.WaitUntilVisible(By.XPath($"//h1[contains(@class,'header')][//*[text()='{location}']]"), _driver);

            return searchResultTitle;
        }

        public bool locationVisible(string location)
        {
            directionsBtn.Click();

            IWebElement destinationField1 = Driver.WaitUntilVisible(By.XPath($"//*[@id='directions-searchbox-1']//input[contains(@aria-label,'{location}')]"), _driver);

            return destinationField1.Displayed;
        }
    }
}