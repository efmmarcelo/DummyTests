using NUnit.Framework;
using OpenQA.Selenium;

namespace SystemTests.Pages
{
    class HomePage
    {
        private IWebDriver driver;
        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void goToGoogleMaps()
        {
            driver.Navigate().GoToUrl("https://www.google.com/maps");
            driver.FindElement(By.XPath("//button[@jsname='higCR']")).Click();
        }

        public void searchLocation(string location)
        {
            IWebElement searchLocationInput = driver.FindElement(By.Id("searchboxinput"));
            searchLocationInput.SendKeys(location);

            driver.FindElement(By.Id("searchbox-searchbutton")).Click();

            //Sometimes element not yet visble(only present in DOM), so we need to ensure visibility wit wait driver
            IWebElement searchResultTitle = Driver.WaitUntilVisible(By.XPath($"//h1[contains(@class,'header')][//*[text()='{location}']]"), driver);

            Assert.That(searchResultTitle.Displayed, $"Cannot validate that Left panel has {location} element text visible!");
            StringAssert.Contains("headline", searchResultTitle.GetAttribute("class"), $"Cannot validate that Left panel has {location} as headline text!");
        }

        public void validateDirections(string location)
        {
            IWebElement directionsBtn = driver.FindElement(By.XPath("//button[contains(@jsaction,'placeActions.directions')]"));
            Assert.That(directionsBtn.Enabled, "Cannot validate that directions button is clickable!");
            directionsBtn.Click();

            IWebElement destinationField1 = driver.FindElement(By.XPath($"//*[@id='directions-searchbox-1']//input[contains(@aria-label,'{location}')]"));
            Assert.That(destinationField1.Displayed, $"Cannot validate that direction '{location}' is on destination field! ");
        }
    }
}