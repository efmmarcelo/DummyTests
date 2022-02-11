using NUnit.Framework;
using OpenQA.Selenium;
using SystemTests.Pages;

namespace SystemTests
{
    public class Tests
    {
        private IWebDriver driver;
  
        //1.Go to https://www.google.com/maps
        //2.Enter Dublin in the search box
        //3.Search
        //4.Verify left panel has "Dublin" as a headline text
        //5.Click Directions icon
        //6.Verify destination field is "Dublin"
        [Test]
        [TestCase("Chrome", "Dublin")]
        [TestCase("Firefox", "Dublin")]
        public void ValidateDirection(string browser, string location)
        {
            driver = Driver.OpenDriver(browser);

            HomePage home = new HomePage(driver);
            home.goToGoogleMaps();
            home.searchLocation(location);
            home.validateDirections(location);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }
    }
}